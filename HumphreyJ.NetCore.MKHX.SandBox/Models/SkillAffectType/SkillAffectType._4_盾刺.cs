namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _4_盾刺 : SkillAffectType
        {
            protected _4_盾刺(string[] AffectValue, string[] AffectValue2) : base(4, AffectValue, AffectValue2)
            {
                {
                    number = double.Parse(AffectValue[0]);
                    percentage = double.Parse(AffectValue2[0]) / 100;
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            public override string AffectTypeName => "盾刺";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  承受物理伤害后对物理攻击实施者和其两侧卡牌造成一定量反击伤害
            //  只要造成伤害，技能必然发动

            private readonly double number;
            private readonly double percentage;
        }

    }
}