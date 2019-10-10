using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallManagerSpace.Resources.DataBase
{
    public class SqliteData
    {
        //读取记录代码 
        public void loadSqliteRecord(int x = 0)
        {
            //设置连接字符串
            string constr = "Data Source=../../DataBase/demotest.db";
            //创建连接对象
            SQLiteConnection con = new SQLiteConnection(constr);
            //设置SQL查询语句
            string sql = "select * from student";
            //创建命令对象
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            con.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(x));
                }
            }

            con.Close();
            cmd = null;
            con.Dispose();
        }
        //添加记录代码
        public void insertSqliteRecord(string id)
        {

            Random ra = new Random(20);//这个随机数不是数据库调用的必要语句
            string constr = "Data Source=../data/MyDataBase.db;";
            //设置SQL查询语句
            string sql = "insert into TestTab(id,name,age) values('" + id + "','student0" + id.TrimStart("stu".ToCharArray()) + "','" + ra.Next(16, 20) + "')";
            ////创建连接对象
            SQLiteConnection con = new SQLiteConnection(constr);
            //创建命令对象
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            cmd = null;
            con.Dispose();

        }
    }
}
