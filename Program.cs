using System;
using System.Collections.Generic;
using System.IO;

namespace Imenik_File_DOmaci
{
		class Program
		{
			static Dictionary<string, string> Osobe = new Dictionary<string, string>();

			static void Main(string[] args)
			{
				UcitavanjePodataka();

				char odabirKorisnika = ' ';

				while (odabirKorisnika != '5')
				{
					Meni();
					odabirKorisnika = Console.ReadKey().KeyChar;
					Console.Write("\n");

					switch (odabirKorisnika)
					{
						case '1':
							Dodavanje();
							break;
						case '2':
							Ispis();
							break;
						case '3':
							Pretraga();
							break;
						case '4':
							Brisanje();
							break;
						case '5':
							Console.WriteLine("bye :/");
							SnimanjePodataka();
							break;

					}
				}
				Console.ReadKey();
			}

			static void UcitavanjePodataka()
			{
				if (File.Exists("imenik.txt"))
				{
					foreach (string s in File.ReadLines("imenik.txt"))
					{
						string[] niz = s.Split(';');
						Osobe.Add(niz[0], niz[1]);
					}
				}
			}

			static void Meni()
			{
				Console.Clear();
				Console.WriteLine("[MENI]\n");

				Console.WriteLine("============================");
				Console.WriteLine("1 - Dodavanje osobe");
				Console.WriteLine("2 - Ispis");
				Console.WriteLine("3 - Pretraga");
				Console.WriteLine("4 - Brisanje");
				Console.WriteLine("5 - Izlazak");
				Console.WriteLine("============================");
				Console.Write("Izaberite :");
			}

			static void Dodavanje()
			{
				Console.Clear();
				Console.WriteLine("[DODAVANJE]\n");

				string ime = null,
					   broj = null;
				do
				{
					unosStringa(ref ime, "Unesite ime nove Osobe :");
					if (Osobe.ContainsKey(ime))
						Console.WriteLine("Ime vec postoji :(");
				} while (Osobe.ContainsKey(ime));

				do
				{
					unosStringa(ref broj, "Unesite novi broj :");
					if (Osobe.ContainsValue(broj))
						Console.WriteLine("Broj vec postoji :(");
				} while (Osobe.ContainsValue(broj));
				Osobe.Add(ime, broj);

				Console.WriteLine("Osoba uspesno dodata");
				Console.Write("\nPritisnite bilo koji taster za povratak na meni ");
				Console.ReadKey();
			}

			static void Ispis()
			{
				Console.Clear();
				Console.WriteLine("[ISPIS]\n");

				foreach (string s in Osobe.Keys)
				{
					Console.WriteLine("===========================================");
					Console.WriteLine($"{s} --- {Osobe[s]}");
					Console.WriteLine("===========================================");
				}

				Console.Write("\nPritisnite bilo koji taster za povratak na meni ");
				Console.ReadKey();
			}

			static void Pretraga()
			{
				Console.Clear();
				Console.WriteLine("[PRETRAGA]\n");

				string zaPretragu = null;
				unosStringa(ref zaPretragu, "Unesite ime osobe ili broj :");
				Console.Write("\n");
				foreach (string s in Osobe.Keys)
				{
					if (s.ToLower().Contains(zaPretragu.ToLower()) || Osobe[s].Contains(zaPretragu))
					{
						Console.WriteLine("===========================================");
						Console.WriteLine($"{s} --- {Osobe[s]}");
						Console.WriteLine("===========================================");
					}
				}

				Console.Write("\nPritisnite bilo koji taster za povratak na meni ");
				Console.ReadKey();
			}

			static void Brisanje()
			{
				Console.Clear();
				Console.WriteLine("[BRISANJE]\n");

				string zaPretragu = null;
				char daLiBrisati;
				unosStringa(ref zaPretragu, "Unesite ime osobe ili broj :");

				Console.Write("\n");
				foreach (string s in Osobe.Keys)
				{
					if (s.ToLower().Contains(zaPretragu.ToLower()) || Osobe[s].Contains(zaPretragu))
					{
						do
						{
							Console.Write($"Da li zelite da izbrisete osobu {s} --- {Osobe[s]} (d/n) :");
							daLiBrisati = Console.ReadKey().KeyChar;
							Console.Write("\n");
						} while (daLiBrisati != 'n' && daLiBrisati != 'd');

						if (daLiBrisati == 'd')
						{
							Osobe.Remove(s);
							Console.WriteLine("Osoba uspesno izbrisana");
							break;
						}

					}
				}

				Console.Write("\nPritisnite bilo koji taster za povratak na meni ");
				Console.ReadKey();
			}

			static void unosStringa(ref string unos, string tekst)
			{
				do
				{
					Console.Write(tekst);
					unos = Console.ReadLine();
				} while (string.IsNullOrWhiteSpace(unos));
			}

			static void SnimanjePodataka()
			{
				if (File.Exists("imenik.txt"))
					File.Delete("imenik.txt");
				foreach (string s in Osobe.Keys)
				{
					File.AppendAllText("imenik.txt", $"{s};{Osobe[s]}" + Environment.NewLine);
				}
			}

		}
}
