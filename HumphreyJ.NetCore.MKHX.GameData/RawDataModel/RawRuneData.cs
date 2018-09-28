using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawRuneData
    {
        public string RuneId;
        public string RuneName;
        public string Property;
        public string Color;
        public string LockSkill1;
        public string LockSkill2;
        public string LockSkill3;
        public string LockSkill4;
        public string LockSkill5;
        public string Price;
        public string SkillTimes;
        public string Condition;
        public string SkillConditionSlide;
        public string SkillConditionType;
        public string SkillConditionRace;
        public string SkillConditionColor;
        public string SkillConditionCompare;
        public string SkillConditionValue;
        public string ThinkGet;
        public string Fragment;
        public string BaseExp;
        public string[] ExpArray;

        public static RawRuneData[] ParseJsonString(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<RawRuneData[]>(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("符文原始数据异常", ex);
            }
        }

        public override string ToString()
        {
            return RuneName;
        }
    }
}