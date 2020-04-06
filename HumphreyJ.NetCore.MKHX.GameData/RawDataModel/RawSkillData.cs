using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    public class RawSkillData
    {
        public string SkillId;
        public string Name;
        public string Type;
        public string LanchType;
        public string LanchCondition;
        public string LanchConditionValue;
        public string AffectType;
        public string AffectValue;
        public string AffectValue2;
        public string SkillCategory;
        public string Desc;
        public string DESCRIBE_NEW;
        public string DESCRIBE_EXTRA;

        public static RawSkillData[] ParseJsonString(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<RawSkillData[]>(json);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
                
                throw new ArgumentException("技能原始数据异常", ex);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}