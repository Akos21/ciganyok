namespace TesztLap.Model
{
    public class Valasz
    {
        public string Szoveg { get; set; }
        public bool Helyes { get; set; }

        public Valasz(string txt, bool correct)
        {
            Szoveg = txt;
            Helyes = correct;
        }

    }
}
