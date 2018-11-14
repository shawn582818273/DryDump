using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace DryDump
{
    public class MCRelated
    {
        //Socket clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //public static byte[] result = new byte[100];
        //IPAddress ip = IPAddress.Parse("192.168.1.254");
        public bool SocketLink(Socket clientsocket, IPAddress ip, int port)
        {
            try
            {
                clientsocket.Connect(new IPEndPoint(ip, port));
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctype"></param>
        /// <param name="CheckSumLength"></param>
        /// <param name="mcmd"></param>
        /// <param name="scmd"></param>
        /// <param name="dataArea"></param>
        /// <param name="beginAddress"></param>
        /// <param name="length"></param>
        /// <param name="writeValue"></param>//if read,writevalue=null
        /// <returns></returns>
        public string CreateMsg(CommunicationType ctype, string RequestDataLength, MainCmd mcmd,SubCmd scmd,string dataArea,string beginAddress,string length,string writeValue)
        {
            MCcmd mc= new MCcmd();
            string mcMsg = null;
            if(ctype==0)
            {
                mcMsg = mc.SubTitleNumber + mc.NetwrokNumber + mc.PcNumber + GetDescription(IOnumber.ascii) + mc.ChannelNumber + RequestDataLength
                    + GetDescription(Cpu_time.ascii) + string.Format("{0:X4}", mcmd.GetHashCode())+ string.Format("{0:X4}", scmd.GetHashCode())
                    + GetDescription((DataArea_ASCII)Enum.Parse(typeof(DataArea_ASCII), dataArea)) + beginAddress + length + writeValue;
                
            }
            else
            {
                mcMsg = mc.SubTitleNumber + mc.NetwrokNumber + mc.PcNumber + GetDescription(IOnumber.bin) + mc.ChannelNumber + RequestDataLength
                 + GetDescription(Cpu_time.bin) + string.Format("{0:X4}", mcmd.GetHashCode()) + string.Format("{0:X4}", scmd.GetHashCode())
                 + beginAddress+ string.Format("{0:X}",(int)Enum.Parse(typeof(DataArea_BIN), dataArea)) + length + writeValue;
              
            }
            
            return mcMsg;
        }
        public string SendMsg(Socket clientsocket,string mcMsg,CommunicationType ctype)
        {
            try
            {
                byte[] result = new byte[100];
                string s1 = null;
                int receiveLength;
                if (ctype == 0)
                {
                    clientsocket.Send(Encoding.ASCII.GetBytes(mcMsg));
                    receiveLength = clientsocket.Receive(result);
                    s1 = Encoding.ASCII.GetString(result, 0, receiveLength);
                }
                else
                {
                    byte[] bytes = new byte[mcMsg.Length / 2];
                    for (int i = 0; i < mcMsg.Length; i += 2)
                        bytes[i / 2] = (byte)Convert.ToByte(mcMsg.Substring(i, 2), 16);
                    clientsocket.Send(bytes);
                    receiveLength = clientsocket.Receive(result);
                    for (int j = 0; j < receiveLength; j++)
                    {
                        s1 += String.Format("{0:X2}", result[j]);
                    }
                }
                return s1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return ex.ToString();
            }
            
        }

        public enum CommunicationType
        {
            ASCII = 0,
            BIN = 1
        }
        public enum MainCmd
        {
            AsciiRead = 0x0401,
            AsciiWrite = 0x1401,
            BinRead = 0x0104,
            BinWrite = 0x0114
        }
        public enum SubCmd
        {
            AsciiWord = 0x0000,
            AsciiBit = 0x0001,
            BinWord = 0x0000,
            BinBit = 0x0100
        }
        
        public enum DataArea_BIN
        {
            M =0x90,
            X = 0x9C,
            Y = 0x9D,
            B = 0xA0,
            D = 0xA8,
            W = 0xB4  
                
        }
        public enum DataArea_ASCII
        {
            [Description("B*")]
            B,
            [Description("W*")]
            W,
            [Description("M*")]
            M,
            [Description("X*")]
            X,
            [Description("Y*")]
            Y,
            [Description("D*")]
            D,
            [Description("R*")]
            R,
            [Description("TN")]
            TN
        }
        public enum IOnumber
        {
            [Description("03FF")]
            ascii,
            [Description("FF03")]
            bin
        }
        public enum Cpu_time
        {
            [Description("0010")]
            ascii,
            [Description("1000")]
            bin
        }

        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public class MCcmd
        {
            public string SubTitleNumber = "5000";      // 副标题 default 5000
            public string NetwrokNumber = "00";      // 网络编号 default 00
            public string PcNumber = "FF";          // PC编号/PLC编号default FF
            public string IoNumber = "03FF";         // 请求目标模块IO编号default 03FF  BIN:LH
            public string ChannelNumber = "00";    // 请求目标模块站编号default 00
            public string RequestDataLength = "0018";//请求数据长:后面所有的总和,  BIN :LH
            public string CpuTimer = "0010";          // CPU监视定时器default 0010   BIN:LH
            //数据部分
            public string MainCommand = string.Format("{0:X4}",MainCmd.AsciiRead.GetHashCode());//指令0401代表读，0x1401 成批写  BIN:LH
            public string SubCommand = string.Format("{0:X4}", SubCmd.AsciiWord.GetHashCode());//子指令Bit = 0x0001按位读写，， Word = 0x0000按字读写    BIN:LH
            public string DataArea = GetDescription(DataArea_ASCII.W);//数据区 W* B*
            public string AddressBegin = "000000";//起始地址 6位 BIN:LH
            public string Length = "0001";//读取位数 BIN:LH
            public string WriteValue = "0";
        }
        
    }
}
