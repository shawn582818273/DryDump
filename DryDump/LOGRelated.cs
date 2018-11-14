using System;
using System.IO;
using System.Text;

namespace DryDump
{
    public class LOGRelated
    {
        public bool LogWrite(string path,string logMessage)
        {
            try
            {
                //DateTime now = DateTime.Now;
                //string ss = now.ToString("yyyy-MM-dd");
                //string path = @"\\Mac\Home\Desktop\BOE\HSMSDemo\HSMSDemo\bin\Debug\PLCLog\" + ss + ".log";
                FileStream fs = new FileStream(@path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                try
                {
                    string logLine = string.Format("{0:G}: {1}.",DateTime.Now, logMessage);
                    sw.WriteLine(logLine);
                }
                finally
                {
                    sw.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public string LogRead(string path)
        {
            try
            {
                //DateTime now = DateTime.Now;
                //string ss = now.ToString("yyyy-MM-dd");
                //string path = @"\\Mac\Home\Desktop\BOE\HSMSDemo\HSMSDemo\bin\Debug\PLCLog\" + ss + ".log";
                FileStream fs = new FileStream(@path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                string strline = sr.ReadLine();
                string str = "";
                while (strline != null)
                {
                    str += strline.ToString() + "\r\n";
                    strline = sr.ReadLine();
                }
                sr.Close();
                fs.Close();
                return str;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.ToString();
            }
        }
        
    }
}
