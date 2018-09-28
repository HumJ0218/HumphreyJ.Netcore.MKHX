using System;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedMapStageDetailLevelData
    {

        public int MapStageDetailId { get; private set; }
        public int Level { get; private set; }
        public string[] CardList { get; private set; }
        public string[] RuneList { get; private set; }
        public int HeroLevel { get; private set; }
        public int AchievementId { get; private set; }
        public int EnergyExpend { get; private set; }
        public string[] BonusWin { get; private set; }
        public string[] FirstBonusWin { get; private set; }
        public string[] BonusLose { get; private set; }
        public string[] AddedBonus { get; private set; }
        public int EnergyExplore { get; private set; }
        public string[] BonusExplore { get; private set; }
        public int Hint { get; private set; }
        public int? BonusChip { get; private set; }
        public int? ChipRate { get; private set; }
        public string AchievementText { get; private set; }
        public ParsedMapStageDetailData MapStageDetail { get; private set; }
        public int[] LevelNumber => new int[] { MapStageDetail.MapStage.Rank + 1, MapStageDetail.Rank + 1, Level };

        public ParsedMapStageDetailLevelData(RawMapStageDetailLevelData raw, ParsedMapStageDetailData MapStageDetail)
        {
            try
            {
                MapStageDetailId = int.Parse(raw.MapStageDetailId);
                Level = int.Parse(raw.Level);
                CardList = string.IsNullOrEmpty(raw.CardList) ? new string[] { } : raw.CardList.Split(',');
                RuneList = string.IsNullOrEmpty(raw.RuneList) ? new string[] { } : raw.RuneList.Split(',');
                HeroLevel = int.Parse(raw.HeroLevel);
                AchievementId = int.Parse(raw.AchievementId);
                EnergyExpend = int.Parse(raw.EnergyExpend);
                BonusWin = string.IsNullOrEmpty(raw.BonusWin) ? new string[] { } : raw.BonusWin.Split(',');
                FirstBonusWin = string.IsNullOrEmpty(raw.FirstBonusWin) ? new string[] { } : raw.FirstBonusWin.Split(',');
                BonusLose = string.IsNullOrEmpty(raw.BonusLose) ? new string[] { } : raw.BonusLose.Split(',');
                AddedBonus = string.IsNullOrEmpty(raw.AddedBonus) ? new string[] { } : raw.AddedBonus.Split(',');
                EnergyExplore = int.Parse(raw.EnergyExplore);
                BonusExplore = string.IsNullOrEmpty(raw.AddedBonus) ? new string[] { } : raw.BonusExplore.Split(',');
                Hint = int.Parse(raw.Hint);
                BonusChip = string.IsNullOrEmpty(raw.BonusChip) ? (int?)null : int.Parse(raw.BonusChip);
                ChipRate = string.IsNullOrEmpty(raw.ChipRate) ? (int?)null : int.Parse(raw.ChipRate);
                AchievementText = raw.AchievementText;
                this.MapStageDetail = MapStageDetail;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("解析关卡子关数据出错", ex);
            }
        }

        public override string ToString()
        {
            return AchievementText;
        }
    }
}