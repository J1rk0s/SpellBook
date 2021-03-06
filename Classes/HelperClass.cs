using System;
using System.IO;

namespace SpellBook.Classes
{
    public class HelperClass
    {
        public static readonly Random Rnd = new Random();

        public static string[] ArrayFromText(string path)
        {
            try
            {
                string content = File.ReadAllText(path);
                return content.Split(Convert.ToChar(";"));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured!\nException: {e.Message}");
                return null;
            }
        }

        public static string GenerateSpell(string[] spellform, string[] spelltype)
        {
            if (spellform == null || spelltype == null)
            {
                return null;
            }
            string sform = spellform[Rnd.Next(0, spellform.Length - 1)];
            string stype = spelltype[Rnd.Next(0, spelltype.Length - 1)];
            return stype + " " + sform;
        }
    }
}