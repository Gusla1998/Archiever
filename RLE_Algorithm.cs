using System;
using System.Collections.Generic;
using System.Text;

namespace Archiever
{
    class RLE_Algorithm
    {
        public byte[] compress(byte[] noncomp_arr)
        {
            List<byte> comp_arr = new List<byte>();
            int counter_same = 1;
            byte counter_diff = 0;
            bool zapis_same = false;
            bool zapis_diff = false;
            List<byte> help = new List<byte>();

            for (int i = 0; i < noncomp_arr.Length - 1; i++)
            {
                if (noncomp_arr[i] == noncomp_arr[i+1])
                {
                    counter_same++;

                    if (zapis_diff == true)
                    {
                        string x = dig_to_bin(Convert.ToInt32(counter_diff), false);//переводим число с изменением 1 бита на 0 если повторяющиеся
                        byte y = bin_to_dig(x);//переводим из двоичного вида в десятеричное
                        comp_arr.Add(y);//добавляем обслуживающий байт в массив
                        for (int k = 0; k < help.Count; k++)
                        {
                            comp_arr.Add(help[k]);
                        }

                        zapis_diff = false;
                        help.Clear();
                        counter_diff = 0;
                    }

                    if (counter_same == 127)
                    {
                        string x = dig_to_bin(Convert.ToInt32(counter_same), true);//переводим число с изменением 1 бита на 0 если повторяющиеся
                        byte y = bin_to_dig(x);//переводим из двоичного вида в десятеричное
                        comp_arr.Add(y);//добавляем обслуживающий байт в массив
                        comp_arr.Add(noncomp_arr[i]);//добавляем само число в массив

                        help.Clear();
                        counter_same = 1;
                        i++;
                    }

                    zapis_same = true;
                }
                else
                {
                    counter_diff++;
                    

                    if (zapis_same == true)
                    {
                        string x = dig_to_bin(Convert.ToInt32(counter_same), true);//переводим число с изменением 1 бита на 0 если повторяющиеся
                        byte y = bin_to_dig(x);//переводим из двоичного вида в десятеричное
                        comp_arr.Add(y);//добавляем обслуживающий байт в массив
                        comp_arr.Add(noncomp_arr[i]);//добавляем само число в массив

                        zapis_same = false;
                        counter_same = 1;
                        i++;
                    }

                    help.Add(noncomp_arr[i]);
                    zapis_diff = true;
                }
            }
            //последняя пачка байтов не записывается, поэтому
            if (zapis_diff == true)
            {
                help.Add(noncomp_arr[noncomp_arr.Length - 1]);
                string x = dig_to_bin(Convert.ToInt32(counter_diff + 1), false);//переводим число с изменением 1 бита на 0 если повторяющиеся
                byte y = bin_to_dig(x);//переводим из двоичного вида в десятеричное
                comp_arr.Add(y);//добавляем обслуживающий байт в массив
                for (int k = 0; k < help.Count; k++)
                {
                    comp_arr.Add(help[k]);
                }
            }
            else if (zapis_same == true)
            {
                string x = dig_to_bin(Convert.ToInt32(counter_same + 1), true);//переводим число с изменением 1 бита на 0 если повторяющиеся
                byte y = bin_to_dig(x);//переводим из двоичного вида в десятеричное
                comp_arr.Add(y);//добавляем обслуживающий байт в массив
                comp_arr.Add(noncomp_arr[noncomp_arr.Length - 1]);//добавляем само число в массив
            }
            byte[] final_arr = new byte[comp_arr.Count];

            for (int i=0; i < comp_arr.Count; i ++)
            {
                final_arr[i] = comp_arr[i];
            }
            return final_arr;
        }

        public byte[] decompress(byte[] comp_arr)
        {
            List<byte> decomp_arr = new List<byte>();
            for (int i=0; i < comp_arr.Length; )
            {
                string bin_view = dig_to_bin(comp_arr[i], true);
                if (bin_view[0] == '0')
                {
                    for (int j=0; j<comp_arr[i];j++)
                    {
                        decomp_arr.Add(comp_arr[i + 1]);
                    }
                    i = i + 2;
                }
                else
                {
                    string remove_view = bin_view.Remove(0, 1);
                    byte byte_view = bin_to_dig(remove_view);
                    for (int j=0; j<byte_view;j++)
                    {
                        decomp_arr.Add(comp_arr[i + j + 1]);
                    }
                    i = i + byte_view + 1;
                }
            }

            byte[] final_arr = new byte[decomp_arr.Count];

            for (int i = 0; i < decomp_arr.Count; i++)
            {
                final_arr[i] = decomp_arr[i];
            }
            return final_arr;
        }
        //перевод в двоичную СС
        private string dig_to_bin(int num, bool flag)
        {
            int temp1 = 0;
            List<int> s = new List<int>();
            if (num == 0)
            {
                s.Add(0);
            }
            else
            {
                while (num > 0)
                {
                    temp1 = num % 2;
                    num = num / 2;
                    s.Add(temp1);
                }
            }
            int[] b = new int[s.Count];
            for (int i = s.Count - 1; i >= 0; i--)
            {
                b[s.Count - 1 - i] = s[i];
            }
            string stroka = string.Join<int>("", b);
            if (flag == true)
            {
                while (stroka.Length < 8)
                {
                    stroka = "0" + stroka;
                }
            }
            else
            {
                while (stroka.Length < 7)
                {
                    stroka = "0" + stroka;
                }
                stroka = "1" + stroka;
            }
            
            return stroka;
        }

        private byte bin_to_dig(string bin_view)
        {
            byte o_num = 0;
            byte f_num = 0;

            for (int i=0; i < bin_view.Length; i++)
            {
                double x = char.GetNumericValue(bin_view[i]);
                o_num = Convert.ToByte(x * Math.Pow(2, bin_view.Length - i - 1));
                f_num += o_num;
            }

            return f_num;
        }
    }
}
