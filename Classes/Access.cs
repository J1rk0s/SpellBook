using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SpellBook.Classes
{
    public class Access
    {
        private IMongoCollection<Spell> _spellBook;

        public Access()
        {
            var client = new MongoClient("mongodb+srv://J1rk0s:KrakenusMaximus@database.5isbe.mongodb.net/Database?retryWrites=true&w=majority");
            var database = client.GetDatabase("DB_Spells");
            _spellBook = database.GetCollection<Spell>("Spells");
            client.ListDatabases();
        }

        public void AddToDb(Spell spell) => _spellBook.InsertOne(spell);
        public void DeleteAllDb() => _spellBook.DeleteMany("{ }");

        public void DeleteLastInDb()
        {
            List<Spell> spBook = _spellBook.Find(new BsonDocument()).ToList();
            var n = spBook.Last();
            var filter = Builders<Spell>.Filter.Eq("_id", n.Id);
            _spellBook.DeleteOne(filter);
        }

        public void ShowAllInDb()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Spell count in mongo: {_spellBook.CountDocuments(new BsonDocument())}\n");
            Console.ResetColor();
            List<Spell> spBook = _spellBook.Find(new BsonDocument()).ToList();
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var x in spBook)
            {
                Console.WriteLine($"Name: {x.Name}\nCost: {x.SpellCost}\nDmg: {x.Dmg}\nTier: {x.Tier}\n");
            }
            Console.ResetColor();
        }

        public void RemoveNameFromDb(string name)
        {
            var filter = Builders<Spell>.Filter.Eq("Name", name);
            _spellBook.DeleteOne(filter);
        }

        public void Tester() => Console.WriteLine("Tester!");
    }
}