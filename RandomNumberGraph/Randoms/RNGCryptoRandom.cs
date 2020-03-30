using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberGraph.Randoms
{
	public class RNGCryptoRandom : Random
	{
		RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider ();
		byte [] bytes = new byte [4];

		public RNGCryptoRandom ()
		{
		}

		public override int Next ()
		{
			provider.GetBytes (bytes);
			return BitConverter.ToInt32 (bytes, 0);
		}
	}
}
