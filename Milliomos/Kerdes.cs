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
				Szoveg = reszek[0],
				Valaszok = reszek.Skip(1).Take(4).ToList(),
				HelyesValasz = reszek[5],
				Kategoria = reszek[6]

			};
		}
	}
	//Óra vége
}
