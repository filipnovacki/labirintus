using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kreiranje_grafa
{
    public class Graf
    {
        private int[,] labirintMatrica;
        private List<Vrh> vrhovi;
        private List<Vrh> nedovrseniVrhovi;
        private List<Brid> bridovi;
        private Vrh oznaceniVrh;
        private StringBuilder nazivVrha;
        private int tezina;
        private string smjer;
        private int red;
        private int stupac;

        public Graf(int[,] labirintMatrica)
        {
            this.labirintMatrica = labirintMatrica;
            vrhovi = new List<Vrh>();
            nedovrseniVrhovi = new List<Vrh>();
            bridovi = new List<Brid>();
            nazivVrha = new StringBuilder("A");
            pronadiUlaz();
        }

        private void pronadiUlaz()
        {
            for (int i = 0; i < labirintMatrica.GetLength(0); i++)
            {
                if (labirintMatrica[i, 0] == 1)
                {
                    Vrh vrh = new Vrh();
                    vrh.naziv = nazivVrha.ToString();
                    vrh.ulazIzlaz = '1';
                    vrh.red = i;
                    red = i;
                    vrh.stupac = 0;
                    stupac = 0;
                    vrhovi.Add(vrh);
                    oznaceniVrh = vrh;
                    smjer = "desno";
                    tezina = 0;
                    break;
                }
            }
            pomakniSe();
        }

        private void pomakniSe()
        {
            if (smjer == "desno")
                stupac++;
            else if (smjer == "gore")
                red--;
            else if (smjer == "lijevo")
                stupac--;
            else if (smjer == "dolje")
                red++;

            tezina++;

            if (stupac == labirintMatrica.GetLength(1)-1)
                kreirajIzlaz();
            else
                ispitajPolozaj();
        }

        private void ispitajPolozaj()
        {
            bool vrhPostoji = false;
            if (nedovrseniVrhovi.Count > 0)
            {
                Vrh vrhZaObrisati = null;
                foreach (var vrh in nedovrseniVrhovi)
                {
                    if(vrh.red == red && vrh.stupac == stupac)
                    {
                        vrhPostoji = true;
                        if (smjer == "desno")
                            vrh.lijevo = false;
                        else if (smjer == "gore")
                            vrh.dolje = false;
                        else if (smjer == "lijevo")
                            vrh.desno = false;
                        else if (smjer == "dolje")
                            vrh.gore = false;

                        Brid brid = new Brid();
                        brid.vrh1 = vrh;
                        brid.vrh2 = oznaceniVrh;
                        brid.tezina = tezina;
                        bridovi.Add(brid);
                        tezina = 0;

                        if (!vrh.desno && !vrh.gore && !vrh.lijevo && !vrh.dolje)
                        {
                            vrhZaObrisati = vrh;
                        }
                        break;
                    }
                }

                if (vrhZaObrisati != null)
                    nedovrseniVrhovi.Remove(vrhZaObrisati);

                if (vrhPostoji)
                {
                    provjeriNedovrseneVrhove();
                }
            }


            if (!vrhPostoji)
            {
                bool naprijed = false;
                bool drugaDvaSmjera = false;
                if (smjer == "desno" || smjer == "lijevo")
                {
                    if (smjer == "desno")
                    {
                        if (labirintMatrica[red, stupac + 1] == 1)
                            naprijed = true;
                    }
                    else if (smjer == "lijevo")
                    {
                        if (labirintMatrica[red, stupac - 1] == 1)
                            naprijed = true;
                    }
                    if (labirintMatrica[red + 1, stupac] == 1 || labirintMatrica[red - 1, stupac] == 1)
                        drugaDvaSmjera = true;
                }
                else if (smjer == "gore" || smjer == "dolje")
                {
                    if (smjer == "gore")
                    {
                        if (labirintMatrica[red - 1, stupac] == 1)
                            naprijed = true;
                    }
                    else if (smjer == "dolje")
                    {
                        if (labirintMatrica[red + 1, stupac] == 1)
                            naprijed = true;
                    }
                    if (labirintMatrica[red, stupac + 1] == 1 || labirintMatrica[red, stupac - 1] == 1)
                        drugaDvaSmjera = true;
                }

                if (!drugaDvaSmjera && naprijed)
                    pomakniSe();
                else if ((!drugaDvaSmjera && !naprijed) || drugaDvaSmjera)
                    kreirajVrh();
            }
        }

        private void kreirajVrh()
        {
            Vrh vrh = new Vrh();
            vrh.naziv = dodjeliNazivVrhu();
            vrh.red = red;
            vrh.stupac = stupac;

            Brid brid = new Brid();
            brid.vrh1 = oznaceniVrh;
            brid.vrh2 = vrh;
            brid.tezina = tezina;
            tezina = 0;

            bridovi.Add(brid);

            int brojSmjerova = 0;
            if(smjer == "desno")
            {
                if (labirintMatrica[red, stupac + 1] == 1) //desno
                {
                    vrh.desno = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red - 1, stupac] == 1) //gore
                {
                    vrh.gore = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red + 1, stupac] == 1) //dolje
                {
                    vrh.dolje = true;
                    brojSmjerova++;
                }
            }
            else if (smjer == "gore")
            {
                if (labirintMatrica[red, stupac + 1] == 1) //desno
                {
                    vrh.desno = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red - 1, stupac] == 1) //gore
                {
                    vrh.gore = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red, stupac - 1] == 1) //lijevo
                {
                    vrh.lijevo = true;
                    brojSmjerova++;
                }
            }
            else if (smjer == "lijevo")
            {
                if (labirintMatrica[red - 1, stupac] == 1) //gore
                {
                    vrh.gore = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red, stupac - 1] == 1) //lijevo
                {
                    vrh.lijevo = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red + 1, stupac] == 1) //dolje
                {
                    vrh.dolje = true;
                    brojSmjerova++;
                }
            }
            else if (smjer == "dolje")
            {
                if (labirintMatrica[red, stupac + 1] == 1) //desno
                {
                    vrh.desno = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red, stupac - 1] == 1) //lijevo
                {
                    vrh.lijevo = true;
                    brojSmjerova++;
                }
                if (labirintMatrica[red + 1, stupac] == 1) //dolje
                {
                    vrh.dolje = true;
                    brojSmjerova++;
                }
            }

            vrhovi.Add(vrh);

            if (brojSmjerova >= 2)
                nedovrseniVrhovi.Add(vrh);

            if (brojSmjerova >= 1)
            {
                oznaceniVrh = vrh;
                izaberiPrviSlobodanSmjer();
            }
            else
            {
                provjeriNedovrseneVrhove();
            }
        }

        private void kreirajIzlaz()
        {
            Vrh vrh = new Vrh();
            vrh.naziv = dodjeliNazivVrhu();
            vrh.ulazIzlaz = '2';
            vrh.red = red;
            vrh.stupac = stupac;
            vrhovi.Add(vrh);

            Brid brid = new Brid();
            brid.vrh1 = oznaceniVrh;
            brid.vrh2 = vrh;
            brid.tezina = tezina;
            tezina = 0;
            bridovi.Add(brid);

            provjeriNedovrseneVrhove();
        }

        private void izaberiPrviSlobodanSmjer()
        {
            bool smjerIzabran = false;
            if (oznaceniVrh.desno && !smjerIzabran)
            {
                oznaceniVrh.desno = false;
                smjer = "desno";
                smjerIzabran = true;
            }
            else if (oznaceniVrh.gore && !smjerIzabran)
            {
                oznaceniVrh.gore = false;
                smjer = "gore";
                smjerIzabran = true;
            }
            else if (oznaceniVrh.lijevo && !smjerIzabran)
            {
                oznaceniVrh.lijevo = false;
                smjer = "lijevo";
                smjerIzabran = true;
            }
            else if (oznaceniVrh.dolje && !smjerIzabran)
            {
                oznaceniVrh.dolje = false;
                smjer = "dolje";
                smjerIzabran = true;
            }

            if (oznaceniVrh.nedovrseniVrh)
            {
                oznaceniVrh.nedovrseniVrh = false;
                if (!oznaceniVrh.desno && !oznaceniVrh.gore && !oznaceniVrh.lijevo && !oznaceniVrh.dolje)
                {
                    nedovrseniVrhovi.Remove(oznaceniVrh);
                }
            }

            pomakniSe();
        }

        private void provjeriNedovrseneVrhove()
        {
            if (nedovrseniVrhovi.Count > 0)
            {
                oznaceniVrh = nedovrseniVrhovi.ElementAt(nedovrseniVrhovi.Count - 1);
                red = oznaceniVrh.red;
                stupac = oznaceniVrh.stupac;
                oznaceniVrh.nedovrseniVrh = true;
                izaberiPrviSlobodanSmjer();
            }
            else
                return;
        }

        private string dodjeliNazivVrhu()
        {
            int zadnjiZnak = nazivVrha.Length - 1;
            if (nazivVrha[zadnjiZnak] < 'Z')
            {
                char znak = nazivVrha[zadnjiZnak];
                znak++;
                nazivVrha[zadnjiZnak] = znak;
            }
            else
            {
                bool postojiKombinacija = false;
                for (int i = 0; i <= zadnjiZnak; i++)
                {
                    if (nazivVrha[i] != 'Z')
                    {
                        postojiKombinacija = true;
                        break;
                    }
                }

                if (postojiKombinacija)
                {
                    int brojac = 0;
                    while (nazivVrha[zadnjiZnak - brojac] == 'Z')
                    {
                        brojac++;
                    }
                    char znak = nazivVrha[zadnjiZnak - brojac];
                    znak++;
                    nazivVrha[zadnjiZnak - brojac] = znak;
                    brojac--;
                    while (brojac != -1)
                    {
                        nazivVrha[zadnjiZnak - brojac] = 'A';
                        brojac--;
                    }
                }
                else
                {
                    nazivVrha.Clear();
                    for (int i = 0; i <= zadnjiZnak + 1; i++)
                    {
                        nazivVrha.Append("A");
                    }
                }
            }

            return nazivVrha.ToString();
        }

        public List<Brid> VratiBridove()
        {
            return bridovi;
        }

        public List<Vrh> VratiVrhove()
        {
            return vrhovi;
        }
    }
}
