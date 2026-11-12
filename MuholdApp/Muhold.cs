using System;
using System.Collections.Generic;
using System.Text;

namespace MuholdApp
{
    internal class Muhold
    {
        public string Azonosito { get; set; }
        protected int sulyKg { get; set; }
        public int SulyKg
        {
            get { return sulyKg; }

            set
            {
                if (value <= 0)
                {
                    value = 100;
                }
                sulyKg = value;
            }
        }
        private static int osszesMuholdSzama;
        static int OszesMuholdSzama { get; }

        public Muhold(string azonosito, int sulyKg, int oszesMuholdSzama)
        {
            Azonosito = azonosito;
            SulyKg = sulyKg;
            osszesMuholdSzama++;
        }

        public virtual int NapiKoltseg()
        {
            return SulyKg * 5;
        }

        public override string ToString()
        {
            return $"Műhold {Azonosito} | Tömeg: {SulyKg} kg | Napi költség: {NapiKoltseg()} EUR";
        }
    }
}
