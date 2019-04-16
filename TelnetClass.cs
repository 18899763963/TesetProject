using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;

namespace TelnetNameSpace
{
    class TelnetClass
    {
        public TelnetClass(string Address, int Port)
        {
            address = Address;
            port = Port;
            dev = new Mutex();
            dev1 = new Mutex();
            dev2 = new Mutex();
            LoginStringMark = "VxWorks login:";
            PasswordStringMark = "Password:";
            PromptMark = "->";
            console_string = "";

            usr_name = "bmu852";
            passwd = "aaaabbbb";
            vxworks = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            buffer_list = new List<string>();
            buffer_list1 = new List<string>();
            buffer_list2 = new List<string>();

            messageQueue = new Queue();
            messageQueueSyn = Queue.Synchronized(messageQueue);

            buffer = "";
            strWorkingData = "";
            task_delay = 10;
        }

        private string usr_name;
        private string passwd;

        private string strWorkingData;

        private string buffer;
        private List<string> buffer_list;  //用于主线处理，例如输入测试命令，waitfor关键字
        private List<string> buffer_list1;  //用于监控线程
        private List<string> buffer_list2;  //用于查看打印信息
        private Mutex dev;
        private Mutex dev1;
        private Mutex dev2;
        private Mutex Send_Mutex;

        private Queue messageQueue;  //定义一个队列，用于放置自定义消息
        private Queue messageQueueSyn;  //转换成线程安全型

        private Socket vxworks;
        private IPEndPoint iep;
        private string address;
        private int port;

        private string LoginStringMark;
        private string PasswordStringMark;
        private string PromptMark;

        private Thread receive;
        private Thread doreceived;

        public string console_string;

        public int task_delay;


        #region 关闭字符接收处理线程
        public void killThread()
        {
            if (receive.IsAlive)
            {
                receive.Abort();
            }
            if (doreceived.IsAlive)
            {
                doreceived.Abort();
            }
        }
        #endregion


        #region buffer_list的操作函数，用于测试线程专用
        //获取buffer_list中头字符串的函数
        public string buf_get_head()
        {
            try
            {
                string tmp;
                dev.WaitOne();
                tmp = buffer_list[0];
                dev.ReleaseMutex();
                return tmp;
            }
            catch
            {
                return "";
            }
        }

        //获取buffer_list中尾字符串的函数
        public string buf_get_end()
        {
            try
            {
                string tmp;
                dev.WaitOne();
                int n = buffer_list.Count - 1;
                tmp = buffer_list[n];
                if (tmp.Length == 0)
                {
                    tmp = buffer_list[n - 1];
                }
                dev.ReleaseMutex();
                return tmp;
            }
            catch
            {
                return "";
            }
        }

        //获取buffer_list中所有字符串的函数
        public string buf_get_all()
        {
            try
            {
                string tmp;
                dev.WaitOne();
                tmp = string.Join("\r\n", buffer_list.ToArray());
                dev.ReleaseMutex();
                return tmp;
            }
            catch
            {
                return "";
            }
        }

        //向buffer_list中增加字符串的函数
        public void buf_add_line(string s)
        {
            dev.WaitOne();
            buffer_list.Add(s);
            dev.ReleaseMutex();
        }

        //删除buffer_list中头一行的函数
        public void buf_del_head()
        {
            dev.WaitOne();
            if (buffer_list.Count == 0)
                return;
            buffer_list.RemoveAt(0);
            dev.ReleaseMutex();
        }

        //更新buffer_list中正在写入的一行（即最后一行）的函数
        public void buf_mod_tail(string s)
        {
            dev.WaitOne();
            if (buffer_list.Count > 0)
                buffer_list.RemoveAt(buffer_list.Count - 1);  //去除字符串集合中最后的一个字符串
            buffer_list.Add(s);
            dev.ReleaseMutex();
        }

        //清空buffer_list中字符串的函数
        public void buf_del_all()
        {
            dev.WaitOne();
            if (buffer_list.Count > 0)
            {
                buffer_list.RemoveRange(0, buffer_list.Count);
            }
            dev.ReleaseMutex();
        }
        #endregion


        #region buffer_list1的操作函数，用于监控线程专用
        //获取buffer_list1中头字符串的函数
        public string buf_get_head1()
        {
            try
            {
                string tmp1;
                dev1.WaitOne();
                tmp1 = buffer_list1[0];
                dev1.ReleaseMutex();
                return tmp1;
            }
            catch
            {
                return "";
            }
        }

