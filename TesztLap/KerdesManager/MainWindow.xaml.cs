using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in Kerdesek.Children)
            {
                ((KerdesControl)item).Remove.Click += (s, e) =>
                {
                    RemoveElem((KerdesControl)item);
                };

                ((KerdesControl)item).Add.Click += (s, e) =>
                {
                    AddElem();
                };
            }
            RefreshKNums();

            GenerateBtn.Click += (s, e) => Generate();
        }

        private void RemoveElem(KerdesControl cont)
        {
            Kerdesek.Children.Remove(cont);
            RefreshKNums() ;
        }
        private void AddElem()
        {
            KerdesControl toAdd = new KerdesControl();
            toAdd.Remove.Click += (ss, se) =>
            {
                RemoveElem(toAdd);
            };
            toAdd.Add.Click += (ss, se) =>
            {
                AddElem();
            };
            Kerdesek.Children.Add(toAdd);
            RefreshKNums();
        }

        private void RefreshKNums()
        {
            for(int i = 0; i <  Kerdesek.Children.Count; i++)
            {
                ((KerdesControl)Kerdesek.Children[i]).UpdateNum(i + 1);
                if(i == 0)
                {
                    ((KerdesControl)Kerdesek.Children[i]).Remove.IsEnabled = false;
                }
                else
                {
                    ((KerdesControl)Kerdesek.Children[i]).Remove.IsEnabled = true;
                }
            }
        }

        private bool CheckGenerate()
        {
            foreach(var item in Kerdesek.Children)
            {
                KerdesControl cnt = (KerdesControl)item;
                if(cnt.Helyes == 0)
                {
                    MessageBox.Show($"Hiba az {cnt.kNum.Content} kérdésnél: Jelöld ki a helyes választ!");
                    return false;
                }
                if(cnt.Kerdes.Text.Length == 0)
                {
                    MessageBox.Show($"Hiba az {cnt.kNum.Content} kérdésnél: Nem írtál kérdést!");
                    return false;
                }
                for(int i = 0; i < cnt.Valaszok.Count; i++)
                {
                    if (cnt.Valaszok[i].Text.Length == 0)
                    {
                        MessageBox.Show($"Hiba az {cnt.kNum.Content} kérdésnél: Nem írtál {i + 1}. választ!");
                        return false;
                    }
                }
            }
            return true;
        }

        private void Generate()
        {
            if (!CheckGenerate())
            {
                return;
            }
            Json json = new Json();
            foreach(var item in Kerdesek.Children)
            {
                KerdesControl cnt = (KerdesControl)item;
                string kerdes = cnt.KerdesBox.Text;
                List<string> valaszok = cnt.Valaszok.Select(x => x.Text).ToList();
                int helyes = cnt.Helyes;

                json.AddKerdes(kerdes, valaszok, helyes);
            }
            json.WriteJson();
            MessageBox.Show($"Sikeres generálás a következő úton:\n{System.IO.Path.Combine(System.Environment.CurrentDirectory, json.path)}");
        }
    }
}
