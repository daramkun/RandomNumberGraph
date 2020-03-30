using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberGraph.Randoms
{
	public class AdditiveLaggedFibonacciRandom : Random
	{
		Troschuetz.Random.IGenerator generator;

		public AdditiveLaggedFibonacciRandom ()
		{
			generator = new Troschuetz.Random.Generators.ALFGenerator (Environment.TickCount);
		}

		public override int Next ()
		{
			return generator.Next ();
		}
	}
}
