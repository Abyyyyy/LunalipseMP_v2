﻿using System;
using System.Collections.Generic;

namespace Lunalipse.Utilities
{
    public enum SizeUnitScaling : int
    {
        Bytes = 0,
        KiloBytes = 1,
        MegaBytes = 2,
        GigaBytes = 3
    };
    public class Utils
    {
        private static string[] Prefixes = { "" ,"K", "M", "G", "T" };

        /// <summary>
        /// 数据大小单位转换
        /// </summary>
        /// <param name="size">大小</param>
        /// <param name="scale">起始度量值</param>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public static string SizeConvert(long size, SizeUnitScaling scale)
        {
            int inx = (int)scale;
            while (size > 1024)
            {
                size = size / 1024;
                inx++;
            }
            if (inx > Prefixes.Length - 1) throw new OverflowException("values for given scale:{0} is too big to convert".FormateEx(scale.ToString()));
            return "{0}{1}".FormateEx(size, Prefixes[inx]);
        }

        public static string Second2Str(long sec, string seperator = ":")
        {
            int h = 0, m = 0, s = 0;
            long tmp = sec;
            while(tmp>0)
            {
                while (tmp >= 60)
                {
                    while (tmp >= 3600)
                    {
                        tmp -= 3600;
                        h++;
                    }
                    tmp -= 60;
                    m++;
                }
                tmp--;
                s++;
            }
            return "{0}{1}{2}{3}{4}".FormateEx(h, seperator, m, seperator, s);
        }

        public static string ms2Str(long ms, string seperator = ":")
        {
            return Second2Str(ms / 1000, seperator);
        }

        public static string[] ParseCommand(string command)
        {
            List<string> arg = new List<string>();
            string str = "";
            bool quoteStart = false;
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == ' ' && !quoteStart)
                {
                    arg.Add(str);
                    str = "";
                    continue;
                }
                if (!quoteStart && command[i] == '"')
                {
                    quoteStart = true;
                    continue;
                }
                else if (quoteStart && command[i] == '"')
                {
                    quoteStart = false;
                    continue;
                }
                str += command[i];
            }
            if (str != "") arg.Add(str);
            return arg.ToArray();
        }
    }
}
