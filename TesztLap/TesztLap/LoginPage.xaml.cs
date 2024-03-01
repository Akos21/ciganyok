using System.Windows;
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
            if (mod.CheckUser(userName.Text, password.Password))
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
