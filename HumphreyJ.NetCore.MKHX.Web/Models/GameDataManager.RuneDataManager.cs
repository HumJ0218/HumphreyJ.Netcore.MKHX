using HumphreyJ.NetCore.MKHX.GameData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public class RuneDataManager : DataManager
        {
            public RuneDataManager(Guid version)
            {
                Version = version;
            }

            public new RawRuneData[] RawData => GetRawData();
            public ParsedRuneData[] ParsedData => GetParsedData();

            private RawRuneData[] rawData;
            private RawRuneData[] GetRawData()
            {
                if (rawData == null)
                {
                    rawData = RawRuneData.ParseJsonString(base.RawData);
                }
                return rawData;
            }
            private static Dictionary<Guid, ParsedRuneData[]> parsedData = new Dictionary<Guid, ParsedRuneData[]>();
            private ParsedRuneData[] GetParsedData()
            {
                if (!parsedData.ContainsKey(Version))
                {
                    parsedData.Add(Version, RawData.Select(m => new ParsedRuneData(m)).ToArray());
                }
                return parsedData[Version];
            }

            internal ParsedRuneData[] Preload()
            {
                var data = ParsedData;
                {
                    //  GC
                    rawData = null;
                }
                return data;
            }
        }

        private readonly Dictionary<ParsedRuneData, ParsedSkillData[]> RuneData_AllSkills = new Dictionary<ParsedRuneData, ParsedSkillData[]>();

        /// <summary>
        /// 获取符文所有技能
        /// </summary>
        public ParsedSkillData[] RuneData_GetAllSkills(ParsedRuneData rune)
        {
            if (!RuneData_AllSkills.ContainsKey(rune))
            {
                RuneData_InitSkillRelationLists();
            }
            return RuneData_AllSkills[rune];
        }

        /// <summary>
        /// 初始化 符文-技能 关系列表
        /// </summary>
        public void RuneData_InitSkillRelationLists()
        {
            //  1   初始化符文所有技能列表
            foreach (var rune in RuneList)
            {
                var list = new List<ParsedSkillData>();
                list.AddRange(rune.LockSkill1.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                list.AddRange(rune.LockSkill2.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                list.AddRange(rune.LockSkill3.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                list.AddRange(rune.LockSkill4.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                list.AddRange(rune.LockSkill5.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                RuneData_AllSkills.TryAdd(rune, list.ToArray());
            }

        }

    }
}