using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.Web.Models.Helpers
{
    public class TestDataAccessKeyHelper
    {
        private static string AK { get; set; }
        internal static void Init()
        {
            var path = "./TestDataAccessKey.txt";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, Guid.NewGuid().ToString("N"));
            }
            AK = File.ReadAllText(path);

            Console.WriteLine();
            Console.WriteLine("********************************");
            Console.WriteLine("AccessKey");
            Console.WriteLine(AK);
            Console.WriteLine("********************************");
            Console.WriteLine();
        }
        public static bool Check(string ak)
        {
            return ak == AK;
        }
    }
}