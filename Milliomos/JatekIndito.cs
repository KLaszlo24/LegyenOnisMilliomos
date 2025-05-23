using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Milliomos
{
    public class ValaszSzavazat{
		public char Betu;
		public int Szazalek;
	}
    internal class JatekIndito
    {
        
        private Random random=new Random();
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
			Console.WriteLine("A: " + sorok.Valaszok[0] + " B: " + sorok.Valaszok[1]);
            Console.WriteLine("C: " + sorok.Valaszok[2] + " D: " + sorok.Valaszok[3]);
            Console.Write("A helyes sorrend megadása: ");
            string valasza=Console.ReadLine().ToUpper();

            if (valasza != sorok.HelyesRend)
            {
                Console.WriteLine("Számára sajnos véget ért a játék!");
                return;
            }

            //A sorkérdések játék commit




            //Sima kérdések fognak majd innen kezdődni

            kerdesek.Betoltes("kerdes.txt");
            while (AktualSzint!=nyeremeny.Length)
            {
                Console.WriteLine($"\n{AktualSzint + 1}. kérdés és a nyeremény: {nyeremeny[AktualSzint]} Ft.");
                var randomkv = kerdesek.Random();
                Console.WriteLine("TÉTRE menő kérdések:");
                Console.WriteLine(randomkv.Szoveg);
                Console.WriteLine("A: " + randomkv.Valaszok[0] + " B: " + randomkv.Valaszok[1]);
                Console.WriteLine("C: " + randomkv.Valaszok[2] + " D: " + randomkv.Valaszok[3]);


                //Beolvasas és a nyereményes játék kész commit

                if (segitsegek.Count > 0)
                {
                    Console.WriteLine("Használható segítségek: " + string.Join(",", segitsegek));
                }

                Console.Write("A helyes válasz megadása vagy segítség használata: ");
                string valaszbeolvasas = Console.ReadLine().ToLower();

                //Válasz megadása commit



                if (segitsegek.Contains(valaszbeolvasas))
                {
                    Felhasznalas(valaszbeolvasas,randomkv );
                    segitsegek.Remove(valaszbeolvasas);
                    continue;

				} 
                if (valaszbeolvasas.ToUpper() != randomkv.HelyesValasz)
                {
                    Console.WriteLine("Helytelen a válasz");
                    Console.WriteLine($"A helyes válasz ez lett volna: {randomkv.HelyesValasz}");
                    Console.WriteLine($"Az elért nyereményed: {(AktualSzint>0? nyeremeny[AktualSzint-1]:0)} Ft");
                    return;
                }
                else
                {
                    Console.WriteLine("Helyes a válasza! Mehetünk a következő kérdésre.");
                    AktualSzint++;
                }
            }

            Console.WriteLine("Gratulálok az Ön nyereménye 50000000Ft!!!!!!");

            /*Mára a feladatam vége, majd még a segítségeknek kell megcsinálnom a metódust amit már ott fönt
            kommentben elkezdtem "Felhasznalas" néven. Azt még pontosan nem tudom hogyan kéne, de majd utánajárok,
            meg aztán színekkel kell majd még formázni. Ezen kívűl viszont működik a játék ahogyan én láttam,
            meg visszamenőleg csináltam pár stílusváltoztatást */

            
        }

		private void Felezo(Kerdes kerd)
		{
            string valaszBetuei = "ABCD";
            string helyesBetu= kerd.HelyesValasz;

            int helyesValasz=valaszBetuei.IndexOf(helyesBetu);

            List<int>rosszvalaszok = new List<int>() { 0, 1, 2, 3 };
            rosszvalaszok.Remove(helyesValasz);
            

            int Randomi=random.Next(rosszvalaszok.Count);
            int kivalasztottRossz=rosszvalaszok[Randomi];

            Console.WriteLine("Felező felhasználva");

            string helyesSzoveg = kerd.Valaszok[helyesValasz];
            string rosszBetu = valaszBetuei[kivalasztottRossz].ToString();
            string rosszSzoveg = kerd.Valaszok[kivalasztottRossz];

            Console.WriteLine("Megmaradt választási opciók:");
            Console.WriteLine($"{helyesBetu}: {helyesSzoveg}");
            Console.WriteLine($"{rosszBetu}: {rosszSzoveg}");

        }

        private void Kozonseg(Kerdes kerd)
        {
			string valaszBetuei = "ABCD";
            Random veletlen=new Random();
			string helyesBetu = kerd.HelyesValasz;

			int helyesValaszi = valaszBetuei.IndexOf(helyesBetu);

            int joszazalek=random.Next(30,50);

            int maradekszazalek=100-joszazalek;

            int[] rosszValszokSzazalek = new int[3];

            int osszszazalek = 0;

            for (int i = 0; i < 2; i++)
            {
                rosszValszokSzazalek[i] = veletlen.Next(0, maradekszazalek - osszszazalek);
                osszszazalek+=rosszValszokSzazalek[i];
            }

			rosszValszokSzazalek[2]=maradekszazalek-osszszazalek;

            List<ValaszSzavazat> szavazatok = new List<ValaszSzavazat>();

            int rosszszazaleki = 0;

			for (int i = 0;i < valaszBetuei.Length;i++)
            {
                ValaszSzavazat szavazat = new ValaszSzavazat();
                szavazat.Betu=valaszBetuei[i];

                if (i == helyesValaszi)
                {
                    szavazat.Szazalek = joszazalek;
                }
                else
                {
                    szavazat.Szazalek = rosszValszokSzazalek[rosszszazaleki];
                    rosszszazaleki++;
                }

                szavazatok.Add(szavazat);
            }

            for (int i = 0; i<szavazatok.Count;i++)
            {
                for (int j = i+1; j<szavazatok.Count;j++)
                {
                    if(szavazatok[i].Szazalek <szavazatok [j].Szazalek)
                    {
                        ValaszSzavazat t = szavazatok[i];
                        szavazatok[i] = szavazatok[j];
                        szavazatok[j] = t;

					}
                }
            }

            Console.WriteLine("Közönség szavazata felhasználva: ");
            for (int i = 0;i<szavazatok.Count; i++)
            {
                char betu = szavazatok[i].Betu;
                int szazalek = szavazatok[i].Szazalek;
                int index=valaszBetuei.IndexOf(betu);
                string szoveg = kerd.Valaszok[index];

                Console.WriteLine("- "+ betu + ": " + szazalek + "(" + szoveg + "% )" );

                //Itt volt a felezo es kozonseg commit

            }
        }

        private void Telefon(Kerdes kerd)
        {
			string valaszBetuei = "ABCD";
			Random veletlen = new Random();
			string helyesBetu = kerd.HelyesValasz;

			int helyesValaszi = valaszBetuei.IndexOf(helyesBetu);

			List<int> rosszvalaszok = new List<int>() { 0, 1, 2, 3 };
			rosszvalaszok.Remove(helyesValaszi);

            int VeletelenRossz = rosszvalaszok[veletlen.Next(rosszvalaszok.Count)];

            bool helyeseTöbb=veletlen.Next(2) == 0;

            int helyesSzazalek;
            int rosszSzazalek;

            if (helyeseTöbb)
            {
				helyesSzazalek = veletlen.Next(40, 55);
                rosszSzazalek = 100 - helyesSzazalek;

			}
            else
            {
                helyesSzazalek = veletlen.Next(20, 30);
				rosszSzazalek = 100 - helyesSzazalek;
			}

            Console.WriteLine("Telefonos segítség használva lett: ");

            Console.WriteLine("- " + valaszBetuei[helyesValaszi] + ": " + helyesSzazalek + " (" + kerd.Valaszok[helyesValaszi] + "% )");
			Console.WriteLine("- " + valaszBetuei[VeletelenRossz] + ": " + rosszSzazalek + " (" + kerd.Valaszok[VeletelenRossz] + "% )");

            //Telefonos vége commit
		}

        private void Felhasznalas(string tipusa, Kerdes kerd)
        {
            switch (tipusa.ToLower())
            {
                case "felezo":
                    Felezo(kerd); break;
				case "kozonseg":
                    Kozonseg(kerd); break;
				case "telefon":
                    Telefon(kerd); break;
                default:
                    Console.WriteLine("Ilyen segítség nincs!");
                    break;

                    //Segitseg felhasználás commit
            }
        }
	}
}
