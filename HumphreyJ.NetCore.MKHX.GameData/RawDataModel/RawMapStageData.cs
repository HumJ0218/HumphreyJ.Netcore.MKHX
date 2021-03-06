﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawMapStageData
    {
        public string MapStageId;
        public string Name;
        public string Count;
        public string EverydayReward;
        public string Rank;
        public string MazeCount;
        public string NeedStar;
        public string Prev;
        public string Next;
        public string FinishAward;
        public string HideChip;
        public string ChipRate;
        public string HpAdd;
        public string AtkAdd;
        public string HerohpAdd;
        public string OpenStatus;
        public RawMapStageDetailData[] MapStageDetails;
        public string EasyEverydayReward;

        public static RawMapStageData[] ParseJsonString(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<RawMapStageData[]>(json);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
                
                throw new ArgumentException("地图原始数据异常", ex);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}