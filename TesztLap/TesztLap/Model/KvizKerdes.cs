﻿using System;
using System.Collections.Generic;

namespace TesztLap.Model
{
    public class KvizKerdes
    {
        public string Kerdes { get; set; }
        public int KerdesNum { get; set; }
        public List<Valasz> Valaszok { get; set; }

        public KvizKerdes(string kerdes, int num, List<string> valaszok, int helyes)
        {
            Kerdes = kerdes;
            KerdesNum = num;
            Valaszok = new List<Valasz>();
            for (int i = 0; i < valaszok.Count; i++)
            {
                Valaszok.Add(new Valasz(valaszok[i], helyes == i));
            }
            Shuffle();
        }

        public void Shuffle()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Valaszok.Count; j++)
                {
                    int temp = rnd.Next(0, Valaszok.Count - 1);
                    Valasz temp2 = Valaszok[j];
                    Valaszok[j] = Valaszok[temp];
                    Valaszok[temp] = temp2;
                }
            }

        }
    }
}
