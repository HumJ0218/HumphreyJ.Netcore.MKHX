using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawCardData
    {
        public string CardId;
        public string CardName;
        public string Cost;
        public string Color;
        public string Race;
        public string Attack;
        public string Wait;
        public string Skill;
        public string LockSkill1;
        public string LockSkill2;
        public string ImageId;
        public string FullImageId;
        public string Price;
        public string BaseExp;
        public string Glory;
        public string EvoCost;
        public string RacePacket;
        public string RacePacketRoll;
        public string MaxInDeck;
        public string CanDecompose;
        public string Rank;
        public string Fragment;
        public string ComposePrice;
        public string DecomposeGet;
        public string DungeonsCard;
        public string DungeonsFrag;
        public string FragMaze;
        public string FragRobber;
        public string FragSeniorPacket;
        public string FragMasterPacket;
        public string FragNewYearPacket;
        public string FragMagicCard;
        public string FragRacePacket;
        public string PriceRank;
        public string ActivityPacket;
        public string ActivityPacketRoll;
        public string BossHelper;
        public string FightRank;
        public string FragmentCanUse;
        public string Dust;
        public string DustLevel;
        public string DustNumber;
        public string[] HpArray;
        public string[] AttackArray;
        public string[] ExpArray;

        public static RawCardData[] ParseJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RawCardData[]>(json);
        }

        public override string ToString()
        {
            return CardName;
        }
    }
}
