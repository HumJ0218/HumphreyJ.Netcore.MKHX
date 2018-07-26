namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedMapStageDetailDialogueData
    {

        public int StoryId { get; private set; }
        public string Did { get; private set; }
        public string NPC { get; private set; }
        public string Dialogue { get; private set; }
        public string Icon { get; private set; }
        public int Orientations { get; private set; }
        public int Opportunity { get; private set; }
        public ParsedMapStageDetailData MapStageDetail { get; private set; }

        public ParsedMapStageDetailDialogueData(RawMapStageDetailDialogueData raw, ParsedMapStageDetailData MapStageDetail)
        {
            StoryId = int.Parse(raw.StoryId);
            Did = raw.Did;
            NPC = raw.NPC;
            Dialogue = raw.Dialogue;
            Icon = raw.Icon;
            Orientations = int.Parse(raw.Orientations);
            Opportunity = int.Parse(raw.Opportunity);
            this.MapStageDetail = MapStageDetail;
        }
        public override string ToString()
        {
            return NPC;
        }
    }
}