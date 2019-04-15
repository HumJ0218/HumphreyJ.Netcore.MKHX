using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public partial class ParsedCardData
    {
        public int CardId { get; private set; }
        public string CardName { get; private set; }
        public int Cost { get; private set; }
        public int Color { get; private set; }
        public int Race { get; private set; }
        public int Attack { get; private set; }
        public int Wait { get; private set; }
        public int[] Skill { get; private set; }
        public int[] LockSkill1 { get; private set; }
        public int[] LockSkill2 { get; private set; }
        public int ImageId { get; private set; }
        public int FullImageId { get; private set; }
        public int Price { get; private set; }
        public int BaseExp { get; private set; }
        public int Glory { get; private set; }
        public int EvoCost { get; private set; }
        public int RacePacket { get; private set; }
        public int RacePacketRoll { get; private set; }
        public int MaxInDeck { get; private set; }
        public int CanDecompose { get; private set; }
        public int Rank { get; private set; }
        public int Fragment { get; private set; }
        public int ComposePrice { get; private set; }
        public int DecomposeGet { get; private set; }
        public int DungeonsCard { get; private set; }
        public int DungeonsFrag { get; private set; }
        public int FragMaze { get; private set; }
        public int FragRobber { get; private set; }
        public int FragSeniorPacket { get; private set; }
        public int FragMasterPacket { get; private set; }
        public int FragNewYearPacket { get; private set; }
        public int FragMagicCard { get; private set; }
        public int FragRacePacket { get; private set; }
        public int PriceRank { get; private set; }
        public int ActivityPacket { get; private set; }
        public int ActivityPacketRoll { get; private set; }
        public int BossHelper { get; private set; }
        public int FightRank { get; private set; }
        public int FragmentCanUse { get; private set; }
        public int Dust { get; private set; }
        public int DustLevel { get; private set; }
        public int DustNumber { get; private set; }
        public int[] HpArray { get; private set; }
        public int[] AttackArray { get; private set; }
        public int[] ExpArray { get; private set; }

        public ParsedCardData(RawCardData raw)
        {
            try
            {
                CardId = int.Parse(raw.CardId);
                CardName = raw.CardName;
                Cost = int.Parse(raw.Cost);
                Color = int.Parse(raw.Color);
                Race = int.Parse(raw.Race);
                Attack = int.Parse(raw.Attack);
                Wait = int.Parse(raw.Wait);
                Skill = raw.Skill?.Split('_').Select(m => string.IsNullOrEmpty(m) ? -1 : int.Parse(m)).ToArray();
                LockSkill1 = raw.LockSkill1?.Split('_').Select(m => string.IsNullOrEmpty(m) ? -1 : int.Parse(m)).ToArray();
                LockSkill2 = raw.LockSkill2?.Split('_').Select(m => string.IsNullOrEmpty(m) ? -1 : int.Parse(m)).ToArray();
                ImageId = int.Parse(raw.ImageId);
                FullImageId = int.Parse(raw.FullImageId);
                Price = int.Parse(raw.Price);
                BaseExp = int.Parse(raw.BaseExp);
                Glory = int.Parse(raw.Glory);
                EvoCost = int.Parse(raw.EvoCost);
                RacePacket = int.Parse(raw.RacePacket);
                RacePacketRoll = int.Parse(raw.RacePacketRoll);
                MaxInDeck = int.Parse(raw.MaxInDeck);
                CanDecompose = int.Parse(raw.CanDecompose);
                Rank = int.Parse(raw.Rank);
                Fragment = int.Parse(raw.Fragment);
                ComposePrice = int.Parse(raw.ComposePrice);
                DecomposeGet = int.Parse(raw.DecomposeGet);
                DungeonsCard = int.Parse(raw.DungeonsCard);
                DungeonsFrag = int.Parse(raw.DungeonsFrag);
                FragMaze = int.Parse(raw.FragMaze);
                FragRobber = int.Parse(raw.FragRobber);
                FragSeniorPacket = int.Parse(raw.FragSeniorPacket);
                FragMasterPacket = int.Parse(raw.FragMasterPacket);
                FragNewYearPacket = int.Parse(raw.FragNewYearPacket);
                FragMagicCard = int.Parse(raw.FragMagicCard);
                FragRacePacket = int.Parse(raw.FragRacePacket);
                PriceRank = raw.PriceRank == null ? 0 : int.Parse(raw.PriceRank);
                ActivityPacket = raw.ActivityPacket == null ? 0 : int.Parse(raw.ActivityPacket);
                ActivityPacketRoll = raw.ActivityPacketRoll == null ? 0 : int.Parse(raw.ActivityPacketRoll);
                BossHelper = int.Parse(raw.BossHelper);
                FightRank = int.Parse(raw.FightRank);
                FragmentCanUse = int.Parse(raw.FragmentCanUse);
                Dust = raw.Dust == null ? 0 : int.Parse(raw.Dust);
                DustLevel = raw.DustLevel == null ? 0 : int.Parse(raw.DustLevel);
                DustNumber = raw.DustNumber == null ? 0 : int.Parse(raw.DustNumber);
                HpArray = raw.HpArray.Select(m => int.Parse(m)).ToArray();
                AttackArray = raw.AttackArray.Select(m => int.Parse(m)).ToArray();
                ExpArray = raw.ExpArray.Select(m => int.Parse(m == "" ? "0" : m)).ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("解析卡牌数据出错", ex);
            }
        }

        public override string ToString()
        {
            return CardName;
        }
    }
}