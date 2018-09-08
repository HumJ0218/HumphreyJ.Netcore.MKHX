using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.Enum
{
    /// <summary>
    /// 卡牌种族
    /// </summary>
    internal class CardRace : IComparable
    {
        internal readonly int Id;
        internal readonly string Name;

        private CardRace(int id, string name) { Id = id; Name = name; }

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((CardRace)obj).Id);
        }

        public override bool Equals(object obj)
        {
            return Id == ((CardRace)obj).Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"[{Id}]{Name}";
        }

        internal static CardRace 未定义 = new CardRace(0, nameof(未定义));
        internal static CardRace 王国 = new CardRace(1, nameof(王国));
        internal static CardRace 森林 = new CardRace(2, nameof(森林));
        internal static CardRace 蛮荒 = new CardRace(3, nameof(蛮荒));
        internal static CardRace 地狱 = new CardRace(4, nameof(地狱));
        internal static CardRace 魔族 = new CardRace(5, nameof(魔族));
        internal static CardRace 魔王 = new CardRace(97, nameof(魔王));
        internal static CardRace 万能卡牌 = new CardRace(98, nameof(万能卡牌));
        internal static CardRace 道具 = new CardRace(99, nameof(道具));
        internal static CardRace 魔神 = new CardRace(100, nameof(魔神));

        public static implicit operator CardRace(int race)
        {
            switch (race)
            {
                case 0: return 未定义;
                case 1: return 王国;
                case 2: return 森林;
                case 3: return 蛮荒;
                case 4: return 地狱;
                case 5: return 魔族;
                case 97: return 魔王;
                case 98: return 万能卡牌;
                case 99: return 道具;
                case 100: return 魔神;
                default: throw new KeyNotFoundException(nameof(race));
            }
        }
    }
}