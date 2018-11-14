using System;
using MySql.Data.MySqlClient;

namespace DryDump
{
    public class SQLRelated
    {
        public MySqlConnection MysqlConnect(string server,string UserID,string password,string Database)
        {
            //string constr = "server=localhost;User Id=root;password=123456;Database=test";
            string constr = "server=" + server + ";User ID=" + UserID + ";password=" + password + ";Database=" + Database;
            MySqlConnection mycon = new MySqlConnection(constr);
            mycon.Open();
            Console.WriteLine("mysql open");
            return mycon;
        }
        public void MysqlClose(MySqlConnection mycon)
        {
            mycon.Close();
        }
        public void MysqlInsert(MySqlConnection mycon,string cmdText)
        {
            #region
            /*
            MySqlCommand mycmd = new MySqlCommand("insert into t1(id,name) values(4,'pfh')", mycon);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("数据插入成功！");
            }               
            
           
            MySqlCommand mycmd = new MySqlCommand("INSERT INTO `test`.`parameters`(`  unitid`, `ip`, `current`, `voltage`, `temper`, `pressure`, `time`) VALUES ('1', '127.0.0.1', '0', '0', '0', '0', '2018-10-08 18:00:00')", mycon);
            MessageBox.Show(mycmd.CommandText);
            */
            #endregion
            MySqlCommand mycmd = new MySqlCommand(cmdText, mycon);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                //MessageBox.Show("数据插入成功！");
                Console.WriteLine("数据插入成功！"+cmdText);
            }
        }
        public MySqlDataReader MysqlQuery1(MySqlConnection mycon,string cmdText)
        {
            #region
            /*
            DateTime now = DateTime.Now;
            string ss = now.ToString("yyyy-MM-dd HH:mm:ss");
            int timespan = -Convert.ToInt32(SelectDay.Text);
            DateTime datebegin = now.AddDays(timespan);
            string datebeginstr = datebegin.ToString("yyyy-MM-dd HH:mm:ss");
            MySqlCommand cmd = new MySqlCommand("select avg(current) as 'current',AVG(voltage) as 'voltage',AVG(temper) as 'temper'," +
                    "AVG(pressure) as 'pressure' from parameters where  time between '" + datebeginstr + "'  and '" + ss + "'and `  unitid`='" + unitid + "';", mycon);
            string sql = "select * from t1";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            MessageBox.Show("hello");
            */
            #endregion
            MySqlCommand cmd = new MySqlCommand(cmdText, mycon);
            MySqlDataReader reader = null;
            reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    if (reader.HasRows)//有一行读一行
            //    {
            //        //MessageBox.Show(reader["current"].ToString()); 
            //        Console.WriteLine(reader.GetString(1));
            //    }
            //}
            return reader;
        }
        public MySqlDataAdapter MysqlQuery2(MySqlConnection mycon, string cmdText)
        {
            MySqlCommand cmd = new MySqlCommand(cmdText, mycon);
            MySqlDataAdapter mda = new MySqlDataAdapter(cmdText, mycon);
            return mda;
        }


    }
}
