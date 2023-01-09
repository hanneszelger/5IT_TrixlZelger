using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIHandler
{
    public class Food
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("barcode")]
        public byte[] Barcode { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("macrosPer100g")]
        public Dictionary<MacroType, double> MacrosPer100g { get; set; }

        [BsonElement("servingSizes")]
        public List<double> ServingSizes { get; set; }

        public Food()
        {

        }

        public Food(string id, byte[] barcode, string name, Dictionary<MacroType, double> macrosPer100g, List<double> servingSizes)
        {
            Id = id;
            Barcode = barcode;
            Name = name;
            MacrosPer100g = macrosPer100g;
            ServingSizes = servingSizes;
        }
    }

    public static class Macros
    {
        public static MacroModel Protein
        {
            get { return new MacroModel(MacroType.Protein, 4); }
        }

        public static MacroModel Carbs
        {
            get { return new MacroModel(MacroType.Carbs, 4); }
        }

        public static MacroModel Fat
        {
            get { return new MacroModel(MacroType.Fat, 9); }
        }
    }

    public class MacroModel
    {
        public MacroType MacroType { get; set; }

        public int CaloriesPerGramm { get; set; }  

        public MacroModel() { }

        public MacroModel(MacroType macroType, int caloriesPerGram)
        {
            MacroType = macroType;
            CaloriesPerGramm = caloriesPerGram;
        }
    }

    public enum MacroType
    {
        Protein,
        Carbs,
        Fat
    }
}
