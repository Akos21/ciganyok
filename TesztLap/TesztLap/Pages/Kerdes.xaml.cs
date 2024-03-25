using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using TesztLap.Model;

namespace TesztLap.Pages
{
    /// <summary>
    /// Interaction logic for Kerdes.xaml
    /// </summary>
    public partial class Kerdes : Page
    {
        public KvizKerdes kvizKerdes;
        public List<Button> Valaszok = new List<Button>();
        public int all = 0;
        public bool Helyes = false;
        public bool Answered = false;

        public Kerdes(KvizKerdes kvizKerdesIn, int all)
        {
            InitializeComponent();

            Valaszok.Add(answer1);
            Valaszok.Add(answer2);
            Valaszok.Add(answer3);
            Valaszok.Add(answer4);

            this.all = all;

            kvizKerdes = kvizKerdesIn;
            InitializePage();

            answer1.Click += (s, e) =>
            {
                CheckAnswer(answer1);
            };
            answer2.Click += (s, e) =>
            {
                CheckAnswer(answer2);
            };
            answer3.Click += (s, e) =>
            {
                CheckAnswer(answer3);
            };
            answer4.Click += (s, e) =>
            {
                CheckAnswer(answer4);
            };
        }

        public void InitializePage()
        {
            Helyes = false;
            Answered = false;
            kvizKerdes.Shuffle();
            foreach (var btn in Valaszok)
            {
                btn.Background = Brushes.LightGray;
            }
        }

        public void InitTxt()
        {
            qNum.Text = kvizKerdes.KerdesNum.ToString();
            kerdes.Text = kvizKerdes.Kerdes;
            answer1_txt.Text = kvizKerdes.Valaszok[0].Szoveg;
            answer2_txt.Text = kvizKerdes.Valaszok[1].Szoveg;
            answer3_txt.Text = kvizKerdes.Valaszok[2].Szoveg;
            answer4_txt.Text = kvizKerdes.Valaszok[3].Szoveg;

            teljesitett.Text = kvizKerdes.KerdesNum.ToString();
            osszes.Text = all.ToString();
        }

        private void CheckAnswer(Button button)
        {
            if (!Answered)
            {
                if (kvizKerdes.Valaszok[Valaszok.FindIndex(x => x == button)].Helyes)
                {
                    //MessageBox.Show("Sikeres!");
                    Helyes = true;
                }
                else
                {
                    //MessageBox.Show("Sikertelen");
                }
                for (int i = 0; i < Valaszok.Count; i++)
                {
                    if (kvizKerdes.Valaszok[i].Helyes)
                    {
                        Valaszok[i].Background = Brushes.Lime;
                    }
                    else
                    {
                        Valaszok[i].Background = Brushes.Crimson;
                    }
                }
            }

        }
    }
}
