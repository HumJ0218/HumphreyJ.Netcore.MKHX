using HumphreyJ.NetCore.MKHX.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public class EnumAccesser
    {
        private static MkhxCoreContext dbContext = new MkhxCoreContext();

        private static readonly Dictionary<string, Dictionary<int, Enum>> Enum = dbContext.Enum.GroupBy(m => m.Type).ToDictionary(m => m.Key, m => m.ToDictionary(n => n.Key, n => n));

        public static Dictionary<int, Enum> CardColor = Enum[nameof(CardColor)];
        public static Dictionary<int, Enum> CardColorCss = Enum[nameof(CardColorCss)];
        public static Dictionary<int, Enum> CardRace = Enum[nameof(CardRace)];

        public static Dictionary<int, Enum> RuneColor = Enum[nameof(RuneColor)];
        public static Dictionary<int, Enum> RuneFragmentColor = Enum[nameof(RuneFragmentColor)];
        public static Dictionary<int, Enum> RuneProperty = Enum[nameof(RuneProperty)];
        public static Dictionary<int, Enum> RuneSkillConditionCompare = Enum[nameof(RuneSkillConditionCompare)];
        public static Dictionary<int, Enum> RuneSkillConditionSlide = Enum[nameof(RuneSkillConditionSlide)];
        public static Dictionary<int, Enum> RuneSkillConditionType = Enum[nameof(RuneSkillConditionType)];

        public static Dictionary<int, Enum> SkillAffectType = Enum[nameof(SkillAffectType)];
        public static Dictionary<int, Enum> SkillAffectTypePosition = Enum[nameof(SkillAffectTypePosition)];
        public static Dictionary<int, Enum> SkillAffectTypeSide = Enum[nameof(SkillAffectTypeSide)];
        public static Dictionary<int, Enum> SkillCategory = Enum[nameof(SkillCategory)];
        public static Dictionary<int, Enum> SkillLaunchCondition = Enum[nameof(SkillLaunchCondition)];
        public static Dictionary<int, Enum> SkillLaunchType = Enum[nameof(SkillLaunchType)];

        public static Dictionary<int, Enum> MapStageDetialType = Enum[nameof(MapStageDetialType)];

        private static readonly Thread GitHubIssueUpdateThread = new Thread(new ParameterizedThreadStart(GitHubIssueUpdate));
        private static void GitHubIssueUpdate(object target)
        {
            var dbContext = new MkhxCoreContext();
            var wc = new WebClient();
            while (true)
            {

                try
                {
                    var GitHubIssue = dbContext.Enum.First(m => m.Type == "GitHubIssue");

                    System.Console.ForegroundColor = System.ConsoleColor.Cyan;
                    System.Console.WriteLine();
                    System.Console.WriteLine("下载 GitHub Issue 页面");
                    System.Console.WriteLine();
                    System.Console.ResetColor();

                    var html = wc.DownloadString("https://github.com/HumJ0218/HumphreyJ.Netcore.MKHX/issues");

                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                    System.Console.WriteLine();
                    System.Console.WriteLine("下载完成，字符长度 "+ html.Length);
                    System.Console.WriteLine();
                    System.Console.ResetColor();


                    GitHubIssue.Desc = html;
                    GitHubIssue.Value1Format = System.DateTime.Now.ToString();

                    dbContext.SaveChanges();

                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                    System.Console.WriteLine();
                    System.Console.WriteLine("已更新 GitHub Issue 页面缓存");
                    System.Console.WriteLine();
                    System.Console.ResetColor();

                    Thread.Sleep(500000);   //  缓存成功后增加间隔500秒
                }
                catch (System.Exception ex)
                {
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                    System.Console.WriteLine();
                    System.Console.WriteLine("更新 GitHub Issue 页面缓存出错");
                    System.Console.WriteLine();
                    System.Console.WriteLine(ex);
                    System.Console.WriteLine();
                    System.Console.ResetColor();
                }
                Thread.Sleep(60000);    //  每轮缓存间隔60秒
            }

        }

        public static Dictionary<string, object> GitHumIssueListPage
        {
            get
            {
                try { GitHubIssueUpdateThread.Start(); } catch { }

                var GitHubIssue = new MkhxCoreContext().Enum.First(m => m.Type == "GitHubIssue");
                return new Dictionary<string, object> {
                    { "Content", GitHubIssue.Desc },
                    { "UpdateTime", System.DateTime.Parse(GitHubIssue.Value1Format) },
                };
            }
        }
    }
}