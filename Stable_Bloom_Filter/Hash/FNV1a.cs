using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * Eigene implementation des Fowler-Noll-Vo hash
 * 
 * Weitere Infos zum Algorithmus :
 * https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
 * 
 * 
 */
namespace Stable_Bloom_Filter.Hash
{
    public static class FNV1a
    {
        public static uint FNV1aHash(string input)
        {
            const uint fnvPrime = 0x01000193;
            const uint offsetBasis = 0x811C9DC5;

            uint hash = offsetBasis;
            foreach (char c in input)
            {
                hash ^= (byte)c;
                hash *= fnvPrime;
            }

            return hash;
        }
    }
}
