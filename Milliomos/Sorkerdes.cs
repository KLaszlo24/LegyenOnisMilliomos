using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliomos
{
	internal class Sorkerdes
	{
		public string Kerdes {  get; set; }
		public List<string> Valaszok { get; set; }
		public string HelyesRend { get; set; }


		public static Sorkerdes Szetszed(string sor)
		{
			var reszek = sor.Split(';');
			return new Sorkerdes
			{
				Kerdes = reszek[0],
				Valaszok = reszek.Skip(1).Take(4).ToList(),
				HelyesRend = reszek[5]
			};
		}
	}
}
