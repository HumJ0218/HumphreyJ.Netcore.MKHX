namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawMapStageDetailLevelData
    {
        public string MapStageDetailId;
        public string Level;
        public string CardList;
        public string RuneList;
        public string HeroLevel;
        public string AchievementId;
        public string EnergyExpend;
        public string BonusWin;
        public string FirstBonusWin;
        public string BonusLose;
        public string AddedBonus;
        public string EnergyExplore;
        public string BonusExplore;
        public string Hint;
        public string BonusChip;
        public string ChipRate;
        public string AchievementText;

        public override string ToString()
        {
            return AchievementText;
        }
    }
}