namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _19_格挡 : SkillAffectType
        {
            protected _19_格挡(string[] AffectValue, string[] AffectValue2) : base(19, AffectValue, AffectValue2)
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

            public override string AffectTypeName => "格挡";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  减少{number}点伤害。
            //  只要造成伤害，技能必然发动

            private readonly double number;
        }

    }
}