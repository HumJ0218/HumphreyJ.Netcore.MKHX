using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedSkillData
    {
        private static char[] AbbreviationTrimStart = "".ToCharArray();
        private static char[] AbbreviationTrimEnd = "0123456789".ToCharArray();

        public int SkillId { get; private set; }
        public string Name { get; private set; }
        public int[] Type { get; private set; }
        public int LanchType { get; private set; }
        public int LanchCondition { get; private set; }
        public string[] LanchConditionValue { get; private set; }
        public int AffectType { get; private set; }
        public string[] AffectValue { get; private set; }
        public string[] AffectValue2 { get; private set; }
        public int SkillCategory { get; private set; }
        public string Desc { get; private set; }
        public string DESCRIBE_NEW { get; private set; }
        public string DESCRIBE_EXTRA { get; private set; }

        public string Abbreviation { get; private set; }
        public bool IsBattleSkill { get; private set; }
        public bool IsSummonSkill { get; private set; }
        public bool IsAwakenSkill { get; private set; }
        public bool IsMultipleSkill { get; private set; }

        public ParsedSkillData(RawSkillData raw)
        {
            try
            {
                this.SkillId = int.Parse(raw.SkillId);
                this.Name = raw.Name;
                this.Type = string.IsNullOrEmpty(raw.Type) ? new int[] { } : raw.Type.Split('_').Select(m => int.Parse(m)).ToArray();
                this.LanchType = int.Parse(raw.LanchType);
                this.LanchCondition = int.Parse(raw.LanchCondition);
                this.LanchConditionValue = raw.LanchConditionValue.Split('_');
                this.AffectType = int.Parse(raw.AffectType);
                this.AffectValue = raw.AffectValue.Split('_');
                this.AffectValue2 = raw.AffectValue2.Split('_');
                this.SkillCategory = int.Parse(raw.SkillCategory);
                this.Desc = raw.Desc;
                this.DESCRIBE_NEW = raw.DESCRIBE_NEW == null ? "" : ParseDescribeNew(raw.DESCRIBE_NEW);
                this.DESCRIBE_EXTRA = raw.DESCRIBE_EXTRA ?? "";

                this.IsBattleSkill = !(AffectType == 55 && AffectValue.Length > 0 && int.Parse(AffectValue[0]) == 0);
                this.IsSummonSkill = AffectType == 101 || AffectType == 180 || AffectType == 154 || AffectType == 184 || AffectType == 193;
                this.IsAwakenSkill = AffectType == 99 || AffectType == 158;
                this.IsMultipleSkill = AffectType == 122;

                this.Abbreviation = IsSummonSkill ? "召唤" : IsAwakenSkill ? "觉醒" : Name.Split(']').Last().TrimStart(AbbreviationTrimStart).TrimEnd(AbbreviationTrimEnd).Replace("MAX", "").Replace(":", "：");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"解析 ID 为 {raw.SkillId} 的技能数据时出错", ex);
            }
        }

        private string ParseDescribeNew(string dESCRIBE_NEW)
        {
            return dESCRIBE_NEW.Replace("href=\"card:", "href=\"/carddata/detail/").Replace("href=\"rune:", "href=\"/runedata/detail/").Replace("href=\"skill:", "href=\"/skilldata/fromkw/");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}