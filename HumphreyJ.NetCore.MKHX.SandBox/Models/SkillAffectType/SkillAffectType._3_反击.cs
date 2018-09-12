namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _3_反击 : SkillAffectType
        {
            protected _3_反击(string[] AffectValue, string[] AffectValue2) : base(3, AffectValue, AffectValue2)
            {
                {
                    number = double.Parse(AffectValue[0]);
                    percentage = double.Parse(AffectValue2[0]);
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            public override string AffectTypeName => "反击";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  承受物理伤害后对物理攻击实施者造成一定量反击伤害
            //  只要造成伤害，技能必然发动

            private readonly double number;
            private readonly double percentage;

        }

    }
}