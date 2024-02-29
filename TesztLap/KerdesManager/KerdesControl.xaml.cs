﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KerdesManager
{
    /// <summary>
    /// Interaction logic for KerdesControl.xaml
    /// </summary>
    public partial class KerdesControl : UserControl
    {
        public List<TextBox> Valaszok = new List<TextBox>();
        private List<RadioButton> Helyesek = new List<RadioButton>();
        public int Helyes = 0;
        public TextBox Kerdes;


        public KerdesControl()
        {
            InitializeComponent();

            Kerdes = KerdesBox;

            
            Valaszok.Add(Valasz1);
            Valaszok.Add(Valasz2);
            Valaszok.Add(Valasz3);
            Valaszok.Add(Valasz4);

            Helyesek.Add(Helyes1);
            Helyesek.Add(Helyes2);
            Helyesek.Add(Helyes3);
            Helyesek.Add(Helyes4);

            foreach(RadioButton radio in Helyesek)
            {
                radio.Click += (s, e) =>
                {
                    if (radio.IsChecked.Value)
                    {
                        for(int i = 0; i <Helyesek.Count; i++)
                        {
                            if (Helyesek[i] != radio)
                            {
                                Helyesek[i].IsChecked = false;
                            }
                            else
                            {
                                Helyes = i + 1;
                            }
                        }
                    }
                };
            }
        }

        public void UpdateNum(int num)
        {
            kNum.Content = $"{num}.";
        }
    }
}