        //获取buffer_list1中尾字符串的函数
        public string buf_get_end1()
        {
            try
            {
                string tmp1;
                dev1.WaitOne();
                int n = buffer_list1.Count - 1;
                tmp1 = buffer_list1[n];
                if (tmp1.Length == 0)
                {
                    tmp1 = buffer_list1[n - 1];
                }
                dev1.ReleaseMutex();
                return tmp1;
            }
            catch
            {
                return "";
            }
        }

        //获取buffer_list1中所有字符串的函数
        public string buf_get_all1()
        {
            try
            {
                string tmp1;
                dev1.WaitOne();
                tmp1 = string.Join("\r\n", buffer_list1.ToArray());
                dev1.ReleaseMutex();
                return tmp1;
            }
            catch
            {
                return "";
            }
        }

        //向buffer_list1中增加字符串的函数
        public void buf_add_line1(string s)
        {
            dev1.WaitOne();
            buffer_list1.Add(s);
            dev1.ReleaseMutex();
        }

        //删除buffer_list1中头一行的函数
        public void buf_del_head1()
        {
            dev1.WaitOne();
            if (buffer_list1.Count == 0)
                return;
            buffer_list1.RemoveAt(0);
            dev1.ReleaseMutex();
        }

        //更新buffer_list1中正在写入的一行（即最后一行）的函数
        public void buf_mod_tail1(string s)
        {
            dev1.WaitOne();
            if (buffer_list1.Count > 0)
                buffer_list1.RemoveAt(buffer_list1.Count - 1);  //去除字符串集合中最后的一个字符串
            buffer_list1.Add(s);
            dev1.ReleaseMutex();
        }

        //清空buffer_list1中字符串的函数
        public void buf_del_all1()
        {
            dev1.WaitOne();
            if (buffer_list1.Count > 0)
            {
                buffer_list1.RemoveRange(0, buffer_list1.Count);
            }
            dev1.ReleaseMutex();
        }
        #endregion


        #region buffer_list2的操作函数，用于保存打印信息用
        //获取buffer_list2中头字符串的函数
        public string buf_get_head2()
        {
            try
            {
                string tmp2;
                dev2.WaitOne();
                tmp2 = buffer_list2[0];
                dev2.ReleaseMutex();
                return tmp2;
            }
            catch
            {
                return "";
            }
        }

        //获取buffer_list2中尾字符串的函数
        public string buf_get_end2()
        {
            try
            {
                string tmp2;
                dev2.WaitOne();
                int n = buffer_list2.Count - 1;
                tmp2 = buffer_list2[n];
                if (tmp2.Length == 0)
                {
                    tmp2 = buffer_list2[n - 1];
                }
                dev2.ReleaseMutex();
                return tmp2;
            }
            catch
            {
                return "";
            }
        }

        //获取buffer_list2中所有字符串的函数
        public string buf_get_all2()
        {
            try
            {
                string tmp2;
                dev2.WaitOne();
                tmp2 = string.Join("\r\n", buffer_list2.ToArray());
                dev2.ReleaseMutex();
                return tmp2;
            }
            catch
            {
                return "";
            }
        }

        //向buffer_list2中增加字符串的函数
        public void buf_add_line2(string s)
        {
            dev2.WaitOne();
            buffer_list2.Add(s);
            dev2.ReleaseMutex();
        }

        //删除buffer_list2中头一行的函数
        public void buf_del_head2()
        {
            dev2.WaitOne();
            if (buffer_list2.Count == 0)
                return;
            buffer_list2.RemoveAt(0);
            dev2.ReleaseMutex();
        }

        //更新buffer_list2中正在写入的一行（即最后一行）的函数
        public void buf_mod_tail2(string s)
        {
            dev2.WaitOne();
            if (buffer_list2.Count > 0)
                buffer_list2.RemoveAt(buffer_list2.Count - 1);  //去除字符串集合中最后的一个字符串
            buffer_list2.Add(s);
            dev2.ReleaseMutex();
        }

        //清空buffer_list2中字符串的函数
        public void buf_del_all2()
        {
            dev2.WaitOne();
            if (buffer_list2.Count > 0)
            {
                buffer_list2.RemoveRange(0, buffer_list2.Count);
            }
            dev2.ReleaseMutex();
        }
        #endregion



