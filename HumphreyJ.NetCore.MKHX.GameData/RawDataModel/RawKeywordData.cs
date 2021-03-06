﻿using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawKeywordData
    {
        public string id;
        public string key;
        public string des;

        public static RawKeywordData[] ParseJsonString(string json)
        {
            try
            {
                var jo = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(json);
                var v = jo.Values();
                var ie = v.Select(m => new RawKeywordData
                {
                    id = m.Path,
                    key = m.Value<string>("key"),
                    des = m.Value<string>("des"),
                }).ToArray();
                return ie;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
                
                throw new ArgumentException("关键字原始数据异常", ex);
            }
        }

        public override string ToString()
        {
            return key;
        }
    }
}