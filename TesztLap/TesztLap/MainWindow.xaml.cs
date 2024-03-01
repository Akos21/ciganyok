using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TesztLap.Data;
using TesztLap.Model;
using TesztLap.Pages;

namespace TesztLap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database db = new Database();
        private bool LoggedIn = false;
        private List<Kerdes> KerdesPages = new List<Kerdes>();
        public int step = 0;
        private int sikeres = 0;
        private int all = 3;
        private bool Answered = false;
        private readonly int delay = 2000;
        private Felhasznalo User = null;

        public MainWindow()
        {
            InitializeComponent();

            Json jsonData = new Json();
            List<KvizKerdes> Kerdesek = jsonData.LoadJson();
            this.all = Kerdesek.Count;

            foreach (KvizKerdes kerdes in Kerdesek)
            {
                KerdesPages.Add(new Kerdes(kerdes, all));
            }

            /*
            KerdesPages.Add(new Kerdes(
                        new Model.KvizKerdes(
                            "Hogy vagy?",
                            KerdesPages.Count + 1,
                            new List<string>
                            {
                                "Válasz 1",
                                "Válasz 2",
                                "Helyes",
                                "Válasz 4"
                            }, 2), all
                        ));
            KerdesPages.Add(new Kerdes(
                        new Model.KvizKerdes(
                            "Milyen a napod?",
                            KerdesPages.Count + 1,
                            new List<string>
                            {
                                "Válasz 1",
                                "Helyes",
                                "Válasz 3",
                                "Válasz 4"
                            }, 1), all
                        ));
            KerdesPages.Add(new Kerdes(
                        new Model.KvizKerdes(
                            "Milyen színű a valószínű?",
                            KerdesPages.Count + 1,
                            new List<string>
                            {
                                "Válasz 1",
                                "Válasz 2",
                                "Válasz 3",
                                "Helyes"
                            }, 3), all
                        ));

            */


            InitializePage();

            loginBtn.Click += (s, e) => LoginHandler();

        }

        private async void SwitchFrame()
        {
            if (step > 0 && step <= all)
            {
                if (KerdesPages[step - 1].Helyes)
                {
                    sikeres++;
                }
            }
            if (step < all)
            {
                await Task.Delay(delay);
                Kerdes kerdes = KerdesPages[step];

                foreach (Button item in kerdes.Valaszok)
                {
                    item.Click += (ss, se) => MoveForward(kerdes);
                }

                frame.Navigate(kerdes);
            }
            else
            {
                await Task.Delay(delay);
                Eredmeny eredmenyPage = new Eredmeny(sikeres, all);
                eredmenyPage.exit.Click += (s, e) =>
                {
                    this.Close();
                };
                eredmenyPage.tryAgain.Click += (s, e) =>
                {
                    InitializePage();
                };

                frame.Navigate(eredmenyPage);
                if (Convert.ToDouble(percent.Content) < eredmenyPage.Szazalek)
                {
                    percent.Content = eredmenyPage.Szazalek.ToString();
                    User.Eredmeny = eredmenyPage.Szazalek;
                    db.EditEredmeny(User.Username, User.Eredmeny);
                }
            }
            Answered = false;
        }

        private void InitializePage()
        {
            step = 0;
            sikeres = 0;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            KerdesPages = KerdesPages.OrderBy(x => rnd.Next()).ToList();
            foreach (var page in KerdesPages)
            {
                page.InitializePage();
            }
            for (int i = 0; i < KerdesPages.Count; i++)
            {
                KerdesPages[i].kvizKerdes.KerdesNum = i + 1;
            }
            foreach (var page in KerdesPages)
            {
                page.InitTxt();
            }
            StartPage start = new StartPage();
            start.teszt1.MouseLeftButtonDown += (s, e) =>
            {
                if (!LoggedIn)
                {
                    LoginHandler();
                }
                else
                {
                    Kerdes kerdes = KerdesPages[step];

                    foreach (Button item in kerdes.Valaszok)
                    {
                        item.Click += (ss, se) => MoveForward(kerdes);
                    }
                    frame.Navigate(kerdes);
                }
            };
            frame.Navigate(start);
        }

        private void LoginHandler()
        {
            LoginPage login = new LoginPage(this, db);
            login.ShowDialog();
            if (login.SuccessFullLogin())
            {
                loginBtn.Visibility = Visibility.Hidden;
                data.Visibility = Visibility.Visible;
                User = login.User;
                percent.Content = User.Eredmeny.ToString();
                username.Content = User.Username;
                LoggedIn = true;
            }
            login.Close();
        }

        private void MoveForward(Kerdes kerdes)
        {
            if (!kerdes.Answered && !Answered)
            {
                step++;
                SwitchFrame();
                Answered = true;
            }
        }
    }
}
