using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _14_烈焰风暴 : SkillAffectType
        {
            protected _14_烈焰风暴(string[] AffectValue, string[] AffectValue2) : base(14, AffectValue, AffectValue2) {

                {
                    cardCount = Config.SKILL_CARDCOUNTALL_VIRTUAL;
                    number = double.Parse(AffectValue[0]);
                }

                {
                    SkillPercentageAll = 0.5;
                }

                {
                    SkillPercentageMean = 0.5;
                }

            }

            public override string AffectTypeName => "烈焰风暴";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  使敌方所有卡牌受到{number}~2×{number}点火焰伤害。
            //  完整发动概率为浮动量2-1除以最大量2，也就是0.5
            //  平均分布，所以期望为0.5

            private readonly int cardCount; //此处cardCount仅用于模拟计算，并非实际抽卡数量
            private readonly double number;
        }

    }
}