using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            Console.WriteLine(@"
   _____            _ _ ____              _    
  / ____|          | | |  _ \            | |   
 | (___  _ __   ___| | | |_) | ___   ___ | | __
  \___ \| '_ \ / _ \ | |  _ < / _ \ / _ \| |/ /
  ____) | |_) |  __/ | | |_) | (_) | (_) |   < 
 |_____/| .__/ \___|_|_|____/ \___/ \___/|_|\_\
        | |                                    
        |_|                                    ");
            
            Thread.Sleep(2000);
            Console.Clear();
            Logic();
        }
        private static void Logic()
        {
            Access a = new Access();
            while (true)
            {
                Console.WriteLine("1) Make Spell\n" +
                                  "2) Custom Spell\n" +
                                  "3) Display spellbook\n" +
                                  "4) Remove last entry\n" +
                                  "5) Remove all\n" +
                                  "6) Remove by name\n" +
                                  "7) Exit");
                Console.Write("Option: ");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        string spell = HelperClass.GenerateSpell(ArrayForm, ArrayType);
                        Console.WriteLine("You made: " + spell);
                        int cost = HelperClass.Rnd.Next(0, 100);
                        var sp = new Spell
                        {
                            Name = spell,
                            SpellCost = cost,
                            Dmg = (cost > 50) ? HelperClass.Rnd.Next(50, 100) : HelperClass.Rnd.Next(0, 50),
                            Tier = (cost > 90) ? HelperClass.Tiers[4] : (cost > 70 && cost < 90) ? HelperClass.Tiers[3] : (cost > 50 && cost < 70) ? HelperClass.Tiers[2] : (cost > 20 && cost < 50) ? HelperClass.Tiers[1] : (cost > 0 && cost < 20) ? HelperClass.Tiers[0] : "Exclusive",
                            Quality = HelperClass.Quality[HelperClass.Rnd.Next(0, HelperClass.Quality.Count - 1)]
                        };
                        Console.Write("Add to Local or remote db(L:Local, R:Remote): ");
                        string dbOption = Console.ReadLine();
                        switch (dbOption)
                        {
                            case "L": case "l":
                                Console.WriteLine("Saving to session db!");
                                SpellList.Add(sp);
                                break;
                            case "R": case "r":
                                Console.WriteLine("Saving to remote db!");
                                a.AddToDb(sp);
                                break;
                            default:
                                Console.WriteLine("Invalid option! Saving to Local!");
                                goto case "L";
                        }
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    
                    case "2":
                        Console.Write("Spell Name: ");
                        string spName = Console.ReadLine();
                        Console.Write("Spell Cost: ");
                        int spCost = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Spell Dmg: ");
                        int spDmg = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Spell Tier: ");
                        string spTier = Console.ReadLine();
                        Console.Write("Quality: ");
                        string spQuality = Console.ReadLine();
                        var _sp = new Spell
                        {
                            Name = spName,
                            SpellCost = spCost,
                            Dmg = spDmg,
                            Tier = spTier,
                            Quality = spQuality
                        };
                        Console.Write("Add to Local or Mongo db(L:Local, M:Mongo): ");
                        var _dbOption = Console.ReadLine();
                        switch (_dbOption)
                        {
                            case "L": case "l":
                                Console.WriteLine("Saving to session db!");
                                SpellList.Add(_sp);
                                break;
                            case "M": case "m":
                                Console.WriteLine("Saving to remote db!");
                                a.AddToDb(_sp);
                                break;
                            default:
                                Console.WriteLine("Invalid option! Saving to Local!");
                                goto case "L";
                        }
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();       
                        break;
                    
                    case "3":
                        Console.WriteLine("Do you want to display local or remote db(L:Local, R:Remote)?");
                        var forOption = Console.ReadLine();
                        switch (forOption)
                        {
                            case "L": case "l":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                foreach (var x in SpellList)
                                {
                                    Console.WriteLine($"Name: {x.Name}\nCost: {x.SpellCost}\nDamage: {x.Dmg}\nTier: {x.Tier}\n");
                                }
                                Console.ResetColor();
                                break;
                            case "R": case "r":
                                a.ShowAllInDb();
                                break;
                            default:
                                Console.WriteLine("Invalid option! Showing local book!");
                                goto case "L";
                        }
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    
                    case "4":
                        Console.WriteLine("Do you want to delete last element in local or remote db(L:Local, R:Remote)?");
                        string moz = Console.ReadLine();
                        switch (moz)
                        {
                            case "L": case "l":
                                Console.WriteLine("Removed last item in local db!");
                                SpellList.RemoveAt(SpellList.Count - 1);
                                Console.WriteLine("Press any key to continue.....");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "R": case "r":
                                Console.WriteLine("Deleted last entry in remote db!");
                                a.DeleteLastInDb();
                                Console.WriteLine("Press any key to continue.....");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            default:
                                Console.WriteLine("Invalid option! Please try again!");
                                break;
                        }
                        break;

                    case "5":
                        Console.WriteLine("Do you want to delete all spell from local or remote db(L:Local, R:Remote)?");
                        var deleteOption = Console.ReadLine();
                        switch (deleteOption)
                        {
                            case "L": case "l":
                                Console.WriteLine("Deleted all spells from local db!");
                                SpellList.Clear();
                                break;
                            case "R": case "r":
                                Console.WriteLine("Deleted all spell from remote db!");
                                a.DeleteAllDb();
                                break;
                            default:
                                Console.WriteLine("Invalid option! please try again!");
                                break;
                        }
                        Console.WriteLine("Press any key to continue.....");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    
                    case "6":
                        Console.Write("Enter the name you want to delete: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Do you want to remove from local or remote db(L:Local, R:Remote)?");
                        string op = Console.ReadLine();
                        switch (op)
                        {
                            case "L": case "l":
                                Console.WriteLine($"Removed: {name} from local db!");
                                Spell s = SpellList.First((x) => x.Name == name);
                                SpellList.Remove(s);
                                break;
                            case "R": case "r":
                                Console.WriteLine($"Removed: {name} from remote db!");
                                a.RemoveNameFromDb(name);
                                break;
                            default:
                                Console.WriteLine("Invalid option! Try again!");
                                break;
                        }
                        break;
                    
                    case "7":
                        Console.WriteLine("Exiting!");
                        return;
                    
                    case "debug":
                        Console.WriteLine("Debug ON!");
                        Console.WriteLine(@"
            _________________________
            |                       |
            | 1) Tester             |
            | 2) Tester             |
            |                       |
            |                       |
            |                       |
            |                       |
            -------------------------");
                        Console.Write("Option: ");
                        var debugOption = Console.ReadLine();
                        switch (debugOption)
                        {
                            case "1":
                                a.Tester();
                                break;
                            case "2":
                                a.Tester();       
                                break;
                        }
                        break;

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