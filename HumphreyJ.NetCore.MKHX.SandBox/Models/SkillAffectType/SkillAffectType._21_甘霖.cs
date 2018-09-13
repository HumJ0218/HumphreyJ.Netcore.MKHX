using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _21_甘霖 : SkillAffectType
        {
            protected _21_甘霖(string[] AffectValue, string[] AffectValue2) : base(21, AffectValue, AffectValue2) {

                {
                    cardCount = Config.SKILL_CARDCOUNTALL_VIRTUAL;
                    number = double.Parse(AffectValue[0]);
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }

            }

            public override string AffectTypeName => "甘霖";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  恢复我方所有卡牌{number}点生命值。
            //  只要己方存在受伤卡牌，技能必然发动

            private readonly int cardCount; //此处cardCount仅用于模拟计算，并非实际抽卡数量
            private readonly double number;
        }

    }
}