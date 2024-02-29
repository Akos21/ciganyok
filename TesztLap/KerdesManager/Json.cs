using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerdesManager
{
    public class KvizKerdesek
    {
        public string kerdes { get; set; }
        public IList<string> valaszok { get; set; }
        public int Helyes { get; set; }

        public KvizKerdesek(string kerdes, List<string> valaszok, int helyes)
        {
            this.kerdes = kerdes;
            this.valaszok = valaszok;
            this.Helyes = helyes;
        }
    }

    public class Json
    {
        string path = "Kerdesek.json";

        public List<KvizKerdesek> kvizkerdesek = new List<KvizKerdesek>();

        public Json()
        {
            CreateJson();
        }

        private void CreateJson()
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            else
            {
                File.WriteAllText(path, "");
            }
        }
        
        public void AddKerdes(string kerdes, List<string> valaszok, int helyes)
        {
            kvizkerdesek.Add(new KvizKerdesek(kerdes, valaszok, helyes));
        }

        public void WriteJson()
        {
            using(StreamWriter sw = new StreamWriter(path))
            {
                foreach(KvizKerdesek kerdes in kvizkerdesek)
                {
                    var json = JsonConvert.SerializeObject(kerdes);
                    sw.WriteLine(json);
                }
            }
        }
    }
}
