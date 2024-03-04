using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public string path = "Kerdesek.json";

        public static List<KvizKerdesek> kvizkerdesek = new List<KvizKerdesek>();

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
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("[");

                var tempKerd = kvizkerdesek.Select(x => JsonConvert.SerializeObject(x)).ToList();

                sw.WriteLine(string.Join(",\n", tempKerd));
                sw.WriteLine("]");
            }
        }
    }
}
