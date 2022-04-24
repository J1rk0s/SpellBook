using MongoDB.Bson;

namespace SpellBook.Classes
{
    public class Spell
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int SpellCost { get; set; }
        public int Dmg { get; set; }
        public string Tier { get; set; }
        public string Quality { get; set; }
    }
}