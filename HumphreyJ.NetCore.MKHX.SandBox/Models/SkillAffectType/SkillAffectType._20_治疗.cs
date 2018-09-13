namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _20_治疗 : SkillAffectType
        {
            protected _20_治疗(string[] AffectValue, string[] AffectValue2) : base(20, AffectValue, AffectValue2)
            {
                {
                    number = double.Parse(AffectValue[0]);
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            public override string AffectTypeName => "治疗";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  恢复我方1张损失生命值最多的卡牌{number}点生命值。
            //  只要己方存在受伤卡牌，技能必然发动

            private readonly double number;
        }

    }
}