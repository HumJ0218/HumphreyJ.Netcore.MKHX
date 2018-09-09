using HumphreyJ.NetCore.MKHX.SandBox.Models;
using HumphreyJ.NetCore.MKHX.SandBox.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.SandBox
{
    class Program
    {
        static Random random = new Random();
        static int index=0;

        static void Main(string[] args)
        {
            Hello();

            var 进攻方玩家 = new Player("进攻方玩家")
                .SetHero(new Hero(random.Next(10000)))
                .SetCards(new object[random.Next(10)].Select(m => new Card(-++index, $"测试卡牌{index}", random.Next(16), random.Next(6), new Skill[] { }, new Skill[] { }, random.Next(6), random.Next(7), random.Next(100), random.Next(2500))).ToArray())
                .SetRunes(new object[random.Next(5)].Select(m => new Rune()).ToArray());

            var 防守方玩家 = new Player("防守方玩家")
                .SetHero(new Hero(random.Next(10000)))
                .SetCards(new object[random.Next(10)].Select(m => new Card(-++index, $"测试卡牌{index}", random.Next(16), random.Next(6), new Skill[] { }, new Skill[] { }, random.Next(6), random.Next(7), random.Next(100), random.Next(2500))).ToArray())
                .SetRunes(new object[random.Next(5)].Select(m => new Rune()).ToArray());

            var battle = new Battle(进攻方玩家, 防守方玩家);

            Console.ReadKey();
        }

        static void Hello() {
            Console.WriteLine("+------------------+");
            Console.WriteLine("| 魔卡幻想对战沙盒 |");
            Console.WriteLine("+------------------+");
        }
    }
}