using HumphreyJ.NetCore.MKHX.Web.Models;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedBonusData
    {
        private readonly GameDataManager GDM;

        public string Type { get; private set; }
        public string Value { get; private set; }
        public string Count { get; private set; }

        public ParsedBonusData(string raw, GameDataManager GDM)
        {
           this. GDM = GDM;

            var a = raw.Split('_');
            this.Type = a[0];
            this.Value = a[1];
            this.Count = a.Length > 2 ? a[2] : "1";
        }

        public static ParsedBonusData Parse(string raw, GameDataManager GDM)
        {
            return string.IsNullOrEmpty(raw) || raw == "0" ? null : new ParsedBonusData(raw, GDM);
        }

        public override string ToString()
        {
            string s;
            switch (Type.ToLower())
            {
                case "card": { s = "卡牌:" + GDM.CardList.FirstOrDefault(m => m.CardId + "" == Value || m.CardName == Value); break; }
                case "chip": { s = "碎片:" + GDM.CardList.FirstOrDefault(m => m.CardId + "" == Value || m.CardName == Value); break; }
                case "rune": { s = "符文:" + GDM.RuneList.FirstOrDefault(m => m.RuneId + "" == Value || m.RuneName == Value); break; }
                case "exp": { s = "经验:" + Value; break; }
                case "coins": { s = "金币:" + Value; break; }
                default: { s = Type; break; }
            }
            return s;
        }

        public bool Equals(ParsedCardData Card)
        {
            var t = Type.ToLower();
            return (t == "card" || t == "chip") && Value == Card.CardId.ToString();
        }
        public bool Equals(ParsedCardData Card, out bool isChip)
        {
            var t = Type.ToLower();
            isChip = t == "chip";
            return (t == "card" || t == "chip") && Value == Card.CardId.ToString();
        }
        public bool Equals(ParsedRuneData Rune)
        {
            var t = Type.ToLower();
            return (t == "rune") && Value == Rune.RuneId.ToString();
        }
    }
}