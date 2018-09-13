namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _15_血炼 : SkillAffectType
        {
            protected _15_血炼(string[] AffectValue, string[] AffectValue2) : base(15, AffectValue, AffectValue2)
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

            public override string AffectTypeName => "血炼";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  使敌方1张卡牌受到{number}点血伤害，并且使自身恢复{number}点生命值。
            //  只要造成伤害，技能必然发动

            private readonly double number;
        }

    }
}