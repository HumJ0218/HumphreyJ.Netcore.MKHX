using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _17_毒雾 : SkillAffectType
        {
            protected _17_毒雾(string[] AffectValue, string[] AffectValue2) : base(17, AffectValue, AffectValue2) {

                {
                    cardCount = 3;
                    number = double.Parse(AffectValue[0]);
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }

            }

            public override string AffectTypeName => "毒雾";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  使敌方3张卡牌受到{number}点毒伤害，附加中毒Debuff，在其行动结束后受到{number}点毒伤害。
            //  技能必然发动

            private readonly int cardCount;
            private readonly double number;
        }

    }
}