        #region 字符接收处理函数....
        private void receive_thread(object o)  //字符接收函数
        {
            try
            {
                Socket s = (Socket)o;
                buffer = "";

                byte[] tmp = new byte[1];
                string tmp_s = "";
                while (true)
                {
                    try
                    {
                        Thread.Sleep(1);  //task_delay

                        if (s.Receive(tmp) != 1)  //如果没有数据包，Receive将一直阻塞；等于0的唯一可能就是网线断了
                        {
                            s.Close();
                            break;
                        }

                        if (((tmp[0] >= 0x20) && (tmp[0] <= 0x7e)) || (tmp[0] == 0x0a || tmp[0] == 0x0d))
                        {
                            tmp_s = Encoding.ASCII.GetString(tmp);  //过滤不合格的字符
                            messageQueueSyn.Enqueue(tmp_s);
                            //Console.Write(tmp_s);  //调试时，将网口接收的信息显示到调试窗口
                        }

                    }
                    catch
                    {
                        break;
                    }

                }

            }
            catch { }
        }
        #endregion


        #region 处理接收到的字符
        public void process_thread()
        {
            while (true)
            {
                Thread.Sleep(task_delay);  //100task_delay

                if (messageQueueSyn.Count > 0)
                {
                    for (int i = 0; i < messageQueueSyn.Count; i++)
                    {
                        if (buffer.EndsWith("\n"))
                        {
                            buffer = "";
                            buf_add_line(buffer.Trim());  //当检测到回车，认为是行末，则添加一行，继续之前的操作
                            buf_add_line1(buffer.Trim());
                            buf_add_line2(buffer.Trim());
                        }

                        buffer += (string)messageQueueSyn.Dequeue();  // 从队列的开头去除一个字符串，并将其值返回，放入buffer中;
                        buf_mod_tail(buffer.Trim());  //在最后一行一直用最新的buffer.Trim()更新整个一行
                        buf_mod_tail1(buffer.Trim());
                        buf_mod_tail2(buffer.Trim());

                    }
                }
            }
        }
        #endregion


        #region 登录时需要输入的特殊字符
        public void telnet_write_foo()  //向流中写入  ff fd 03 ff fd 01
        {
            byte[] buff1 = { 0xff, 0xfd, 0x03 };
            byte[] buff2 = { 0xff, 0xfd, 0x01 };

            vxworks.Send(buff1);
            Thread.Sleep(100);
            vxworks.Send(buff2);
        }
        #endregion


        #region 专为登录写的输入函数，放慢了输入速度
        public void telnet_write_string(string s)  //向流中写入字符串
        {
            if (vxworks.Connected)
            {
                StreamWriter sw = new StreamWriter(new NetworkStream(vxworks));
                foreach (char c in s)
                {
                    sw.Write(c);
                    sw.Flush();
                    //Thread.Sleep(300);
                    Thread.Sleep(100);
                }
                sw.Close();
            }
        }

        public void telnet_write_line(string s)  //向流中写入字符串
        {
            telnet_write_string(s);
            //telnet_write_string("\r");  //不能输入回车符
            Thread.Sleep(200);
            telnet_write_string("\n");
            Thread.Sleep(200);
        }
        #endregion


        #region 普通的输入函数
        public void telnet_write_string_add(string s)  //向流中写入字符串
        {
            if (vxworks.Connected)
            {
                StreamWriter sw = new StreamWriter(new NetworkStream(vxworks));
                foreach (char c in s)
                {
                    sw.Write(c);
                    sw.Flush();
                    //Thread.Sleep(300);
                }
                sw.Close();
            }
        }

        public void telnet_write_line_add(string s)  //向流中写入字符串
        {

            try
            {
                Send_Mutex.WaitOne();
                telnet_write_string_add(s + "\n");
                Send_Mutex.ReleaseMutex();
            }
            catch
            {

            }

            //telnet_write_string("\r");  //不能输入回车符
            //Thread.Sleep(200);
            //telnet_write_string_add("\n");
            // Thread.Sleep(200);
        }
        #endregion


