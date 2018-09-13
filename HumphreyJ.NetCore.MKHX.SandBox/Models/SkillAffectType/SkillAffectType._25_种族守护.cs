namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _25_种族守护 : SkillAffectType
        {
            protected _25_种族守护(string[] AffectValue, string[] AffectValue2) : base(25, AffectValue, AffectValue2)
            {

                {
                    race = int.Parse(AffectValue2[0]);
                    cardId = AffectValue2.Length > 1 ? int.Parse(AffectValue2[1]) : 0;

                    if (AffectValue[0][0] == '%')
                    {
                        percentage = double.Parse(AffectValue[0].Substring(1)) / 100;
                        number = 0;
                    }
                    else
                    {
                        percentage = 1;
                        number = double.Parse(AffectValue[0]);
                    }
                }

                {
                    switch (race)
                    {
                        case 5:
                            {
                                {
                                    SkillPercentageAll = 1;
                                }

                                {
                                    SkillPercentageMean = 1;
                                }
                                break;
                            }
                        case 6:
                            {
                                {
                                    SkillPercentageAll = 1.0 / Config.SKILL_CARDCOUNTALL_VIRTUAL;
                                }

                                {
                                    SkillPercentageMean = 1.0 / Config.SKILL_CARDCOUNTALL_VIRTUAL;
                                }
                                break;
                            }
                        default:
                            {
                                {
                                    SkillPercentageAll = Config.SKILL_CARDRACEPERCENT_VIRTUAL;
                                }

                                {
                                    SkillPercentageMean = Config.SKILL_CARDRACEPERCENT_VIRTUAL;
                                }
                                break;
                            }
                    };
                }
            }

            public override string AffectTypeName => "种族守护";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  增加场上除自身外我方其他{race}种族（5表示所有、6表示由可选参数{cardId}指定编号的卡牌）卡牌{percentage}%+{number}生命值。
            //  主要种族有4个。小概率遇到魔族、魔王、魔神等，此概率暂时忽略

            private readonly int race;
            private readonly int cardId;
            private readonly double percentage;
            private readonly double number;

        }
    }
}