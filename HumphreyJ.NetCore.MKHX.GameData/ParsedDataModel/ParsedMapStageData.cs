using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedMapStageData
    {
        public int MapStageId { get; private set; }
        public string Name { get; private set; }
        public int Count { get; private set; }
        public int EverydayReward { get; private set; }
        public int Rank { get; private set; }
        public int MazeCount { get; private set; }
        public int NeedStar { get; private set; }
        public int Prev { get; private set; }
        public int Next { get; private set; }
        public string[] FinishAward { get; private set; }
        public int[] HideChip { get; private set; }
        public int? ChipRate { get; private set; }
        public double? HpAdd { get; private set; }
        public double? AtkAdd { get; private set; }
        public double? HerohpAdd { get; private set; }
        public int? OpenStatus { get; private set; }
        public ParsedMapStageDetailData[] MapStageDetails { get; private set; }
        public int? EasyEverydayReward { get; private set; }
        public int[] LevelNumber => new int[] { Rank + 1 };

        public ParsedMapStageData(RawMapStageData raw)
        {
            try
            {
                MapStageId = int.Parse(raw.MapStageId);
                Name = raw.Name;
                Count = int.Parse(raw.Count);
                EverydayReward = int.Parse(raw.EverydayReward);
                Rank = int.Parse(raw.Rank);
                MazeCount = int.Parse(raw.MazeCount);
                NeedStar = int.Parse(raw.NeedStar);
                Prev = int.Parse(raw.Prev);
                Next = int.Parse(raw.Next);
                FinishAward = raw.FinishAward?.Split(',') ?? new string[] { };
                HideChip = raw.HideChip?.Split(',').Select(m => int.Parse(m)).ToArray() ?? new int[] { };
                ChipRate = string.IsNullOrEmpty(raw.ChipRate) ? (int?)null : int.Parse(raw.ChipRate);
                HpAdd = string.IsNullOrEmpty(raw.HpAdd) ? (double?)null : double.Parse(raw.HpAdd);
                AtkAdd = string.IsNullOrEmpty(raw.AtkAdd) ? (double?)null : double.Parse(raw.AtkAdd);
                HerohpAdd = string.IsNullOrEmpty(raw.HerohpAdd) ? (double?)null : double.Parse(raw.HerohpAdd);
                OpenStatus = string.IsNullOrEmpty(raw.OpenStatus) ? (int?)null : int.Parse(raw.OpenStatus);
                MapStageDetails = raw.MapStageDetails.Select(m => new ParsedMapStageDetailData(m, this)).Where(m => m != null).ToArray();
                EasyEverydayReward = string.IsNullOrEmpty(raw.EasyEverydayReward) ? (int?)null : int.Parse(raw.EasyEverydayReward);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("解析地图数据出错", ex);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}