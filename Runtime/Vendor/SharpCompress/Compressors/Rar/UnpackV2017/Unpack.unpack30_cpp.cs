﻿#if !Rar2017_64bit
using nint = System.Int32;
using nuint = System.UInt32;
using size_t = System.UInt32;
#else
using nint = System.Int64;
using nuint = System.UInt64;
using size_t = System.UInt64;
#endif
using int64 = System.Int64;

//using static SharpCompress.Compressors.Rar.UnpackV2017.Unpack.Unpack30Local;
/*
namespace SharpCompress.Compressors.Rar.UnpackV2017
{
    internal partial class Unpack
    {

#if !RarV2017_RAR5ONLY
// We use it instead of direct PPM.DecodeChar call to be sure that
// we reset PPM structures in case of corrupt data. It is important,
// because these structures can be invalid after PPM.DecodeChar returned -1.
int SafePPMDecodeChar()
{
  int Ch=PPM.DecodeChar();
  if (Ch==-1)              // Corrupt PPM data found.
  {
    PPM.CleanUp();         // Reset possibly corrupt PPM data structures.
    UnpBlockType=BLOCK_LZ; // Set faster and more fail proof LZ mode.
  }
  return(Ch);
}

internal static class Unpack30Local {
  public static readonly byte[] LDecode={0,1,2,3,4,5,6,7,8,10,12,14,16,20,24,28,32,40,48,56,64,80,96,112,128,160,192,224};
  public static readonly byte[] LBits=  {0,0,0,0,0,0,0,0,1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4,  4,  5,  5,  5,  5};
  public static readonly int[] DDecode = new int[DC];
  public static readonly byte[] DBits = new byte[DC];
  public static readonly int[] DBitLengthCounts= {4,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,14,0,12};
  public static readonly byte[] SDDecode={0,4,8,16,32,64,128,192};
  public static readonly byte[] SDBits=  {2,2,3, 4, 5, 6,  6,  6};
}
void Unpack29(bool Solid)
{
  uint Bits;

  if (DDecode[1]==0)
  {
    int Dist=0,BitLength=0,Slot=0;
    for (int I=0;I<DBitLengthCounts.Length;I++,BitLength++)
      for (int J=0;J<DBitLengthCounts[I];J++,Slot++,Dist+=(1<<BitLength))
      {
        DDecode[Slot]=Dist;
        DBits[Slot]=(byte)BitLength;
      }
  }

  FileExtracted=true;

  if (!Suspended)
  {
    UnpInitData(Solid);
    if (!UnpReadBuf30())
      return;
    if ((!Solid || !TablesRead3) && !ReadTables30())
      return;
  }

  while (true)
  {
    UnpPtr&=MaxWinMask;

    if (Inp.InAddr>ReadBorder)
    {
      if (!UnpReadBuf30())
        break;
    }
    if (((WrPtr-UnpPtr) & MaxWinMask)<260 && WrPtr!=UnpPtr)
    {
      UnpWriteBuf30();
      if (WrittenFileSize>DestUnpSize)
        return;
      if (Suspended)
      {
        FileExtracted=false;
        return;
      }
    }
    if (UnpBlockType==BLOCK_PPM)
    {
      // Here speed is critical, so we do not use SafePPMDecodeChar,
      // because sometimes even the inline function can introduce
      // some additional penalty.
      int Ch=PPM.DecodeChar();
      if (Ch==-1)              // Corrupt PPM data found.
      {
        PPM.CleanUp();         // Reset possibly corrupt PPM data structures.
        UnpBlockType=BLOCK_LZ; // Set faster and more fail proof LZ mode.
        break;
      }
      if (Ch==PPMEscChar)
      {
        int NextCh=SafePPMDecodeChar();
        if (NextCh==0)  // End of PPM encoding.
        {
          if (!ReadTables30())
            break;
          continue;
        }
        if (NextCh==-1) // Corrupt PPM data found.
          break;
        if (NextCh==2)  // End of file in PPM mode.
          break;
        if (NextCh==3)  // Read VM code.
        {
          if (!ReadVMCodePPM())
            break;
          continue;
        }
        if (NextCh==4) // LZ inside of PPM.
        {
          uint Distance=0,Length;
          bool Failed=false;
          for (int I=0;I<4 && !Failed;I++)
          {
            int _Ch=SafePPMDecodeChar();
            if (_Ch==-1)
              Failed=true;
            else
              if (I==3)
                Length=(byte)_Ch;
              else
                Distance=(Distance<<8)+(byte)_Ch;
          }
          if (Failed)
            break;

          CopyString(Length+32,Distance+2);
          continue;
        }
        if (NextCh==5) // One byte distance match (RLE) inside of PPM.
        {
          int Length=SafePPMDecodeChar();
          if (Length==-1)
            break;
          CopyString((uint)(Length+4),1);
          continue;
        }
        // If we are here, NextCh must be 1, what means that current byte
        // is equal to our 'escape' byte, so we just store it to Window.
      }
      Window[UnpPtr++]=(byte)Ch;
      continue;
    }

    uint Number=DecodeNumber(Inp,BlockTables.LD);
    if (Number<256)
    {
      Window[UnpPtr++]=(byte)Number;
      continue;
    }
    if (Number>=271)
    {
      uint Length=(uint)(LDecode[Number-=271]+3);
      if ((Bits=LBits[Number])>0)
      {
        Length+=Inp.getbits()>>(int)(16-Bits);
        Inp.addbits(Bits);
      }

      uint DistNumber=DecodeNumber(Inp,BlockTables.DD);
      uint Distance=(uint)(DDecode[DistNumber]+1);
      if ((Bits=DBits[DistNumber])>0)
      {
        if (DistNumber>9)
        {
          if (Bits>4)
          {
            Distance+=((Inp.getbits()>>(int)(20-Bits))<<4);
            Inp.addbits(Bits-4);
          }
          if (LowDistRepCount>0)
          {
            LowDistRepCount--;
            Distance+=(uint)PrevLowDist;
          }
          else
          {
            uint LowDist=DecodeNumber(Inp,BlockTables.LDD);
            if (LowDist==16)
            {
              LowDistRepCount=(int)(LOW_DIST_REP_COUNT-1);
              Distance+=(uint)PrevLowDist;
            }
            else
            {
              Distance+=LowDist;
              PrevLowDist=(int)LowDist;
            }
          }
        }
        else
        {
          Distance+=Inp.getbits()>>(int)(16-Bits);
          Inp.addbits(Bits);
        }
      }

      if (Distance>=0x2000)
      {
        Length++;
        if (Distance>=0x40000)
          Length++;
      }

      InsertOldDist(Distance);
      LastLength=Length;
      CopyString(Length,Distance);
      continue;
    }
    if (Number==256)
    {
      if (!ReadEndOfBlock())
        break;
      continue;
    }
    if (Number==257)
    {
      if (!ReadVMCode())
        break;
      continue;
    }
    if (Number==258)
    {
      if (LastLength!=0)
        CopyString(LastLength,OldDist[0]);
      continue;
    }
    if (Number<263)
    {
      uint DistNum=Number-259;
      uint Distance=OldDist[DistNum];
      for (uint I=DistNum;I>0;I--)
        OldDist[I]=OldDist[I-1];
      OldDist[0]=Distance;

      uint LengthNumber=DecodeNumber(Inp,BlockTables.RD);
      int Length=LDecode[LengthNumber]+2;
      if ((Bits=LBits[LengthNumber])>0)
      {
        Length+=(int)(Inp.getbits()>>(int)(16-Bits));
        Inp.addbits(Bits);
      }
      LastLength=(uint)Length;
      CopyString((uint)Length,Distance);
      continue;
    }
    if (Number<272)
    {
      uint Distance=(uint)(SDDecode[Number-=263]+1);
      if ((Bits=SDBits[Number])>0)
      {
        Distance+=Inp.getbits()>>(int)(16-Bits);
        Inp.addbits(Bits);
      }
      InsertOldDist(Distance);
      LastLength=2;
      CopyString(2,Distance);
      continue;
    }
  }
  UnpWriteBuf30();
}


// Return 'false' to quit unpacking the current file or 'true' to continue.
bool ReadEndOfBlock()
{
  uint BitField=Inp.getbits();
  bool NewTable,NewFile=false;

  // "1"  - no new file, new table just here.
  // "00" - new file,    no new table.
  // "01" - new file,    new table (in beginning of next file).
  
  if ((BitField & 0x8000)!=0)
  {
    NewTable=true;
    Inp.addbits(1);
  }
  else
  {
    NewFile=true;
    NewTable=(BitField & 0x4000)!=0;
    Inp.addbits(2);
  }
  TablesRead3=!NewTable;

  // Quit immediately if "new file" flag is set. If "new table" flag
  // is present, we'll read the table in beginning of next file
  // based on 'TablesRead3' 'false' value.
  if (NewFile)
    return false;
  return ReadTables30(); // Quit only if we failed to read tables.
}


bool ReadVMCode()
{
  // Entire VM code is guaranteed to fully present in block defined 
  // by current Huffman table. Compressor checks that VM code does not cross
  // Huffman block boundaries.
  uint FirstByte=Inp.getbits()>>8;
  Inp.addbits(8);
  uint Length=(FirstByte & 7)+1;
  if (Length==7)
  {
    Length=(Inp.getbits()>>8)+7;
    Inp.addbits(8);
  }
  else
    if (Length==8)
    {
      Length=Inp.getbits();
      Inp.addbits(16);
    }
  if (Length==0)
    return false;
  Array<byte> VMCode(Length);
  for (uint I=0;I<Length;I++)
  {
    // Try to read the new buffer if only one byte is left.
    // But if we read all bytes except the last, one byte is enough.
    if (Inp.InAddr>=ReadTop-1 && !UnpReadBuf30() && I<Length-1)
      return false;
    VMCode[I]=Inp.getbits()>>8;
    Inp.addbits(8);
  }
  return AddVMCode(FirstByte,&VMCode[0],Length);
}


bool ReadVMCodePPM()
{
  uint FirstByte=(uint)SafePPMDecodeChar();
  if ((int)FirstByte==-1)
    return false;
  uint Length=(FirstByte & 7)+1;
  if (Length==7)
  {
    int B1=SafePPMDecodeChar();
    if (B1==-1)
      return false;
    Length=B1+7;
  }
  else
    if (Length==8)
    {
      int B1=SafePPMDecodeChar();
      if (B1==-1)
        return false;
      int B2=SafePPMDecodeChar();
      if (B2==-1)
        return false;
      Length=B1*256+B2;
    }
  if (Length==0)
    return false;
  Array<byte> VMCode(Length);
  for (uint I=0;I<Length;I++)
  {
    int Ch=SafePPMDecodeChar();
    if (Ch==-1)
      return false;
    VMCode[I]=Ch;
  }
  return AddVMCode(FirstByte,&VMCode[0],Length);
}


bool AddVMCode(uint FirstByte,byte[] Code,int CodeSize)
{
  VMCodeInp.InitBitInput();
  //x memcpy(VMCodeInp.InBuf,Code,Min(BitInput.MAX_SIZE,CodeSize));
  Array.Copy(Code, 0, VMCodeInp.InBuf, 0, Math.Min(BitInput.MAX_SIZE,CodeSize));
  VM.Init();

  uint FiltPos;
  if ((FirstByte & 0x80)!=0)
  {
    FiltPos=RarVM.ReadData(VMCodeInp);
    if (FiltPos==0)
      InitFilters30(false);
    else
      FiltPos--;
  }
  else
    FiltPos=(uint)this.LastFilter; // Use the same filter as last time.

  if (FiltPos>Filters30.Count || FiltPos>OldFilterLengths.Count)
    return false;
  LastFilter=(int)FiltPos;
  bool NewFilter=(FiltPos==Filters30.Count);

  UnpackFilter30 StackFilter=new UnpackFilter30(); // New filter for PrgStack.

  UnpackFilter30 Filter;
  if (NewFilter) // New filter code, never used before since VM reset.
  {
    if (FiltPos>MAX3_UNPACK_FILTERS)
    {
      // Too many different filters, corrupt archive.
      //delete StackFilter;
      return false;
    }

    Filters30.Add(1);
    Filters30[Filters30.Count-1]=Filter=new UnpackFilter30();
    StackFilter.ParentFilter=(uint)(Filters30.Count-1);

    // Reserve one item to store the data block length of our new filter 
    // entry. We'll set it to real block length below, after reading it.
    // But we need to initialize it now, because when processing corrupt
    // data, we can access this item even before we set it to real value.
    OldFilterLengths.Add(0);
  }
  else  // Filter was used in the past.
  {
    Filter=Filters30[(int)FiltPos];
    StackFilter.ParentFilter=FiltPos;
  }

  int EmptyCount=0;
  for (int I=0;I<PrgStack.Count;I++)
  {
    PrgStack[I-EmptyCount]=PrgStack[I];
    if (PrgStack[I]==null)
      EmptyCount++;
    if (EmptyCount>0)
      PrgStack[I]=null;
  }
  if (EmptyCount==0)
  {
    if (PrgStack.Count>MAX3_UNPACK_FILTERS)
    {
      //delete StackFilter;
      return false;
    }
    PrgStack.Add(1);
    EmptyCount=1;
  }
  size_t StackPos=(uint)(this.PrgStack.Count-EmptyCount);
  PrgStack[(int)StackPos]=StackFilter;
 
  uint BlockStart=RarVM.ReadData(VMCodeInp);
  if ((FirstByte & 0x40)!=0)
    BlockStart+=258;
  StackFilter.BlockStart=(uint)((BlockStart+UnpPtr)&MaxWinMask);
  if ((FirstByte & 0x20)!=0)
  {
    StackFilter.BlockLength=RarVM.ReadData(VMCodeInp);

    // Store the last data block length for current filter.
    OldFilterLengths[(int)FiltPos]=(int)StackFilter.BlockLength;
  }
  else
  {
    // Set the data block size to same value as the previous block size
    // for same filter. It is possible for corrupt data to access a new 
    // and not filled yet item of OldFilterLengths array here. This is why
    // we set new OldFilterLengths items to zero above.
    StackFilter.BlockLength=FiltPos<OldFilterLengths.Count ? OldFilterLengths[(int)FiltPos]:0;
  }

  StackFilter.NextWindow=WrPtr!=UnpPtr && ((WrPtr-UnpPtr)&MaxWinMask)<=BlockStart;

//  DebugLog("\nNextWindow: UnpPtr=%08x WrPtr=%08x BlockStart=%08x",UnpPtr,WrPtr,BlockStart);

  memset(StackFilter.Prg.InitR,0,sizeof(StackFilter.Prg.InitR));
  StackFilter.Prg.InitR[4]=StackFilter.BlockLength;

  if ((FirstByte & 0x10)!=0) // Set registers to optional parameters if any.
  {
    uint InitMask=VMCodeInp.fgetbits()>>9;
    VMCodeInp.faddbits(7);
    for (int I=0;I<7;I++)
      if ((InitMask & (1<<I)) != 0)
        StackFilter.Prg.InitR[I]=RarVM.ReadData(VMCodeInp);
  }

  if (NewFilter)
  {
    uint VMCodeSize=RarVM.ReadData(VMCodeInp);
    if (VMCodeSize>=0x10000 || VMCodeSize==0)
      return false;
    Array<byte> VMCode(VMCodeSize);
    for (uint I=0;I<VMCodeSize;I++)
    {
      if (VMCodeInp.Overflow(3))
        return false;
      VMCode[I]=VMCodeInp.fgetbits()>>8;
      VMCodeInp.faddbits(8);
    }
    VM.Prepare(&VMCode[0],VMCodeSize,&Filter->Prg);
  }
  StackFilter.Prg.Type=Filter.Prg.Type;

  return true;
}


bool UnpReadBuf30()
{
  int DataSize=ReadTop-Inp.InAddr; // Data left to process.
  if (DataSize<0)
    return false;
  if (Inp.InAddr>BitInput.MAX_SIZE/2)
  {
    // If we already processed more than half of buffer, let's move
    // remaining data into beginning to free more space for new data
    // and ensure that calling function does not cross the buffer border
    // even if we did not read anything here. Also it ensures that read size
    // is not less than CRYPT_BLOCK_SIZE, so we can align it without risk
    // to make it zero.
    if (DataSize>0)
      //x memmove(Inp.InBuf,Inp.InBuf+Inp.InAddr,DataSize);
      Array.Copy(Inp.InBuf,Inp.InAddr,Inp.InBuf,0,DataSize);
    Inp.InAddr=0;
    ReadTop=DataSize;
  }
  else
    DataSize=ReadTop;
  int ReadCode=UnpIO_UnpRead(Inp.InBuf,DataSize,BitInput.MAX_SIZE-DataSize);
  if (ReadCode>0)
    ReadTop+=ReadCode;
  ReadBorder=ReadTop-30;
  return ReadCode!=-1;
}


void UnpWriteBuf30()
{
  uint WrittenBorder=(uint)WrPtr;
  uint WriteSize=(uint)((UnpPtr-WrittenBorder)&MaxWinMask);
  for (int I=0;I<PrgStack.Count;I++)
  {
    // Here we apply filters to data which we need to write.
    // We always copy data to virtual machine memory before processing.
    // We cannot process them just in place in Window buffer, because
    // these data can be used for future string matches, so we must
    // preserve them in original form.

    UnpackFilter30 flt=PrgStack[I];
    if (flt==null)
      continue;
    if (flt.NextWindow)
    {
      flt.NextWindow=false;
      continue;
    }
    uint BlockStart=flt.BlockStart;
    uint BlockLength=flt.BlockLength;
    if (((BlockStart-WrittenBorder)&MaxWinMask)<WriteSize)
    {
      if (WrittenBorder!=BlockStart)
      {
        UnpWriteArea(WrittenBorder,BlockStart);
        WrittenBorder=BlockStart;
        WriteSize=(uint)((UnpPtr-WrittenBorder)&MaxWinMask);
      }
      if (BlockLength<=WriteSize)
      {
        uint BlockEnd=(BlockStart+BlockLength)&MaxWinMask;
        if (BlockStart<BlockEnd || BlockEnd==0)
          VM.SetMemory(0,Window+BlockStart,BlockLength);
        else
        {
          uint FirstPartLength=uint(MaxWinSize-BlockStart);
          VM.SetMemory(0,Window+BlockStart,FirstPartLength);
          VM.SetMemory(FirstPartLength,Window,BlockEnd);
        }

        VM_PreparedProgram *ParentPrg=&Filters30[flt->ParentFilter]->Prg;
        VM_PreparedProgram *Prg=&flt->Prg;

        ExecuteCode(Prg);

        byte[] FilteredData=Prg.FilteredData;
        uint FilteredDataSize=Prg.FilteredDataSize;

        delete PrgStack[I];
        PrgStack[I]=null;
        while (I+1<PrgStack.Count)
        {
          UnpackFilter30 NextFilter=PrgStack[I+1];
          // It is required to check NextWindow here.
          if (NextFilter==null || NextFilter.BlockStart!=BlockStart ||
              NextFilter.BlockLength!=FilteredDataSize || NextFilter.NextWindow)
            break;

          // Apply several filters to same data block.

          VM.SetMemory(0,FilteredData,FilteredDataSize);

          VM_PreparedProgram *ParentPrg=&Filters30[NextFilter.ParentFilter]->Prg;
          VM_PreparedProgram *NextPrg=&NextFilter->Prg;

          ExecuteCode(NextPrg);

          FilteredData=NextPrg.FilteredData;
          FilteredDataSize=NextPrg.FilteredDataSize;
          I++;
          delete PrgStack[I];
          PrgStack[I]=null;
        }
        UnpIO_UnpWrite(FilteredData,0,FilteredDataSize);
        UnpSomeRead=true;
        WrittenFileSize+=FilteredDataSize;
        WrittenBorder=BlockEnd;
        WriteSize=(uint)((UnpPtr-WrittenBorder)&MaxWinMask);
      }
      else
      {
        // Current filter intersects the window write border, so we adjust
        // the window border to process this filter next time, not now.
        for (size_t J=I;J<PrgStack.Count;J++)
        {
          UnpackFilter30 flt=PrgStack[J];
          if (flt!=null && flt.NextWindow)
            flt.NextWindow=false;
        }
        WrPtr=WrittenBorder;
        return;
      }
    }
  }
      
  UnpWriteArea(WrittenBorder,UnpPtr);
  WrPtr=UnpPtr;
}


void ExecuteCode(VM_PreparedProgram *Prg)
{
  Prg->InitR[6]=(uint)WrittenFileSize;
  VM.Execute(Prg);
}


bool ReadTables30()
{
  byte[] BitLength = new byte[BC];
  byte[] Table = new byte[HUFF_TABLE_SIZE30];
  if (Inp.InAddr>ReadTop-25)
    if (!UnpReadBuf30())
      return(false);
  Inp.faddbits((uint)((8-Inp.InBit)&7));
  uint BitField=Inp.fgetbits();
  if ((BitField & 0x8000) != 0)
  {
    UnpBlockType=BLOCK_PPM;
    return(PPM.DecodeInit(this,PPMEscChar));
  }
  UnpBlockType=BLOCK_LZ;
  
  PrevLowDist=0;
  LowDistRepCount=0;

  if ((BitField & 0x4000) == 0)
    Utility.Memset(UnpOldTable,0,UnpOldTable.Length);
  Inp.faddbits(2);

  for (uint I=0;I<BC;I++)
  {
    uint Length=(byte)(Inp.fgetbits() >> 12);
    Inp.faddbits(4);
    if (Length==15)
    {
      uint ZeroCount=(byte)(Inp.fgetbits() >> 12);
      Inp.faddbits(4);
      if (ZeroCount==0)
        BitLength[I]=15;
      else
      {
        ZeroCount+=2;
        while (ZeroCount-- > 0 && I<BitLength.Length)
          BitLength[I++]=0;
        I--;
      }
    }
    else
      BitLength[I]=(byte)Length;
  }
  MakeDecodeTables(BitLength,0,BlockTables.BD,BC30);

  const uint TableSize=HUFF_TABLE_SIZE30;
  for (uint I=0;I<TableSize;)
  {
    if (Inp.InAddr>ReadTop-5)
      if (!UnpReadBuf30())
        return(false);
    uint Number=DecodeNumber(Inp,BlockTables.BD);
    if (Number<16)
    {
      Table[I]=(byte)((Number+this.UnpOldTable[I]) & 0xf);
      I++;
    }
    else
      if (Number<18)
      {
        uint N;
        if (Number==16)
        {
          N=(Inp.fgetbits() >> 13)+3;
          Inp.faddbits(3);
        }
        else
        {
          N=(Inp.fgetbits() >> 9)+11;
          Inp.faddbits(7);
        }
        if (I==0)
          return false; // We cannot have "repeat previous" code at the first position.
        else
          while (N-- > 0 && I<TableSize)
          {
            Table[I]=Table[I-1];
            I++;
          }
      }
      else
      {
        uint N;
        if (Number==18)
        {
          N=(Inp.fgetbits() >> 13)+3;
          Inp.faddbits(3);
        }
        else
        {
          N=(Inp.fgetbits() >> 9)+11;
          Inp.faddbits(7);
        }
        while (N-- > 0 && I<TableSize)
          Table[I++]=0;
      }
  }
  TablesRead3=true;
  if (Inp.InAddr>ReadTop)
    return false;
  MakeDecodeTables(Table,0,BlockTables.LD,NC30);
  MakeDecodeTables(Table,(int)NC30,BlockTables.DD,DC30);
  MakeDecodeTables(Table,(int)(NC30+DC30),BlockTables.LDD,LDC30);
  MakeDecodeTables(Table,(int)(NC30+DC30+LDC30),BlockTables.RD,RC30);
  //x memcpy(UnpOldTable,Table,sizeof(UnpOldTable));
  Array.Copy(Table,0,UnpOldTable,0,UnpOldTable.Length);
  return true;
}

#endif

void UnpInitData30(bool Solid)
{
  if (!Solid)
  {
    TablesRead3=false;
    Utility.Memset(UnpOldTable, 0, UnpOldTable.Length);
    PPMEscChar=2;
    UnpBlockType=BLOCK_LZ;
  }
  InitFilters30(Solid);
}


void InitFilters30(bool Solid)
{
  if (!Solid)
  {
    //OldFilterLengths.SoftReset();
    OldFilterLengths.Clear();
    LastFilter=0;

    //for (size_t I=0;I<Filters30.Count;I++)
    //  delete Filters30[I];
    //Filters30.SoftReset();
    Filters30.Clear();
  }
  //for (size_t I=0;I<PrgStack.Count;I++)
  //  delete PrgStack[I];
  //PrgStack.SoftReset();
  PrgStack.Clear();
}


    }
}
*/