﻿using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyOa.Common
{
    public static class StringExtention
    {
        /// <summary>
        /// Md5计算
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5(this string str)
        {
            return EncryptHelper.Md5String(str);
        }
        /// <summary>
        /// 拼音转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="simple"></param>
        /// <returns></returns>
        public static string ToSpell(this string str, bool simple = false)
        {
            if (string.IsNullOrEmpty(str)) return "";
            string fullPath = AppConfig.basePath + AppConfig.GetConfig("pinypath");
            Dictionary<string, string[]> dict = FileHelper.ReadFileSplit(fullPath, "|");
            if (dict != null && dict.Count > 0)
            {
                foreach (string key in dict.Keys)
                {
                    string[] value = dict[key];
                    if (str.Contains(key))
                    {
                        if (simple)
                        {
                            if (value.Length > 1)
                            {
                                str = str.Replace(key, value[1]);
                            }
                            else
                            {
                                str = str.Replace(key, value[0].Substring(0, 1));
                            }
                        }
                        else
                        {
                            str = str.Replace(key, value[0]);
                        }
                    }
                }
            }
            List<string> res = new List<string>();
            foreach (char c in str)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    string piny = new ChineseChar(c).Pinyins[0];
                    piny = piny.Substring(0, piny.Length - 1);
                    res.Add(piny.ToLower());
                }
                else
                {
                    res.Add(c.ToString());
                }
            }
            return simple ? string.Join("", res.Select(r => r.Substring(0, 1))) : string.Join("", res);
        }
        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regexp"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string Replace2(this string str, string regexp, string replacement)
        {
            return Regex.Replace(str, regexp, replacement);
        }
    }
}
