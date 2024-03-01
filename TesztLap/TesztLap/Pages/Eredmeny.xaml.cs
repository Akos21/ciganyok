using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace TesztLap.Pages
{
    /// <summary>
    /// Interaction logic for Eredmeny.xaml
    /// </summary>
    public partial class Eredmeny : Page
    {
        private double Passing = 60f;
        public double Szazalek = 0f;

        public Eredmeny(int sikeres, int all)
        {
            InitializeComponent();
            teljesitett.Text = sikeres.ToString();
            osszes.Text = all.ToString();


            if (all > 0)
            {
                double szazalek = Math.Round((float)sikeres / all, 4) * 100;
                percentage.Text = szazalek.ToString();
                Szazalek = szazalek;
                if (szazalek >= Passing)
                {
                    eredmeny.Text = "Sikeres!";
                    eredmeny.Background = Brushes.Lime;
                }
                else
                {
                    eredmeny.Text = "Sikertelen";
                    eredmeny.Background = Brushes.Red;
                }
            }
            else
            {
                percentage.Text = "0";
                Szazalek = 0f;
            }
        }

    }
}
