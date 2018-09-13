using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _18_毒云 : SkillAffectType
        {
            protected _18_毒云(string[] AffectValue, string[] AffectValue2) : base(18, AffectValue, AffectValue2) {

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

            public override string AffectTypeName => "毒云";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  使敌方所有卡牌受到{number}点毒伤害，附加中毒Debuff，在其行动结束后受到{number}点毒伤害。
            //  技能必然发动

            private readonly int cardCount; //此处cardCount仅用于模拟计算，并非实际抽卡数量
            private readonly double number;
        }

    }
}