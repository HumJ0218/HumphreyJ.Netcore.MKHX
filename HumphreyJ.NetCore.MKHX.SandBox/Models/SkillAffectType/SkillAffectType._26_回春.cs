namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _26_回春 : SkillAffectType
        {
            protected _26_回春(string[] AffectValue, string[] AffectValue2) : base(26, AffectValue, AffectValue2)
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

            public override string AffectTypeName => "回春";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  恢复自身{number}点生命值。 
            //  只要生命值不满，技能必然发动

            private readonly double number;
        }

    }
}