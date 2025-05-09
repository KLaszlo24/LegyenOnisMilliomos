using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliomos
{
	internal class Kerdesek
	{
		public List<Kerdes> Lista { get; private set; } = new();

		public void Betoltes(string fajl)
		{
			foreach (var sor in File.ReadAllLines(fajl))
			{
				Lista.Add(Kerdes.Szetszed(sor));
			}
		}

		public Kerdes Random()
		{
			var rnd = new Random();
			return Lista[rnd.Next(Lista.Count)];
		}
	}
}
