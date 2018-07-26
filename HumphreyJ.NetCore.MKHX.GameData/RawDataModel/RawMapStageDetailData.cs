namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawMapStageDetailData
    {
        public string MapStageDetailId;
        public string Name;
        public string Type;
        public string MapStageId;
        public string Rank;
        public string X;
        public string Y;
        public string Prev;
        public string Next;
        public string NextBranch;
        public string FightName;
        public string FightImg;
        public RawMapStageDetailDialogueData[] Dialogue;
        public RawMapStageDetailDialogueData[] DialogueAfter;
        public RawMapStageDetailLevelData[] Levels;

        public override string ToString()
        {
            return Name;
        }
    }
}