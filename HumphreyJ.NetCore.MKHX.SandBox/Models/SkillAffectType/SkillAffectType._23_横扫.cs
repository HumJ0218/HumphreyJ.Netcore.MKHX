namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _23_横扫 : SkillAffectType
        {
            public _23_横扫(string[] AffectValue, string[] AffectValue2) : base(23, AffectValue, AffectValue2)
            {
                {
                    count = 3;
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            override public string AffectTypeName => "横扫";

            override public double SkillPercentageAll { get; }

            override public double SkillPercentageMean { get; }

            //  攻击正面卡牌造成伤害后对以正对卡牌为中心的{count-1}其他卡牌造成等额伤害。
            //  先对正面卡牌进行攻击，如果造成有效伤害则使用有效伤害值按场上顺序对两侧卡牌进行普通攻击
            //  横扫的攻击动画需要替换

            private readonly double count;
        }

    }
}