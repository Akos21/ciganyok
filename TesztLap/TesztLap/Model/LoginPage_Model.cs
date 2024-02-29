using System;
using System.Collections.Generic;
using TesztLap.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace TesztLap.Model
{
    public class LoginPage_Model
    {
        public List<Felhasznalo> felh = new List<Felhasznalo>();
        public Felhasznalo User = null;

        public LoginPage_Model(TesztLap.Data.Database db)
        {
            felh = db.ReadTable();
        }

        public bool CheckUser(string username, string password)
        {
            if(felh.Where(x => x.Username == username).Count() > 0)
            {
                if(felh.Where(x => x.Username == username).First().Password == password)
                {
                    AssignValue(username);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool CheckUser(Felhasznalo user)
        {
            if (felh.Where(x => x.Username == user.Username).Count() > 0)
            {
                return felh.Where(x => x.Username == user.Username).First() == user;
            }
            return false;
        }
        
        public void AssignValue(string username)
        {
            User = felh.Where(x => x.Username == username).First();
        }
    }
}
