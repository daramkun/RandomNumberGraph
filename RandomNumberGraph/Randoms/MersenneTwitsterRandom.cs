using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberGraph.Randoms
{
	public class MersenneTwitsterRandom : Random
	{
		Troschuetz.Random.IGenerator generator;

		public MersenneTwitsterRandom ()
		{
			generator = new Troschuetz.Random.Generators.MT19937Generator (Environment.TickCount);
		}

		public override int Next ()
		{
			return generator.Next ();
		}
	}
}
