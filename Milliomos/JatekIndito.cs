using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Milliomos
{
    internal class JatekIndito
    {
        private Kerdesek kerdesek = new Kerdesek();
        private List<string> segitsegek = new() { "kozonseg", "telefon", "felezo" };
        private int AktualSzint = 0;
        private int[] nyeremeny = { 10000, 20000, 50000, 100000, 250000, 500000, 750000, 1000000, 1500000, 2000000, 5000000, 10000000, 15000000, 25000000, 50000000 };

        public void Indit()
        {
            string[] sorkerdesSorok = File.ReadAllLines("sorkerdes.txt");
            List<Sorkerdes> sorkerdesek = new List<Sorkerdes>();
            foreach (string sor in sorkerdesSorok)
            {
                sorkerdesek.Add(Sorkerdes.Szetszed(sor));
            }
            //Sorkerdesek beolvasasa commit

            Random rand= new Random();
            int index = rand.Next(sorkerdesek.Count);
            Sorkerdes sorok= sorkerdesek[index];

            Console.WriteLine("A játék kezdete");
            Console.WriteLine("SORKÉRDÉS következik:");
            Console.WriteLine(sorok.Kerdes);
            Console.WriteLine("A " + sorok.Valaszok[0] + "B " + sorok.Valaszok[1]);
            Console.WriteLine("C " + sorok.Valaszok[2] + "D " + sorok.Valaszok[3]);
            Console.WriteLine("A helyes sorrend megadása: ");
            string valasza=Console.ReadLine().ToUpper();

            if (valasza != sorok.HelyesRend)
            {
                Console.WriteLine("Számára sajnos véget ért a játék!");
                return;
            }

            //A sorkérdések játék commit
        }
    }
}
