namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawMapStageDetailDialogueData
    {
        public string StoryId;
        public string Did;
        public string NPC;
        public string Dialogue;
        public string Icon;
        public string Orientations;
        public string Opportunity;

        public override string ToString()
        {
            return NPC;
        }
    }
}