using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Archive2._0.Utility
{
    class Binary
    {
        public static string ToBinConvert(char arg)
        {
            string ret;
            ret = Convert.ToString(arg,2); //обычная функция получения бинарного кода
            while(ret.Length <8)
            {
                ret = '0' + ret; //дополнение длины до 8
            }
            return ret;

        }
        public static string ToBinConvert(string arg)
        {
            string ret = "";
            for (byte i =0; i<arg.Length;i++)
            {
                ret += ToBinConvert(arg[i]);
            }
            return ret;
        }

        public static char FromBinConvert(string arg)
        {
            int bt = 0;
            for (byte i = 0; i < 8; i++)
            {
                if (arg[i] == '1')
                {
                    bt += (int)(Math.Pow(2, 7 - i));
                }
            }
            return (char)(bt);
        }
    }
}
