namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _2_吸血 : SkillAffectType
        {
            protected _2_吸血(string[] AffectValue, string[] AffectValue2) : base(1, AffectValue, AffectValue2)
            {
                {
                    percentage = double.Parse(AffectValue[0]);
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            public override string AffectTypeName => "吸血";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  回复造成物理伤害一定百分比的生命值
            //  只要造成伤害，技能必然发动

            private readonly double percentage;
        }

    }
}