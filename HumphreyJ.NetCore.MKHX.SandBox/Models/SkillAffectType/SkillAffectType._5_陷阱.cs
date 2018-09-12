using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _5_陷阱 : SkillAffectType
        {
            protected _5_陷阱(string[] AffectValue, string[] AffectValue2) : base(5, AffectValue, AffectValue2) {

                {
                    number = double.Parse(AffectValue[0]);
                }

                {
                    SkillPercentageAll = Math.Pow(percentage, number);
                }

                {
                    var p = new List<double>();
                    for (var i = 0; i < number; i++)
                    {
                        p.Add(Math.Pow(percentage, i));
                    }
                    SkillPercentageMean = p.Average();
                }

            }

            public override string AffectTypeName => "陷阱";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  使敌方{number}张卡牌65%概率丧失下一个行动回合
            //  完整发动概率为{number}张卡牌均受影响的概率
            //  期望为1-{number}张卡牌均受影响的概率的平均值

            private readonly double number;
            private readonly double percentage = 0.65;
        }

    }
}