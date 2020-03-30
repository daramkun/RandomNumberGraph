using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberGraph.Randoms
{
	public class NR3Random : Random
	{
		Troschuetz.Random.IGenerator generator;

		public NR3Random ()
		{
			generator = new Troschuetz.Random.Generators.NR3Generator (Environment.TickCount);
		}

		public override int Next ()
		{
			return generator.Next ();
		}
	}
}
