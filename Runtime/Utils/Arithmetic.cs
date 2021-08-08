using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlephVault.Unity.Support
{
    namespace Utils
    {
        /// <summary>
        ///   <para>
        ///     This class contains several utilities to compute
        ///     rounded integer divisions and related operations.
        ///   </para>
        ///   <para>
        ///     Essentially stolen from MLAPI package.
        ///   </para>
        /// </summary>
        public static class Arithmetic
        {
            /// <summary>
            ///   The minimum long-64 value.
            /// </summary>
            public const long SIGN_BIT_64 = long.MinValue;

            /// <summary>
            ///   Divides a number and rounds up.
            /// </summary>
            /// <param name="u1">Dividend</param>
            /// <param name="u2">Divider</param>
            /// <returns>The quotient, rounded up</returns>
            internal static ulong CeilingExact(ulong u1, ulong u2) => (u1 + u2 - 1) / u2;

            /// <summary>
            ///   Divides a number and rounds up.
            /// </summary>
            /// <param name="u1">Dividend</param>
            /// <param name="u2">Divider</param>
            /// <returns>The quotient, rounded up</returns>
            internal static uint CeilingExact(uint u1, uint u2) => (u1 + u2 - 1) / u2;

            /// <summary>
            ///   Divides a number and rounds up.
            /// </summary>
            /// <param name="u1">Dividend</param>
            /// <param name="u2">Divider</param>
            /// <returns>The quotient, rounded up</returns>
            internal static ushort CeilingExact(ushort u1, ushort u2) => (ushort)((u1 + u2 - 1) / u2);

            /// <summary>
            ///   Divides a number and rounds up.
            /// </summary>
            /// <param name="u1">Dividend</param>
            /// <param name="u2">Divider</param>
            /// <returns>The quotient, rounded up</returns>
            internal static byte CeilingExact(byte u1, byte u2) => (byte)((u1 + u2 - 1) / u2);

            /// <summary>
            /// ZigZag encodes a signed integer and maps it to a unsigned integer
            /// </summary>
            /// <param name="value">The signed integer to encode</param>
            /// <returns>A ZigZag encoded version of the integer</returns>
            public static ulong ZigZagEncode(long value) => (ulong)((value >> 63) ^ (value << 1));

            /// <summary>
            /// Decides a ZigZag encoded integer back to a signed integer
            /// </summary>
            /// <param name="value">The unsigned integer</param>
            /// <returns>The signed version of the integer</returns>
            public static long ZigZagDecode(ulong value) => (((long)(value >> 1) & 0x7FFFFFFFFFFFFFFFL) ^ ((long)(value << 63) >> 63));

            /// <summary>
            /// Gets the output size in bytes after VarInting a unsigned integer
            /// </summary>
            /// <param name="value">The unsigned integer whose length to get</param>
            /// <returns>The amount of bytes</returns>
            public static int VarIntSize(ulong value) =>
                value <= 240 ? 1 :
                value <= 2287 ? 2 :
                value <= 67823 ? 3 :
                value <= 16777215 ? 4 :
                value <= 4294967295 ? 5 :
                value <= 1099511627775 ? 6 :
                value <= 281474976710655 ? 7 :
                value <= 72057594037927935 ? 8 :
                9;

            /// <summary>
            ///   Divides a number by 8 and rounds up.
            /// </summary>
            /// <param name="value">The number to divide</param>
            /// <returns>The divided and rounded value</returns>
            public static long Div8Ceil(ulong value) => (long)((value >> 3) + ((value & 1UL) | ((value >> 1) & 1UL) | ((value >> 2) & 1UL)));
        }
    }
}
