using System;
using System.Collections;
using System.Text;
namespace DryDump
{
    /// <summary>
    /// 1.Reverse(string input,int step)：字符串反转
    /// 2.eightHexStringToFloat(string x1)：16进制字符串转float
    /// 3.FloatStringToHexString(string X)：float字符串转16进制字符串
    /// 4.StringToByteArray(string str)：字符串转字节数组
    /// 5.ByteArrayToString(byte[] byteArray)：字节数组转字符串
    /// 6.ByteArrayToFloat(byte[] byteArray)：字节数组转float
    /// 7.DeleteHyphen(string str)：去除字符串中的-
    /// 8.DateTimeToShortDate(DateTime dt)：datetime转短日期字符串
    /// 9.DateTimeToLongDate(DateTime dt)：datetime转长日期字符串
    /// 10.DecimalToHex(int dec)：int数字转16进制字符串
    /// </summary>
    public class FormatRelated
    {
        /// <summary>
        /// assume input="123456"
        /// if(step==1) return "654321"
        /// if(step==2) return "563412"
        /// </summary>
        /// <param name="input"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public string Reverse(string input,int step)
        {
            try
            {
                string result = "";
                for (int i = input.Length; i > 0; i = i - step)
                {
                    result += input.Substring(i - step, step);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.ToString();
            }
            
        }
        /// <summary>
        /// W0=999A  W1=3F99 regard W1W0 as float
        /// 999A3F99->1.2
        /// </summary>
        /// <param name="x1"></param>
        /// <returns></returns>
        public float eightHexStringToFloat(string x1)//8wei
        {
            byte[] tmp = new byte[4];
            tmp[0] = (byte)Convert.ToInt32(x1.Substring(2, 2), 16);
            tmp[1] = (byte)Convert.ToInt32(x1.Substring(0, 2), 16);
            tmp[2] = (byte)Convert.ToInt32(x1.Substring(6, 2), 16);
            tmp[3] = (byte)Convert.ToInt32(x1.Substring(4, 2), 16);
            float f = BitConverter.ToSingle(tmp, 0);
            return f;
        }
        /// <summary>
        /// X=1.2
        /// buf={"9A","99","99","3F"}
        /// x=9A-99-99-3F
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public string FloatStringToHexString(string X) //1.2->999A3F99
        {
            float f=Convert.ToSingle(X);
            byte[] buf = BitConverter.GetBytes(f);
            string x =BitConverter.ToString(buf);
            string[] temp = x.Split('-');
            x = temp[1] + temp[0] + temp[3] + temp[2];
            return x;
        }
        public byte[] StringToByteArray(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        public string ByteArrayToString(byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray);
        }
        public float ByteArrayToFloat(byte[] byteArray)
        {
            return BitConverter.ToSingle(byteArray, 0);
        }
        public string DeleteHyphen(string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, "-", "");
        }
        public string DateTimeToShortDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        public string DateTimeToLongDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public string DecimalToHex(int dec)
        {
            return string.Format("{0:X}", dec);
        }
        /// <summary>
        /// da=W num=12
        /// return WC
        /// </summary>
        /// <param name="da"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public string ReturnHexDataArea(string da,int num)
        {
            return da + string.Format("{0:X}", num);
        }
        /// <summary>
        /// beginAddress="000000" increment=10
        /// return 00000A
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public string ReturnHexAddress(string beginAddress,int increment)
        {
            int b1 = Convert.ToInt32(beginAddress, 16) + increment;
            return string.Format("{0:X6}", b1);
        }
        public object[] ArrayListToArray(ArrayList arr)
        {
            return  (object[])arr.ToArray(typeof(object));
        }
    }
}
