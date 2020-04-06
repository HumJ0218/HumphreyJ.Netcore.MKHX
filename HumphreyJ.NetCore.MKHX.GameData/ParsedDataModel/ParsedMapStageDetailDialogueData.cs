using Newtonsoft.Json;
using System;

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
            try
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.WriteLine(JsonConvert.SerializeObject(raw));
                Console.ResetColor();

                throw new ArgumentException($"解析 ID 为 {raw.StoryId} 的剧情数据时出错", ex);
            }
        }
        public override string ToString()
        {
            return NPC;
        }
    }
}