using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace kreiranje_grafa
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] labirintMatrica;
            KreirajLabirintMatricu();
            Graf graf = new Graf(labirintMatrica);
            List<Vrh> vrhovi = graf.VratiVrhove();
            List<Brid> bridovi = graf.VratiBridove();
            SpremiVrhove();
            SpremiBridove();

            void KreirajLabirintMatricu()
            {
                string datoteka = AppDomain.CurrentDomain.BaseDirectory + "labirint.txt";
                string[] linije = File.ReadAllLines(datoteka);
                int brojRedaka = linije.Length;
                int brojStupaca = linije[0].Length;
                labirintMatrica = new int[brojRedaka, brojStupaca];
                for (int i = 0; i < brojRedaka; i++)
                {
                    for (int j = 0; j < brojStupaca; j++)
                    {
                        labirintMatrica[i, j] = int.Parse(linije[i][j].ToString());
                    }
                }
            }

            void SpremiBridove()
            {
                string sadrzaj = "vrh1,vrh2,tezina" + Environment.NewLine;
                string datoteka = AppDomain.CurrentDomain.BaseDirectory + "bridovi.txt";
                foreach (var brid in bridovi)
                {
                    sadrzaj += $"{brid.vrh1.naziv},{brid.vrh2.naziv},{brid.tezina}" + Environment.NewLine;
                }
                File.WriteAllText(datoteka, sadrzaj);
            }

            void SpremiVrhove()
            {
                string sadrzaj = "naziv,red,stupac" + Environment.NewLine;
                string datoteka = AppDomain.CurrentDomain.BaseDirectory + "vrhovi.txt";
                foreach (var vrh in vrhovi)
                {
                    sadrzaj += $"{vrh.naziv},{vrh.red},{vrh.stupac}" + Environment.NewLine;
                }
                File.WriteAllText(datoteka, sadrzaj);
            }

            void IspisiLabirintMatricu()
            {
                for (int i = 0; i < labirintMatrica.GetLength(0); i++)
                {
                    for (int j = 0; j < labirintMatrica.GetLength(1); j++)
                    {
                        Console.Write(labirintMatrica[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
