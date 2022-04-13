using System;
using System.Collections.Generic;
using SpellBook.Classes;

namespace SpellBook
{
    internal static class Program
    {
        private static readonly List<Spell> SpellList = new List<Spell>();
        private static readonly string[] ArrayForm = HelperClass.ArrayFromText("../../txt/SpellForm.txt");
        private static readonly string[] ArrayType = HelperClass.ArrayFromText("../../txt/SpellType.txt");
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Spell Generator");
            Menu();
        }
        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine("1) Add Spell\n" +
                                  "2) Display spellbook\n" +
                                  "3) Remove last entry\n" +
                                  "4) Remove all\n" +
                                  "5) Exit");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        var spell = HelperClass.GenerateSpell(ArrayForm, ArrayType);
                        Console.WriteLine("You made: " + spell);
                        var cost = HelperClass.Rnd.Next(0, 100);
                        var sp = new Spell
                        {
                            Name = spell,
                            SpellCost = cost,
                            Dmg = (cost > 50) ? HelperClass.Rnd.Next(50, 100) : HelperClass.Rnd.Next(0, 50)
                        };
                        SpellList.Add(sp);
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        foreach (var x in SpellList)
                        {
                            Console.WriteLine($"Name: {x.Name}\nCost: {x.SpellCost}\nDamage: {x.Dmg}\n");
                        }
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine("Removed last item!");
                        SpellList.RemoveAt(SpellList.Count - 1);
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("Deleted all spells!");
                        SpellList.Clear();
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine("Exiting!");
                        return;
                    default:
                        Console.WriteLine("Invalid Option! Try again\nPress any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}