using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _12_火球 : SkillAffectType
        {
            protected _12_火球(string[] AffectValue, string[] AffectValue2) : base(12, AffectValue, AffectValue2) {

                {
                    cardCount = 1;
                    number = double.Parse(AffectValue[0]);
                }

                {
                    SkillPercentageAll = 0.5;
                }

                {
                    SkillPercentageMean = 0.5;
                }

            }

            public override string AffectTypeName => "火球";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  使敌方1张卡牌受到{number}~2×{number}点火焰伤害。
            //  完整发动概率为浮动量2-1除以最大量2，也就是0.5
            //  平均分布，所以期望为0.5

            private readonly int cardCount;
            private readonly double number;
        }

    }
}