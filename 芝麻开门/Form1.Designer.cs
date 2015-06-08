namespace 芝麻开门
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrCheckWindow = new System.Windows.Forms.Timer(this.components);
            this.tmrDisplay = new System.Windows.Forms.Timer(this.components);
            this.lblConnectTime = new System.Windows.Forms.Label();
            this.lblErrMsg = new System.Windows.Forms.Label();
            this.txturl = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblLastUpdateTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblremainconfirm = new System.Windows.Forms.Label();
            this.lbltodayorderno = new System.Windows.Forms.Label();
            this.lbltotalorderno = new System.Windows.Forms.Label();
            this.lbldaytime = new System.Windows.Forms.Label();
            this.lbltotaltime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrUpdateStatus = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrCheckWindow
            // 
            this.tmrCheckWindow.Interval = 5000;
            this.tmrCheckWindow.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmrDisplay
            // 
            this.tmrDisplay.Interval = 1000;
            this.tmrDisplay.Tick += new System.EventHandler(this.tmrDisplay_Tick);
            // 
            // lblConnectTime
            // 
            this.lblConnectTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnectTime.AutoSize = true;
            this.lblConnectTime.Location = new System.Drawing.Point(13, 629);
            this.lblConnectTime.Name = "lblConnectTime";
            this.lblConnectTime.Size = new System.Drawing.Size(89, 12);
            this.lblConnectTime.TabIndex = 2;
            this.lblConnectTime.Text = "未有客户端连接";
            // 
            // lblErrMsg
            // 
            this.lblErrMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblErrMsg.AutoSize = true;
            this.lblErrMsg.ForeColor = System.Drawing.Color.Firebrick;
            this.lblErrMsg.Location = new System.Drawing.Point(12, 652);
            this.lblErrMsg.Name = "lblErrMsg";
            this.lblErrMsg.Size = new System.Drawing.Size(59, 12);
            this.lblErrMsg.TabIndex = 3;
            this.lblErrMsg.Text = "检查中...";
            // 
            // txturl
            // 
            this.txturl.Location = new System.Drawing.Point(15, 12);
            this.txturl.Name = "txturl";
            this.txturl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txturl.Size = new System.Drawing.Size(355, 21);
            this.txturl.TabIndex = 5;
            this.txturl.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.txturl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(376, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1043, 517);
            this.webBrowser1.TabIndex = 8;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 33);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "<  后退";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(95, 33);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "前进 >";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(175, 33);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(74, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "刷新";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1057, 549);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1049, 523);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "浏览器";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblLastUpdateTime);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.lblremainconfirm);
            this.tabPage2.Controls.Add(this.lbltodayorderno);
            this.tabPage2.Controls.Add(this.lbltotalorderno);
            this.tabPage2.Controls.Add(this.lbldaytime);
            this.tabPage2.Controls.Add(this.lbltotaltime);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1049, 523);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "云端状态";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblLastUpdateTime
            // 
            this.lblLastUpdateTime.Location = new System.Drawing.Point(153, 176);
            this.lblLastUpdateTime.Name = "lblLastUpdateTime";
            this.lblLastUpdateTime.Size = new System.Drawing.Size(53, 12);
            this.lblLastUpdateTime.TabIndex = 12;
            this.lblLastUpdateTime.Text = "00:00:00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "上次数据刷新时间：";
            // 
            // lblremainconfirm
            // 
            this.lblremainconfirm.Location = new System.Drawing.Point(153, 152);
            this.lblremainconfirm.Name = "lblremainconfirm";
            this.lblremainconfirm.Size = new System.Drawing.Size(53, 12);
            this.lblremainconfirm.TabIndex = 10;
            this.lblremainconfirm.Text = "0";
            this.lblremainconfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltodayorderno
            // 
            this.lbltodayorderno.Location = new System.Drawing.Point(153, 128);
            this.lbltodayorderno.Name = "lbltodayorderno";
            this.lbltodayorderno.Size = new System.Drawing.Size(53, 12);
            this.lbltodayorderno.TabIndex = 9;
            this.lbltodayorderno.Text = "0";
            this.lbltodayorderno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltotalorderno
            // 
            this.lbltotalorderno.Location = new System.Drawing.Point(153, 103);
            this.lbltotalorderno.Name = "lbltotalorderno";
            this.lbltotalorderno.Size = new System.Drawing.Size(53, 12);
            this.lbltotalorderno.TabIndex = 8;
            this.lbltotalorderno.Text = "0";
            this.lbltotalorderno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbldaytime
            // 
            this.lbldaytime.Location = new System.Drawing.Point(153, 76);
            this.lbldaytime.Name = "lbldaytime";
            this.lbldaytime.Size = new System.Drawing.Size(53, 12);
            this.lbldaytime.TabIndex = 7;
            this.lbldaytime.Text = "0:00:00";
            this.lbldaytime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbltotaltime
            // 
            this.lbltotaltime.Location = new System.Drawing.Point(153, 52);
            this.lbltotaltime.Name = "lbltotaltime";
            this.lbltotaltime.Size = new System.Drawing.Size(53, 12);
            this.lbltotaltime.TabIndex = 6;
            this.lbltotaltime.Text = "0:00:00";
            this.lbltotaltime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "您好，张三";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "总下单数量：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "待确认订单数量：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "今日下单数量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "今日挂机时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "总挂机时间：";
            // 
            // tmrUpdateStatus
            // 
            this.tmrUpdateStatus.Interval = 1000;
            this.tmrUpdateStatus.Tick += new System.EventHandler(this.tmrUpdateStatus_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 684);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txturl);
            this.Controls.Add(this.lblErrMsg);
            this.Controls.Add(this.lblConnectTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "芝麻开门云端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrCheckWindow;
        private System.Windows.Forms.Timer tmrDisplay;
        private System.Windows.Forms.Label lblConnectTime;
        private System.Windows.Forms.Label lblErrMsg;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblremainconfirm;
        private System.Windows.Forms.Label lbltodayorderno;
        private System.Windows.Forms.Label lbltotalorderno;
        private System.Windows.Forms.Label lbldaytime;
        private System.Windows.Forms.Label lbltotaltime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblLastUpdateTime;
        private System.Windows.Forms.Timer tmrUpdateStatus;
    }
}

