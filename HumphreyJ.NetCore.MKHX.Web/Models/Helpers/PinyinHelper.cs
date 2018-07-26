using Pinyin4net.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumphreyJ.NetCore.MKHX.Web.Models.Helpers
{
    public class PinyinHelper
    {

        public static HanyuPinyinOutputFormat HanyuPinyinOutputFormat = new HanyuPinyinOutputFormat
        {
            CaseType = HanyuPinyinCaseType.LOWERCASE,
            VCharType = HanyuPinyinVCharType.WITH_U_UNICODE,
            ToneType = HanyuPinyinToneType.WITH_TONE_NUMBER
        };

        public static char GetPinyinAbbreviation(char c)
        {
            c = c.ToString().ToUpper()[0];
            if ('A' <= c && c <= 'Z')
            {
                return c;
            }
            try
            {
                return Pinyin4net.PinyinHelper.ToHanyuPinyinStringArray(c, HanyuPinyinOutputFormat)[0][0];
            }
            catch
            {
                return '#';
            }
        }
    }
}
