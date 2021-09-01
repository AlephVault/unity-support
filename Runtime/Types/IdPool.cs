using System.Collections.Generic;


namespace AlephVault.Unity.Support
{
    namespace Types
    {
		/// <summary>
		///   A pool of ulong integers. Adding and removing a number
		///   to the pool involves incrementing and compacting to
		///   rollback consecutively removed items from the last
		///   one, if a "right-tail" of elements is removed. Just
		///   for convenience, the 0 element is excluded from this
		///   set of pooled numbers.
		/// </summary>
		public class IdPool
		{
			private ulong last = 0;
			private SortedSet<ulong> disposed = new SortedSet<ulong>();

			/// <summary>
			///   Gets the next available value.
			/// </summary>
			public ulong Next()
			{
				if (last < ulong.MaxValue)
				{
					return ++last;
				}
				else
				{
					throw new Exception("Overflow!");
				}
			}

			/// <summary>
			///   Releases a value. Either the value is the last
			///   one, and the release can be done immediately and
			///   also right-trimming all of the values in the
			///   disposed set that are consecutive to the just
			///   removed last value, or the value to release is
			///   in the middle, and so added to the set of already
			///   disposed elements.
			/// </summary>
			/// <param name="value">The value to release</param>
			/// <returns>whether the value could be released</returns>
			public bool Release(ulong value)
			{
				if (value > last || value == 0)
				{
					// The value will never be greater than the last.
					return false;
				}
				else if (value < last)
				{
					// A "middle" value is removed. Just add it to the set of disposed values.
					// If already added to the disposed values, then nothing to do here.
					return disposed.Add(value);
				}
				else
				{
					// The value to delete is the last one. We will decrement the last value,
					// and *not* add the value to the disposed elements. Then we will run:
					// >> while the last element in the set equals to the last value:
					//    >> we pop that element from the set, and decrement the last.
					last--;
					while (disposed.Count > 0 && disposed.Max == last)
					{
						disposed.Remove(disposed.Max);
						last--;
					}
					return true;
				}
			}
		}
	}
}