using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TesztLap.Model;

namespace TesztLap.Data
{
    internal class KvizKerdesJson
    {
        public string kerdes { get; set; }
        public IList<string> valaszok { get; set; }
        public int Helyes { get; set; }
    }


    public class Json
    {
        string path = "../../Data/Kerdesek.json";

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
        }

        public List<KvizKerdes> LoadJson()
        {
            List<KvizKerdes> Kerdesek = new List<KvizKerdes>();
            List<KvizKerdesJson> _kerdesIn = new List<KvizKerdesJson>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                _kerdesIn = JsonConvert.DeserializeObject<List<KvizKerdesJson>>(json);
            }

            int num = 1;

            foreach (var k in _kerdesIn)
            {
                Kerdesek.Add(
                    new KvizKerdes(k.kerdes, num, k.valaszok.ToList(), k.Helyes - 1)
                    );
                num++;
            }

            return Kerdesek;
        }
    }
}
