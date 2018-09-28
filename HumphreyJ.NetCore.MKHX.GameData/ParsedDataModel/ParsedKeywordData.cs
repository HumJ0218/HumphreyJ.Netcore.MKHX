using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
                throw new ArgumentException("解析关键字数据出错", ex);
            }
        }

        public override string ToString()
        {
            return key;
        }

    }
}