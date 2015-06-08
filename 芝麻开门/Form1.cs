using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace 芝麻开门
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd,int WM_CHAR,int wParam,int lParam);
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(
            IntPtr hWnd,
            StringBuilder lpString,
            int nMaxCount
        );
        private delegate void delSetText(Control p_obj, string str);
        private delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);
        [DllImport("user32.dll")]
        private static extern int EnumWindows
            (WNDENUMPROC lpEnumFunc, int lParam);
        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern bool EnumChildWindows(IntPtr hwndParent, WNDENUMPROC lpEnumFunc, int lParam);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern int SetForegroundWindow(IntPtr hwnd);//激活窗口
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        private string currentAnyDeskId="";
        private string currentUserId = "";
        private string currentRemoteUserId = "";
        private incomingUser incomingUserInfo=null;
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread ithread = new Thread(startAnyDesk);
            ithread.Start();
            button2.Enabled = false;//浏览按钮关闭
            string strFullPath = Application.ExecutablePath;
            string strFileName = System.IO.Path.GetFileName(strFullPath).ToLower();
            caculatWindows();
            this.tmrCheckWindow.Enabled = true;
            //this.WindowState = FormWindowState.Maximized;
            //this.TopMost = true;
            this.webBrowser1.Navigate("http://zmkm.chensi.org.cn/serverlogin.aspx");
        }
        private string currentConnectStatus = "";
        private int ChildHandleCount = 0;
        private RemoteInfo currentConnection;
        private int connectCountDown;
        private cWindows currentWindows;//记录当前有多少个相关窗口
        private bool bolSoftwareReady=false;
        private delegate void setLoginParameters();
        private string GetMD5(string p_input)
        {
            string rtnvalue;
            rtnvalue = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(p_input, "MD5");
            return rtnvalue;
        }
        private string caculateToken(string utctime)
        {
            return GetMD5(utctime + "|81451505");
        }

        public void startAnyDesk()
        {
            Process[] allProcess = Process.GetProcesses();
            Process[] anydeskProcess = Process.GetProcessesByName("AnyDesk");
            for (int i = 0; i < anydeskProcess.Length; i++)
                anydeskProcess[i].Kill();
            anydeskProcess = Process.GetProcessesByName("anyD");
            for (int i = 0; i < anydeskProcess.Length; i++)
                anydeskProcess[i].Kill();
            SetText(lblConnectTime,"正在初始化系统..步骤1/4");
            RunCMD("anyD.exe --install \"c:\\anyD\" --start-with-win﻿﻿ --silent ﻿--remove-first");
            SetText(lblConnectTime, "正在初始化系统..步骤2/4");
            RunCMD("echo 54FF!3E6C | anyD.exe --set-password");
            SetText(lblConnectTime, "正在初始化系统..步骤3/4");
            RunCMD("anyD.exe --get-id >serverid.txt");
            SetText(lblConnectTime, "正在初始化系统..步骤4/4");
            //打开serverid.txt
            currentAnyDeskId = "";
            System.IO.StreamReader sr = new System.IO.StreamReader("serverid.txt");
            string strMessage=sr.ReadToEnd();
            if (isNumberic(strMessage))
            {
                currentAnyDeskId = strMessage;
                while (!webDocCompleted)
                {
                    Thread.Sleep(200);
                }
                setLoginParameters slp = new setLoginParameters(SetParametersForLogin);
                this.Invoke(slp);
                SetText(lblConnectTime, "系统初始化完成");
            }
            if (currentAnyDeskId=="")
            {
                MessageBox.Show("无法正确启动远程工具，请重新安装本程序。","芝麻开门");
                Process.GetCurrentProcess().Kill();
            }
        }

        private void RunCMD(string cmd)
        {
            //创建实例
            Process p = new Process();
            //设定调用的程序名，不是系统目录的需要完整路径
            p.StartInfo.FileName = "cmd.exe";
            //传入执行参数
            //p.StartInfo.Arguments = " " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            //启动
            p.Start();
            p.StandardInput.WriteLine(cmd);
            p.StandardInput.WriteLine("exit");
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            p.ErrorDataReceived += new DataReceivedEventHandler(SortErrorHandler);
            p.WaitForExit();
            p.Close();
        }

        private void SetParametersForLogin()
        {
            string webpagename = webBrowser1.Url.ToString();
            HtmlDocument doc = webBrowser1.Document;
            webpagename = webpagename.Substring(webpagename.LastIndexOf("/") + 1, webpagename.Length - webpagename.LastIndexOf("/") - 1);
            if (webpagename == "serverlogin.aspx")
            {
                //替换其中的machinecode
                object[] objects = new object[1];
                objects[0] = currentAnyDeskId;
                webBrowser1.Document.InvokeScript("setAnyDeskId", objects);
            }
        }

        protected bool isNumberic(string message)
        {
            //判断是否为整数字符串
            //是的话则将其转换为数字并将其设为out类型的输出值、返回true, 否则为false
            Int32 result = -1;   //result 定义为out 用来输出值
            try
            {
                //当数字字符串的为是少于4时，以下三种都可以转换，任选一种
                //如果位数超过4的话，请选用Convert.ToInt32() 和int.Parse()
                result = Convert.ToInt32(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        private void SortOutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
        {
            
        }

        private void SortErrorHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
        {
            
        }
        

        private void TrytoAccept()
        {
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                StringBuilder sb = new StringBuilder(256);
                //get hwnd
                GetWindowText(hWnd, sb, sb.Capacity);
                if (sb.ToString().ToLower().IndexOf("anydesk (") >= 0)
                {
                    //寻找anydesk的弹出窗口，并点击接受
                    //激活窗口
                    ShowWindow(hWnd, 1);
                    SetForegroundWindow(hWnd);
                    ChildHandleCount = 0;
                    currentConnectStatus = "REMOTECONNECTING";
                    currentConnection = new RemoteInfo();
                    EnumChildWindows(hWnd, new WNDENUMPROC(acceptRemoteConnection), 0);
                    return false;
                }
                return true;
            }, 0);
        }
        private void TrytoRefuse()
        {
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                StringBuilder sb = new StringBuilder(256);
                //get hwnd
                GetWindowText(hWnd, sb, sb.Capacity);
                if (sb.ToString().ToLower().IndexOf("anydesk (") >= 0)
                {
                    //寻找anydesk的弹出窗口，准备拒绝
                    //激活窗口
                    ShowWindow(hWnd, 1);
                    SetForegroundWindow(hWnd);
                    ChildHandleCount = 0;
                    currentConnectStatus = "REMOTEENDING";
                    currentConnection = new RemoteInfo();
                    EnumChildWindows(hWnd, new WNDENUMPROC(refuseRemoteConnection), 0);
                    return false;
                }
                return true;
            }, 0);
        }

        private void caculatWindows()
        {
            currentWindows = new cWindows();
            //this.textBox1.Text = "";
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                StringBuilder sb = new StringBuilder(256);
                //get hwnd
                GetWindowText(hWnd, sb, sb.Capacity);
                if (sb.ToString().ToLower().IndexOf("anydesk (") >= 0)
                {
                    currentWindows.countofAnyDesk++;
                }
                if (sb.ToString().ToLower().IndexOf("阿里旺旺") >= 0)
                {
                    currentWindows.countofWangwang++;
                }
                /*
                if (sb.ToString() != "")
                    this.textBox1.Text += sb.ToString() + Environment.NewLine;*/
                return true;
            }, 0);
        }
        
        private bool refuseRemoteConnection(IntPtr handle, int lparam)
        {
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(handle, sb, sb.Capacity);
            if (sb.ToString() != "")
                ChildHandleCount++;
            if (ChildHandleCount == 3)
            {
                //记录连接的名字
                currentConnection.windowstatus = sb.ToString();
            }
            if (sb.ToString() == "结束" && currentConnection.windowstatus=="正与您的电脑相连接。")
            {
                try
                {
                    //连接超时，将现有连接关闭
                    SendMessage(handle, 0x00F5, 0, 0);
                    currentConnectStatus = "";
                    currentConnection = null;//已断开
                    //通知服务器端关闭连接
                    if (incomingUserInfo != null)
                    {
                        string utctime = getUTCTime().ToString();
                        string strurl = "http://zmkm.chensi.org.cn/zmkmservice.asmx/updateConnectStatus?userhistoryid=" + incomingUserInfo.userHistoryId + "&utctime=" + utctime + "&token=" + caculateToken(utctime);
                        string strmessage = GetDataFromMarket(strurl);
                        incomingUserInfo = null;
                    }
                    this.lblConnectTime.Text = "未有客户端连接";
                }
                catch (Exception ex)
                {
                    //软件出现错误，比如anyDesk被关闭
                    MessageBox.Show("无法正确启动远程工具，请重新安装本程序。", "芝麻开门");
                    Process.GetCurrentProcess().Kill();
                }
            }
            return true;
        }

        private bool acceptRemoteConnection(IntPtr handle, int lparam)
        {
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(handle, sb, sb.Capacity);
            if (sb.ToString() != "")
                ChildHandleCount++;
            if (ChildHandleCount == 1)
            {
                //记录连接的名字
                currentConnection.nickname = sb.ToString();
            }
            if (ChildHandleCount == 2)
            {
                //记录连接的名字
                currentConnection.anydeskid = sb.ToString();
            }
            if (ChildHandleCount == 3)
            {
                //记录连接的名字
                currentConnection.windowstatus = sb.ToString();
            }
            if (sb.ToString() == "接受" && currentConnection.windowstatus == "希望与您的电脑进行连接。")
            {
                try
                {
                    SendMessage(handle, 0x00F5, 0, 0);
                    currentConnectStatus = "REMOTECONNECTACCEPT";
                    //这里需要根据anydeskid来获取它的其他信息.如shopid
                    string utctime = getUTCTime().ToString();
                    string strurl =
                        "http://zmkm.chensi.org.cn/zmkmservice.asmx/userConnectSucceed?userid=" + currentUserId + "&utctime=" + utctime + "&token=" + caculateToken(utctime);
                    string strmessage = GetDataFromMarket(strurl);
                    try
                    {
                        incomingUserInfo = JsonConvert.DeserializeObject<incomingUser>(strmessage);
                        currentConnection.countdownTimer = 240;//12代表一分钟
                        connectCountDown = currentConnection.countdownTimer * 5;
                    }
                    catch
                    {
                        errorinfo ei = JsonConvert.DeserializeObject<errorinfo>(strmessage);
                        //这里说明返回的值不对,短期内就断开它
                        currentConnection.countdownTimer = 2;//10秒就断开
                        connectCountDown = 10;
                    }
                    tmrDisplay.Enabled = true;
                    
                }
                catch (Exception ex)
                {
                    //软件出现错误，比如anyDesk被关闭
                    MessageBox.Show("无法正确启动远程工具，请重新安装本程序。", "芝麻开门");
                    Process.GetCurrentProcess().Kill();
                }
            }
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            caculatWindows();
            //这是一个检查Windows的Timer
            
            if (currentWindows.countofWangwang<2)
            {
                lblErrMsg.Text = "您至少需要开启两个旺旺才能使用本软件的功能";
                return;
            }
            
            bolSoftwareReady = true;
            if (currentWindows.countofAnyDesk == 0)
            {
                lblErrMsg.Text = "未发现远程连接";
                return;
            }
            
            lblErrMsg.Text = "运行中...";
            switch (currentConnectStatus)
            {
                case "":
                    TrytoAccept();
                    break;
                case "REMOTECONNECTACCEPT":
                    currentConnection.countdownTimer--;
                    if (currentConnection.countdownTimer == 0)
                        TrytoRefuse();//1分钟后结束
                    break;
            }
        }

        private void tmrDisplay_Tick(object sender, EventArgs e)
        {
            string cdString;
            int minutecount = connectCountDown / 60;
            int secondcount = connectCountDown % 60;
            string strMinute;
            string strSecond;
            if (minutecount < 10)
                strMinute = "0" + minutecount.ToString();
            else
                strMinute = minutecount.ToString();
            if (secondcount < 10)
                strSecond = "0" + secondcount.ToString();
            else
                strSecond = secondcount.ToString();
            cdString = strMinute + ":" + strSecond;
            this.lblConnectTime.Text = "本次连接将在" + cdString + "后断开";
            connectCountDown--;
            if (connectCountDown < 0)
                tmrDisplay.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {   
            if (e.KeyValue == 13)
                button2_Click(null,null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!bolSoftwareReady)
            {
                return;
            }
            if (txturl.Text.IndexOf("zmkm.chensi.org.cn") >= 0)
            {
                txturl.Text = "http://www.taobao.com";
                this.webBrowser1.Navigate(this.txturl.Text);
            }
            if (txturl.Text!="")
            {
                this.webBrowser1.Navigate(this.txturl.Text);
            }
                
        }
        private bool webDocCompleted = false;
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //将所有的链接的目标，指向本窗体
            webDocCompleted = true;
            foreach (HtmlElement archor in this.webBrowser1.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }
            //将所有的FORM的提交目标，指向本窗体
            foreach (HtmlElement form in this.webBrowser1.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }
            if (webBrowser1.Url.ToString().ToLower().IndexOf("http://zmkm.chensi.org.cn/serverlogin.aspx") >= 0 )
            {
                //替换其中的machinecode
                object[] objects = new object[1];
                objects[0] = currentAnyDeskId;
                webBrowser1.Document.InvokeScript("setAnyDeskId", objects);
            }
            if (webBrowser1.Url.ToString().ToLower().IndexOf("http://trade.tmall.com/order/confirm_goods.htm") >= 0)
            {
                //确认收货,该项操作无需远端登陆即可进行
                string orderid = "";
                int start, end;
                start = webBrowser1.Url.ToString().ToLower().IndexOf("biz_order_id=") + "biz_order_id=".Length;
                end = webBrowser1.Url.ToString().ToLower().IndexOf("&", start);
                if (end < 0)
                    end = webBrowser1.Url.ToString().Length;
                orderid = webBrowser1.Url.ToString().Substring(start, end - start);
                //访问数据库，更新orderid
            }
            //如果当前没有连接。就不处理了
            
            //如果不是买本店铺，马上结束
            if (webBrowser1.Url.ToString().ToLower().IndexOf("http://buy.taobao.com/auction/buy_now.jhtml") >= 0)
            {
                int start, end;
                string strmessage=webBrowser1.DocumentText;
                start = strmessage.IndexOf("orderData");
                start = strmessage.IndexOf("orderInfo",start);
                start = strmessage.IndexOf("shopUrl", start);
                start = strmessage.IndexOf("shop_id=", start) + "shop_id=".Length;
                end = strmessage.IndexOf("\"", start);
                string targetshopid = strmessage.Substring(start, end - start);
                if (targetshopid != incomingUserInfo.incomingShopId)//判断是否未注册店铺id，这里随便写一个做测试
                {
                    MessageBox.Show("禁止购买其他店铺物品","提示");
                    webBrowser1.Navigate("http://www.taobao.com");
                    return;
                }
            }
            if (webBrowser1.Url.ToString().ToLower().IndexOf("http://trade.taobao.com/trade/pay_success.htm?biz_order_id=") >= 0)
            {
                string orderid = "";
                string orderprice = "";
                int start, end;
                start = webBrowser1.Url.ToString().ToLower().IndexOf("biz_order_id=") + "biz_order_id=".Length;
                end = webBrowser1.Url.ToString().ToLower().IndexOf("&", start);
                if (end < 0)
                    end = webBrowser1.Url.ToString().Length;
                orderid = webBrowser1.Url.ToString().Substring(start, end - start);
                if (incomingUserInfo != null && !currentConnection.orderid.Contains(orderid))
                {
                    //购买成功，记录Orderid
                    // 您已成功付款<em>25.00</em>元
                    System.IO.StreamReader sr = new System.IO.StreamReader(webBrowser1.DocumentStream, System.Text.Encoding.GetEncoding(936));
                    string strMessage = sr.ReadToEnd();
                    start = strMessage.IndexOf("您已成功付款<em>") + "您已成功付款<em>".Length;
                    end = strMessage.IndexOf("</em>", start);
                    if (start < 0 || end < start)
                        return;
                    orderprice = strMessage.Substring(start, end - start);
                    string wwid;
                    strMessage = webBrowser1.Document.GetElementById("J_SiteNavBd").InnerHtml;
                    start = strMessage.IndexOf(">", strMessage.IndexOf("http://i.taobao.com/my_taobao.htm?ad_id="))+1;
                    end = strMessage.IndexOf("<", start);
                    wwid = strMessage.Substring(start, end - start);
                    string utctime = getUTCTime().ToString();
                    string strurl = "http://zmkm.chensi.org.cn/zmkmservice.asmx/addUserOrder?userid=" + incomingUserInfo.incomingUserId;
                    strurl += "&orderid=" + orderid;
                    strurl += "&orderprice=" + orderprice;
                    strurl += "&ordertaobaoid="+wwid;
                    strurl += "&remoteuserid=" + currentUserId + "&utctime=" + utctime + "&token=" + caculateToken(utctime);
                    currentConnection.orderid.Add(orderid);
                }
                

                //zmkmservice.asmx/addUserOrder
                //?userid=string&orderid=string&orderprice=string&ordertaobaoid=string&remoteuserid=string&utctime=string&token=string
                
                // public void addUserOrder(
                // string userid, string orderid, string orderprice,
                // string ordertaobaoid, string remoteuserid,
                // string utctime, string token)
                
            }
            
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            webDocCompleted = false;
            string currenturl = webBrowser1.Url.ToString();
            //this.txturl.Text = webBrowser1.Url.ToString();
            SetText(this.txturl, currenturl);
            string pagename;
            pagename = currenturl.Substring(currenturl.LastIndexOf("/") + 1, currenturl.Length - currenturl.LastIndexOf("/") - 1);
            if (pagename == "serverlogin.aspx" && currentAnyDeskId == "")
            {
                MessageBox.Show("请等待系统初始化完成", "芝麻开门");
                e.Cancel = true;
            }
            /*
            if (currenturl == "serverlogin.aspx" && bolSoftwareReady == false)
            {
                MessageBox.Show("请确保开启两个或以上的小号才能使用该系统", "芝麻开门");
                e.Cancel = true;
            }*/
            
            if (currenturl.IndexOf("http://www.taobao.com/?msg=") >= 0)
            {
                string strMessage;
                int start, end;
                start = currenturl.IndexOf("http://www.taobao.com/?msg=") + "http://www.taobao.com/?msg=".Length;
                end = currenturl.Length;
                strMessage = currenturl.Substring(start, end - start);
                userlogon ul =
                        JsonConvert.DeserializeObject<userlogon>(strMessage);
                currentUserId = ul.userid;//此处已经成功登陆
                /*
                webBrowser1.Url = new Uri("http://www.taobao.com");
                button2.Enabled = true;
                tmrUpdateStatus.Enabled = true;*/
            }

            /*
            if (webBrowser1.Url.ToString().IndexOf("http://zmkm.chensi.org.cn/")<0)
            {
                if (bolSoftwareReady && currenturl.IndexOf("http://www.taobao.com/?msg=") < 0)
                    this.txturl.Text = webBrowser1.Url.ToString();
                else
                    this.txturl.Text = "http://www.taobao.com";
            }
            else
                this.txturl.Text = "http://www.taobao.com";*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (bolSoftwareReady)
                webBrowser1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (bolSoftwareReady)
                webBrowser1.GoBack();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bolSoftwareReady)
                webBrowser1.GoForward();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            MessageBox.Show("请勿关闭挂机端，未挂满足够市场的用户将无法使用芝麻开门的其他功能","警告");
            e.Cancel = true;*/
        }
        public static long getUTCTime()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        private bool bolStartGJ = false;
        private long gjtime=0;//挂机的时间
        private int GjTimerCount=0;
        private void tmrUpdateStatus_Tick(object sender, EventArgs e)
        {
            //Use it to Refresh the Server State
            if (bolStartGJ==false)
            {
                string utctime = getUTCTime().ToString();
                string strurl = "http://zmkm.chensi.org.cn/zmkmservice.asmx/getServerStatus?userid=" + currentUserId + "&utctime=" + utctime + "&token=" + caculateToken(utctime);
                string strmessage = GetDataFromMarket(strurl);
                if (strmessage != "")
                {
                    ServerStatus ss = JsonConvert.DeserializeObject<ServerStatus>(strmessage);
                    this.lbltotaltime.Text = ss.totalgjtime;
                    this.lbldaytime.Text = ss.todaygjtime;
                    this.lbltotalorderno.Text = ss.totalorderno;
                    this.lbltodayorderno.Text = ss.todayorderno;
                    this.lblremainconfirm.Text = ss.remainconfirmorderno;
                    this.lblLastUpdateTime.Text = DateTime.Now.ToLongTimeString();
                    if (bolStartGJ == false)
                    {
                        gjtime = getUTCTime();
                        bolStartGJ = true;
                    }
                }
            }
            if (bolStartGJ)
            {
                long timespan = getUTCTime() - gjtime;
                gjtime = getUTCTime();
                DateTime totalgjtime = DateTime.Parse(this.lbltotaltime.Text).AddSeconds(timespan);
                DateTime todaygjtime = DateTime.Parse(this.lbldaytime.Text).AddSeconds(timespan);
                this.lbltotaltime.Text = totalgjtime.ToLongTimeString();
                this.lbldaytime.Text = todaygjtime.ToLongTimeString();
            }
            GjTimerCount++;
            if (GjTimerCount>5)
                this.lblConnectTime.Text = "下一次心跳包发送在" + (10 - GjTimerCount);
            if (GjTimerCount%10==0)
            {
                //发送心跳包
                string utctime = getUTCTime().ToString();
                string strurl = "http://zmkm.chensi.org.cn/zmkmservice.asmx/sendHeartBeat?userid=" + currentUserId + 
                    "&utctime=" + utctime + "&token=" + caculateToken(utctime);
                this.lblConnectTime.Text = strurl;
                string strmessage = GetDataFromMarket(strurl);//发送心跳包
                if (strmessage.IndexOf("errcode")>=0)
                {
                    this.webBrowser1.Navigate("http://zmkm.chensi.org.cn/login.aspx");
                }
                GjTimerCount = 0;
            }
        }
        
        private string GetDataFromMarket(string strurl)
        {
            try
            {
                HttpWebRequest weq = (HttpWebRequest)HttpWebRequest.Create(strurl);
                HttpWebResponse wer = (HttpWebResponse)weq.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(wer.GetResponseStream());
                string strMessage = sr.ReadToEnd();
                return strMessage;
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        private void SetText(Control p_obj,string s)
        {
            if (p_obj.InvokeRequired)
            {
                delSetText setTextInvoker = new delSetText(SetText);
                this.Invoke(setTextInvoker, new object[] { p_obj, s });
            }
            else
            {
                p_obj.Text = s;
            }
        }
        
    }

}
