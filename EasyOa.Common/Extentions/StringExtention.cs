﻿using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOa.Common
{
    public static class StringExtention
    {
        public static string ToMD5(this string str)
        {
            return EncryptHelper.Md5String(str);
        }
        public static string ToSpell(this string str, bool simple = false)
        {
            if (string.IsNullOrEmpty(str)) return "";
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
    }
}
