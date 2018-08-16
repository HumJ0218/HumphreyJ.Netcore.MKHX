using HumphreyJ.NetCore.MKHX.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    /// <summary>
    /// 卡牌评分
    /// </summary>
    public class DataBenchmark
    {
        /// <summary>
        /// 精确度
        /// </summary>
        const int Round = 2;

        /// <summary>
        /// 进攻能力基准值
        /// </summary>
        const double AttackBenchmark = 1500;

        /// <summary>
        /// 防御能力基准值
        /// </summary>
        const double DefendBenchmark = 3000;

        /// <summary>
        /// 防御能力魔族加分
        /// </summary>
        const double DefendBenchmark_Race5Buff = 0.15;

        /// <summary>
        /// 机动性基准值
        /// </summary>
        const double MobilityBenchmark = 8;

        /// <summary>
        /// 稳定性基准值
        /// </summary>
        const double StabilityBenchmark = 3;

        /// <summary>
        /// 开销基准值
        /// </summary>
        const double CostBenchmark = 40;

        /// <summary>
        /// 进攻能力，由ATK、攻击技能确定
        /// </summary>
        public double Attack { get; private set; }

        /// <summary>
        /// 生存能力，由HP、回复技能、防御技能、控制技能确定
        /// </summary>
        public double Defend { get; private set; }

        /// <summary>
        /// 机动性，由等待时间和转生及类似技能、逃跑等技能确定
        /// </summary>
        public double Mobility { get; private set; }

        /// <summary>
        /// 稳定性，概率性技能越少稳定性越高
        /// </summary>
        public double Stability { get; private set; }

        /// <summary>
        /// 开销，卡牌COST
        /// </summary>
        public double Cost { get; private set; }

        /// <summary>
        /// 获取指定卡牌的评分
        /// </summary>
        /// <param name="Card">卡牌数据</param>
        public static DataBenchmark CardBenchmark(ParsedCardData Card)
        {
            var MAX = Math.Pow(10, Round);

            double Restrict(double min, double value, double max)
            {
                var r = value > min ? value : min;
                return r < max ? r : max;
            }

            double
                Attack = (Math.Round(Card.AttackArray[10] / AttackBenchmark, Round)) * MAX,
                Defend = (Math.Round(Card.HpArray[10] / DefendBenchmark, Round) + (Card.Race == 5 || Card.Race == 97 || Card.Race == 99 || Card.Race == 100 ? DefendBenchmark_Race5Buff : 0)) * MAX,
                Mobility = (Math.Round((MobilityBenchmark - Card.Wait) / MobilityBenchmark, Round)) * MAX,
                Stability = (Math.Round(StabilityBenchmark * 0.5 / StabilityBenchmark, Round)) * MAX,
                Cost = (Math.Round((CostBenchmark - Card.Cost) / CostBenchmark, Round)) * MAX;

            return new DataBenchmark
            {
                Attack = Restrict(0, Attack, MAX),
                Defend = Restrict(0, Defend, MAX),
                Mobility = Restrict(0, Mobility, MAX),
                Stability = Restrict(0, Stability, MAX),
                Cost = Restrict(0, Cost, MAX),
            };
        }
    }
}