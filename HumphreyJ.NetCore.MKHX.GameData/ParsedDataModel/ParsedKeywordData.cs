using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class ParsedKeywordData
    {
#pragma warning disable IDE1006 // 命名样式
        public int id { get; private set; }
        public string key { get; private set; }
        public string des { get; private set; }
#pragma warning restore IDE1006 // 命名样式

        public ParsedKeywordData(RawKeywordData raw)
        {
            try
            {
                id = int.Parse(raw.id);
                key = raw.key;
                des = raw.des;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.WriteLine(JsonConvert.SerializeObject(raw));
                Console.ResetColor();

                throw new ArgumentException($"解析 ID 为 {raw.id} 的关键字数据时出错", ex);
            }
        }

        public override string ToString()
        {
            return key;
        }

    }
}