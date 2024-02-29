using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using TesztLap.Model;

namespace TesztLap
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        //private string Username = "Akos";
        //private string Password = "alma123";
        LoginPage_Model mod;
        public Felhasznalo User = null;

        public LoginPage(Window owner, TesztLap.Data.Database db)
        {
            this.Owner = owner;
            mod = new LoginPage_Model(db);
            InitializeComponent();
            userName.Focus();
            loginBtn.Click += (s, e) =>
            {
                if (SuccessFullLogin())
                {
                    MessageBox.Show(this, "Sikeres bejelentkezés!");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "Hibás jelszó vagy felhasználónév!");
                }
            };
        }

        public bool SuccessFullLogin()
        {
            if(mod.CheckUser(userName.Text, password.Password))
            {
                User = mod.User;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string UserName()
        {
            return userName.Text;
        }
    }
}
