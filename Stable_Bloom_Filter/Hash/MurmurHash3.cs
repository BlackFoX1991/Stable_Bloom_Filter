using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 *  Eigene Implementation des 32 bit Murmur Hash 3, ausreichend für den Zweck des Bloom Filters.
 * 
 *  Weitere Infos zum Algorithmus :
 *  https://en.wikipedia.org/wiki/MurmurHash
 * 
 */
namespace Stable_Bloom_Filter.Hash
{
    public static class MurmurHash3
    {
        public static uint MurmurHash3_32(string key, uint seed = 0)
        {
            byte[] data = Encoding.UTF8.GetBytes(key);
            int length = data.Length;
            int nblocks = length / 4;

            uint h1 = seed;

            const uint c1 = 0xcc9e2d51;
            const uint c2 = 0x1b873593;

            // Body
            for (int i = 0; i < nblocks; i++)
            {
                uint k1 = BitConverter.ToUInt32(data, i * 4);

                k1 *= c1;
                k1 = RotateLeft(k1, 15);
                k1 *= c2;

                h1 ^= k1;
                h1 = RotateLeft(h1, 13);
                h1 = h1 * 5 + 0xe6546b64;
            }

            // Tail
            uint k1_tail = 0;
            int tailIndex = nblocks * 4;
            switch (length & 3)
            {
                case 3:
                    k1_tail ^= (uint)(data[tailIndex + 2] << 16);
                    goto case 2;
                case 2:
                    k1_tail ^= (uint)(data[tailIndex + 1] << 8);
                    goto case 1;
                case 1:
                    k1_tail ^= data[tailIndex];
                    k1_tail *= c1;
                    k1_tail = RotateLeft(k1_tail, 15);
                    k1_tail *= c2;
                    h1 ^= k1_tail;
                    break;
            }

            // Finalization
            h1 ^= (uint)length;
            h1 = FMix(h1);

            return h1;
        }

        private static uint RotateLeft(uint x, int r)
        {
            return x << r | x >> 32 - r;
        }

        private static uint FMix(uint h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;

            return h;
        }
    }
}
