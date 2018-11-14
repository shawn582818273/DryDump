using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DryDump
{
    public class FileRelated
    {
        public bool DeleteFileOrDirectory(string path)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(path);
                if (attr == FileAttributes.Directory)
                {
                    Directory.Delete(path, true);
                }
                else
                {
                    File.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
        public bool CreateFileOrDirectory(string path)
        {
            try
            {
                string Regextext = "^.*\\..*";
                if (Regex.IsMatch(path, Regextext))
                {
                    if (File.Exists(path)) { }
                    else
                        File.Create(path).Close();
                }                   
                else
                    Directory.CreateDirectory(path);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
        }
        public bool DataTableToExcel(DataTable dt,string savePath)
        {
            try
            {
                //创建文件
                FileStream file = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);

                //以指定的字符编码向指定的流写入字符
                StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding("GB2312"));

                StringBuilder strbu = new StringBuilder();

                //写入标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strbu.Append(dt.Columns[i].ColumnName.ToString() + "\t");
                }
                //加入换行字符串
                strbu.Append(Environment.NewLine);

                //写入内容
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        strbu.Append(dt.Rows[i][j].ToString() + "\t");
                    }
                    strbu.Append(Environment.NewLine);
                }

                sw.Write(strbu.ToString());
                sw.Flush();
                file.Flush();

                sw.Close();
                sw.Dispose();

                file.Close();
                file.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        
    }
}
