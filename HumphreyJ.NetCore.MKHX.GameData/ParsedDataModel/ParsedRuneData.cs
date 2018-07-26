using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedRuneData
    {
        public int RuneId { get; private set; }
        public string RuneName { get; private set; }
        public int Property { get; private set; }
        public int Color { get; private set; }
        public int[] LockSkill1 { get; private set; }
        public int[] LockSkill2 { get; private set; }
        public int[] LockSkill3 { get; private set; }
        public int[] LockSkill4 { get; private set; }
        public int[] LockSkill5 { get; private set; }
        public int Price { get; private set; }
        public int SkillTimes { get; private set; }
        public string Condition { get; private set; }
        public int SkillConditionSlide { get; private set; }
        public int SkillConditionType { get; private set; }
        public int SkillConditionRace { get; private set; }
        public int SkillConditionColor { get; private set; }
        public int SkillConditionCompare { get; private set; }
        public int SkillConditionValue { get; private set; }
        public int ThinkGet { get; private set; }
        public int Fragment { get; private set; }
        public int BaseExp { get; private set; }
        public int[] ExpArray { get; private set; }

        public ParsedRuneData(RawRuneData raw)
        {
            RuneId = int.Parse(raw.RuneId);
            RuneName = raw.RuneName;
            Property = int.Parse(raw.Property);
            Color = int.Parse(raw.Color);
            LockSkill1 = raw.LockSkill1.Split('_').Select(m => int.Parse(m)).ToArray();
            LockSkill2 = raw.LockSkill2.Split('_').Select(m => int.Parse(m)).ToArray();
            LockSkill3 = raw.LockSkill3.Split('_').Select(m => int.Parse(m)).ToArray();
            LockSkill4 = raw.LockSkill4.Split('_').Select(m => int.Parse(m)).ToArray();
            LockSkill5 = raw.LockSkill5.Split('_').Select(m => int.Parse(m)).ToArray();
            Price = int.Parse(raw.Price);
            SkillTimes = int.Parse(raw.SkillTimes);
            Condition = raw.Condition;
            SkillConditionSlide = int.Parse(raw.SkillConditionSlide);
            SkillConditionType = int.Parse(raw.SkillConditionType);
            SkillConditionRace = int.Parse(raw.SkillConditionRace);
            SkillConditionColor = int.Parse(raw.SkillConditionColor);
            SkillConditionCompare = int.Parse(raw.SkillConditionCompare);
            SkillConditionValue = int.Parse(raw.SkillConditionValue);
            ThinkGet = int.Parse(raw.ThinkGet);
            Fragment = int.Parse(raw.Fragment);
            BaseExp = int.Parse(raw.BaseExp);
            ExpArray = raw.ExpArray.Select(m => int.Parse(m)).ToArray();
        }

        public override string ToString()
        {
            return RuneName;
        }
    }
}
