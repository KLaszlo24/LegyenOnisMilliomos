using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliomos
{
	internal class Kerdes
	{
		public string Szoveg { get; set; }
		public List<string> Valaszok { get; set; }
		public string HelyesValasz { get; set; }
		public string Kategoria { get; set; }

		public static Kerdes Szetszed(string sor)
		{
			var reszek = sor.Split(';');
			return new Kerdes
			{
				Szoveg = reszek[1],
				Valaszok = reszek.Skip(2).Take(4).ToList(),
				HelyesValasz = reszek[6],
				Kategoria = reszek[7]

			};
		}
	}
	//Óra vége
}