        #region 连接函数，连接后启动字符接收线程
        public bool Connect()
        {
            IPAddress import = IPAddress.Parse(address);

            iep = new IPEndPoint(import, port);

            try
            {
                vxworks.Bind(new IPEndPoint(IPAddress.Any, 0));
                vxworks.Connect(iep);

                receive = new Thread(receive_thread);
                receive.IsBackground = true;
                receive.Start(vxworks);

                doreceived = new Thread(process_thread);
                doreceived.IsBackground = true;
                doreceived.Start();

                //Thread.Sleep(1000);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion


        #region 等待某字符串函数
        public string WaitFor(string DataToWaitFor, int timeout)  //timeout的单位是秒
        {
            long lngStart = DateTime.Now.AddSeconds(timeout).Ticks;
            long lngCurTime = 0;
            //DateTime startTime = DateTime.Now;

            //do
            //{
            //    strWorkingData = buf_get_all();
            //} while (strWorkingData.ToLower().IndexOf(DataToWaitFor.ToLower()) == -1 && (DateTime.Now - startTime).Seconds < timeout);

            //strWorkingData = buf_get_all();
            //string strPath = System.Windows.Forms.Application.StartupPath + "\\pp.txt";

            while (strWorkingData.ToLower().IndexOf(DataToWaitFor.ToLower()) == -1)
            {
                lngCurTime = DateTime.Now.Ticks;
                if (lngCurTime > lngStart)
                {
                    //MessageBox.Show("Timed Out waiting for : " + DataToWaitFor, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DataToWaitFor;
                }
                strWorkingData = buf_get_all();
                Thread.Sleep(100);

                //File.AppendAllText(strPath, strWorkingData);
                //Console.Write(strWorkingData);
                //Console.Write("\r\n");
            }

            strWorkingData = "";
            return "";
        }
        #endregion


        #region 登录函数
        public bool login()
        {
            int state = 0;
            int tryTimes = 50;
            while (tryTimes>0)
            {
                if (state == 0)
                {
                    if (buffer_list.Count > 1 && (!buf_get_head().ToLower().Contains(LoginStringMark.ToLower())))
                    {
                        buf_del_head();
                        continue;
                    }
                    if (buf_get_head().ToLower().Contains(LoginStringMark.ToLower()))
                    {
                        //telnet_write_foo();
                        //Thread.Sleep(500);
                        telnet_write_line(usr_name);
                        state = 1;
                    }
                }
                if (state == 1)
                {
                    if (buffer_list.Count > 1 && (!buf_get_head().ToLower().Contains(PasswordStringMark.ToLower())))
                    {
                        buf_del_head();
                        continue;
                    }
                    if (buf_get_head().ToLower().Contains(PasswordStringMark.ToLower()))
                    {
                        //Thread.Sleep(500);
                        telnet_write_line(passwd);
                        break;
                    }
                }
                if (tryTimes > 0) { tryTimes--; }
                else return false;
                Thread.Sleep(100);

            }

            Thread.Sleep(1000);
            telnet_write_line(console_string);
            Thread.Sleep(100);
            if (buf_get_all().Contains(PromptMark))
                return true;
            else
                return false;
        }
        #endregion


        #region 登出函数
        public bool logout()
        {
            Thread.Sleep(100);
            telnet_write_line_add("");
            Thread.Sleep(100);
            telnet_write_line_add("");
            Thread.Sleep(100);
            telnet_write_line_add("");

            try
            {
                telnet_write_line_add("exit");
            }
            catch { return false; }

            vxworks.Close();
            //Thread.Sleep(1000);

            buffer = "";
            buffer_list.Clear();
            strWorkingData = "";
            console_string = "";
            return true;
        }
        #endregion


        #region 关闭套接字函数
        public void close()
        {
            try
            {
                vxworks.Close();
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// 调用cmd的ping命令测试某ip是否能通
        /// </summary>
        /// <param name="strIp">目标ip</param>
        /// <returns>ping的结果</returns>
        public bool CmdPing(string strIp)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
            p.StandardInput.WriteLine("ping -n 1 " + strIp);
            p.StandardInput.WriteLine("exit");

            string strRst = p.StandardOutput.ReadToEnd();

            if (strRst.IndexOf("TTL") != -1)
                return true;
            else
                p.Close();
            return false;
        }

        public bool waitPing(string strIp, int time)
        {
            float timeFlag = 0;
            while (true)
            {
                if (true == CmdPing(strIp))
                {
                    return true;
                }

                Thread.Sleep(100);
                timeFlag += 0.1f;
                if (timeFlag > time)
                {
                    return false;
                }
            }
        }

    }
}
