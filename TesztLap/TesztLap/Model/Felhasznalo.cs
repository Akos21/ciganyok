using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TesztLap.Model
{
    public class Felhasznalo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public double Eredmeny { get; set; }

        public Felhasznalo(SQLiteDataReader reader)
        {
            Username = reader.GetString(0);
            Password = reader.GetString(1);
            Eredmeny = reader.GetDouble(2);
        }

        public Felhasznalo(string user, string pass)
        {
            Username = user;
            Password = pass;
            Eredmeny = 0f;
        }

        public Felhasznalo(string user, string pass, double eredmeny)
        {
            Username = user;
            Password = pass;
            Eredmeny = eredmeny;
        }
    }
}
