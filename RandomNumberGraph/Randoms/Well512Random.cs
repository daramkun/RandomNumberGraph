using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberGraph.Randoms
{
    public class Well512Random : Random
    {
        static uint [] state = new uint [16];
        static uint index = 0;

        static Well512Random ()
        {
            Random random = new Random ((int) DateTime.Now.Ticks);

            for (int i = 0; i < 16; i++)
            {
                state [i] = (uint) random.Next ();
            }
        }

        public override int Next ()
        {
            uint a, b, c, d;

            a = state [index];
            c = state [(index + 13) & 15];
            b = a ^ c ^ (a << 16) ^ (c << 15);
            c = state [(index + 9) & 15];
            c ^= (c >> 11);
            a = state [index] = b ^ c;
            d = a ^ ((a << 5) & 0xda442d24U);
            index = (index + 15) & 15;
            a = state [index];
            state [index] = a ^ b ^ d ^ (a << 2) ^ (b << 18) ^ (c << 28);

            return (int) state [index];
        }
    }
}
