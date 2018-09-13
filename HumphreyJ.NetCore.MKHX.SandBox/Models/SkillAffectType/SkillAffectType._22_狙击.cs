namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _22_狙击 : SkillAffectType
        {
            protected _22_狙击(string[] AffectValue, string[] AffectValue2) : base(22, AffectValue, AffectValue2)
            {
                {
                    number1 = double.Parse(AffectValue[0]);
                    number2 = double.Parse(AffectValue2[0]);
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            public override string AffectTypeName => "狙击";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  给予敌方生命值最低的{number2}张卡牌{number1}点狙击伤害。
            //  技能必然发动

            private readonly double number1;
            private readonly double number2;

        }

    }
}