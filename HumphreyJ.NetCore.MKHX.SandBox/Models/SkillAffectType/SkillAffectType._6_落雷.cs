using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _6_落雷 : SkillAffectType
        {
            protected _6_落雷(string[] AffectValue, string[] AffectValue2) : base(6, AffectValue, AffectValue2) {

                {
                    cardCount = 1;
                    number = double.Parse(AffectValue[0]);
                    percentage = double.Parse(AffectValue2[0]) / 100;
                }

                {
                    SkillPercentageAll = Math.Pow(percentage, cardCount);
                }

                {
                    var p = new List<double>();
                    for (var i = 0; i < cardCount; i++)
                    {
                        p.Add(Math.Pow(percentage, i));
                    }
                    SkillPercentageMean = p.Average();
                }


            }

            public override string AffectTypeName => "落雷";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  使敌方1张卡牌受到{number}点雷电伤害，{percentage}%概率无法物理攻击。 
            //  完整发动概率为卡牌受影响的概率
            //  期望为卡牌受影响的概率

            private readonly int cardCount;
            private readonly double number;
            private readonly double percentage;
        }

    }
}