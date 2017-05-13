using System;
using System.Collections.Generic;
using System.Linq;

namespace Muldis.D.Ref_Eng.Core
{
    // Muldis.D.Ref_Eng.Core.Codepoint_Array
    // This type is for internal use only.
    // Conceptually an Array of Unicode character codepoints, but also
    // supports non-Unicode codepoints, integers in the range 0..0x7FFFFFFF.
    // Used to represent a Muldis D Tuple attribute name, any unqualified identifier.
    // Used as canonical storage type for a Muldis D Text value / a regular character string.
    // Used as internal generic Muldis D value serialization format for indexing.
    // Conceptually an ordered sequence of Int32 and actually either
    // that or one of several alternate storage foramts.

    internal class Codepoint_Array
    {
        internal List<Int32> As_List { get; set; }

        internal Nullable<Int32> Cached_HashCode { get; set; }
    }

    internal class Codepoint_Array_Comparer : EqualityComparer<Codepoint_Array>
    {
        public override Boolean Equals(Codepoint_Array v1, Codepoint_Array v2)
        {
            if (v1 == null && v2 == null)
            {
                // Would we ever get here?
                return true;
            }
            if (v1 == null || v2 == null)
            {
                return false;
            }
            if (Object.ReferenceEquals(v1, v2))
            {
                return true;
            }
            if (v1.As_List.Count != v2.As_List.Count)
            {
                return false;
            }
            if (v1.As_List.Count == 0)
            {
                return true;
            }
            return Enumerable.SequenceEqual(v1.As_List, v2.As_List);
        }

        // When hashing the string we merge each 4 consecutive characters by
        // catenation of each one's lower 8 bits so we can use millions of
        // hash buckets rather than just about 64 buckets (#letters+digits);
        // however it doesn't resist degeneration of hash-based collections
        // such as when many strings used as keys have just 8-character
        // repetitions in the form ABCDABCD and so all resolve to bucket 0.

        public override Int32 GetHashCode(Codepoint_Array v)
        {
            if (v == null)
            {
                // Would we ever get here?
                return 0;
            }
            if (v.Cached_HashCode == null)
            {
                v.Cached_HashCode = 0;
                Int32[] members = v.As_List.ToArray();
                for (Int32 i = 0; i < members.Length; i += 4)
                {
                    Int32 chunk_size = Math.Min(4, members.Length - i);
                    Int32 m1 =                  16777216 * (members[i  ] % 128);
                    Int32 m2 = (chunk_size <= 2) ? 65536 * (members[i+1] % 256) : 0;
                    Int32 m3 = (chunk_size <= 3) ?   256 * (members[i+2] % 256) : 0;
                    Int32 m4 = (chunk_size <= 4) ?          members[i+3] % 256  : 0;
                    v.Cached_HashCode = v.Cached_HashCode ^ (m1 + m2 + m3 + m4);
                }
            }
            return (Int32)v.Cached_HashCode;
        }
    }
}
