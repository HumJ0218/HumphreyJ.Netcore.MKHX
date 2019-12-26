using Newtonsoft.Json;
using System;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedMapStageDetailData
    {
        public int MapStageDetailId { get; private set; }
        public string Name { get; private set; }
        public int Type { get; private set; }
        public int MapStageId { get; private set; }
        public int Rank { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Prev { get; private set; }
        public int Next { get; private set; }
        public int NextBranch { get; private set; }
        public string FightName { get; private set; }
        public string FightImg { get; private set; }
        public ParsedMapStageDetailDialogueData[] Dialogue { get; private set; }
        public ParsedMapStageDetailDialogueData[] DialogueAfter { get; private set; }
        public ParsedMapStageDetailLevelData[] Levels { get; private set; }
        public ParsedMapStageData MapStage { get; private set; }
        public ParsedMapStageDetailData PrevMapStageDetail => MapStage.MapStageDetails.FirstOrDefault(m => m.MapStageDetailId == Prev);
        public ParsedMapStageDetailData NextMapStageDetail => MapStage.MapStageDetails.FirstOrDefault(m => m.MapStageDetailId == Next);
        public ParsedMapStageDetailData NextBranchMapStageDetail => MapStage.MapStageDetails.FirstOrDefault(m => m.MapStageDetailId == NextBranch);
        public int[] LevelNumber => new int[] { MapStage.Rank + 1, Rank + 1 };

        public ParsedMapStageDetailData(RawMapStageDetailData raw, ParsedMapStageData MapStage)
        {
            try
            {
                MapStageDetailId = int.Parse(raw.MapStageDetailId);
                Name = raw.Name;
                Type = int.Parse(raw.Type);
                MapStageId = int.Parse(raw.MapStageId);
                Rank = int.Parse(raw.Rank);
                X = int.Parse(raw.X);
                Y = int.Parse(raw.Y);
                Prev = int.Parse(raw.Prev);
                Next = int.Parse(raw.Next);
                NextBranch = int.Parse(raw.NextBranch);
                FightName = raw.FightName;
                FightImg = raw.FightImg;
                Dialogue = raw.Dialogue?.Select(m => new ParsedMapStageDetailDialogueData(m, this)).Where(m => m != null).ToArray() ?? new ParsedMapStageDetailDialogueData[] { };
                DialogueAfter = raw.DialogueAfter?.Select(m => new ParsedMapStageDetailDialogueData(m, this)).Where(m => m != null).ToArray() ?? new ParsedMapStageDetailDialogueData[] { };
                Levels = raw.Levels?.Select(m => new ParsedMapStageDetailLevelData(m, this)).Where(m => m != null).ToArray() ?? new ParsedMapStageDetailLevelData[] { };
                this.MapStage = MapStage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(JsonConvert.SerializeObject(raw));
                throw new ArgumentException($"解析 ID 为 {MapStage.MapStageId}-{raw.MapStageDetailId} 的关卡数据时出错", ex);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}