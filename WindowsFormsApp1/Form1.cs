using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using NFTS;

namespace WindowsFormsApp1
{
    public class Form1 : Form
    {
        
        public static Button userBt;
        public static string userId = ".";
        public static string userName;
        public static int userGold;
        public static string userPass = ".";
        public static Dictionary<string, string> uiLibrary = new Dictionary<string, string>();
        private static readonly Dictionary<string, int> uiType = new Dictionary<string, int>();
        public static Dictionary<string, int> uiPrice = new Dictionary<string, int>();
        private static ListBox Box;
        private static CheckBox CB1;
        private static CheckBox CB2;
        private static CheckBox CB3;
        private static CheckBox CB4;
        public static List<string> userList = new List<string>();
        private readonly IContainer components = null;
        private readonly int Edition = 4;
        private readonly Dictionary<string, string> uiComment = new Dictionary<string, string>();
        private readonly Dictionary<string, string> uiDescription = new Dictionary<string, string>();
        private readonly Dictionary<string, int> uiEditionl = new Dictionary<string, int>();
        private readonly Dictionary<int, List<string>> uiImgUrl = new Dictionary<int, List<string>>();
        private readonly Dictionary<string, string> uiNumber = new Dictionary<string, string>();
        private readonly Dictionary<string, string> uiPCNFTS = new Dictionary<string, string>();
        private readonly List<string> ydConfig = new List<string>();
        private Button button1;
        private Button button2;
        private Button button3;
        public Button button4;
        private Button button5;
        private Button button7;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private string cofurl = "";
        private string dirurl = "";
        private string Function;
        private Label label5;
        private ListBox listBox1;
        private string url_url = "mpq";
        private int yd;
        private string ydUrl = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Form5
            {
                Visible = true
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ydUrl == "")
            {
                MessageBox.Show("请选择YDWE位置");
            }
            else if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("请选择一个项目");
            }
            else
            {
                var name = listBox1.SelectedItem.ToString();
                var lib = uiLibrary[name];
                var text = button2.Text;
                if (text == "安装")
                    Install(name, lib);
                else if (text == "卸载")
                    Uninstall(name, lib);
                else if (text != "请安装最新版本") Shop(name, lib);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new Form6
            {
                Visible = true
            };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new Form2
            {
                Visible = true
            };
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ydUrl = "";
            ydConfig.Clear();
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "请选择YDWE",
                Filter = "选择YDWE|YDWE.exe"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                label5.Text = dialog.FileName;
                ydUrl = Path.GetDirectoryName(label5.Text);
            }

            if (ydUrl == "")
            {
                label5.Text = "未选择YDWE路径";
            }
            else
            {
                string str;
                if (File.Exists(ydUrl + "/share/mpq/config"))
                {
                    url_url = "mpq";
                    cofurl = ydUrl + "/share/" + url_url + "/config";
                    dirurl = ydUrl + "/share/" + url_url + "/9527";
                    yd = 1;
                }
                else if (File.Exists(ydUrl + "/share/ui/config"))
                {
                    url_url = "ui";
                    cofurl = ydUrl + "/share/" + url_url + "/config";
                    dirurl = ydUrl + "/share/" + url_url + "/9527";
                    yd = 1;
                }
                else if (File.Exists(ydUrl + "/ui/config"))
                {
                    cofurl = ydUrl + "/ui/config";
                    dirurl = ydUrl + "/ui/9527";
                    yd = 2;
                }

                if (File.Exists(ydUrl + "/plugin/YDTrigger/Function.h"))
                    Function = "/plugin/YDTrigger/Function.h";
                else
                    Function = "/compiler/include/YDTrigger/Function.h";
                var reader = new StreamReader(cofurl);
                while ((str = reader.ReadLine()) != null)
                    if (str.Length > 5 && str.Substring(0, 5) == @"9527\")
                        ydConfig.Add(str.Substring(5));
                reader.Close();
                if (yd == 1)
                    foreach (var str2 in uiEditionl.Keys)
                        if (uiEditionl[str2] == 3 && File.Exists(ydUrl + "/logs/" + uiLibrary[str2] + ".l"))
                            ydConfig.Add(uiLibrary[str2]);
                        else if (yd == 2)
                            foreach (var str3 in uiEditionl.Keys)
                                if (uiEditionl[str3] == 3 && File.Exists(ydUrl + "/ui/" + uiLibrary[str3] + ".l"))
                                    ydConfig.Add(uiLibrary[str3]);
                if (!Directory.Exists(dirurl)) Directory.CreateDirectory(dirurl);
                if (yd == 1 && !Directory.Exists(ydUrl + "/jass/9527")) Directory.CreateDirectory(ydUrl + "/jass/9527");
                UpdataList(sender, e);
            }
        }

        private void CreateFile(string url, string data)
        {
            var stream = new FileStream(url, FileMode.Create);
            var writer = new StreamWriter(stream);
            writer.Write(data);
            writer.Flush();
            writer.Close();
            stream.Close();
        }

        private void DeleteFile(string url)
        {
            if (File.Exists(url)) File.Delete(url);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userBt = button4;
            Box = listBox1;
            CB1 = checkBox1;
            CB2 = checkBox2;
            CB3 = checkBox3;
            CB4 = checkBox4;
            var request = (HttpWebRequest) WebRequest.Create("http://foreverxip.com/PCNFTS/GetList.php");
            var response = (HttpWebResponse) request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            while (!reader.EndOfStream)
            {
                var str = reader.ReadLine();
                if (str != "")
                {
                    var separator = new[] {'|'};
                    var strArray = str.Split(separator);
                    uiType.Add(strArray[0], int.Parse(strArray[1]));
                    uiPrice.Add(strArray[0], int.Parse(strArray[2]));
                    uiLibrary.Add(strArray[0], strArray[3]);
                    uiDescription.Add(strArray[0], strArray[4]);
                    uiComment.Add(strArray[0], strArray[5]);
                    uiPCNFTS.Add(strArray[0], strArray[6]);
                    uiNumber.Add(strArray[0], strArray[7]);
                    uiImgUrl.Add(int.Parse(strArray[7]), new List<string>());
                    uiEditionl.Add(strArray[0], int.Parse(strArray[8]));
                }
            }

            request = (HttpWebRequest) WebRequest.Create("http://foreverxip.com/PCNFTS/GetImage.php");
            response = (HttpWebResponse) request.GetResponse();
            reader = new StreamReader(response.GetResponseStream());
            while (!reader.EndOfStream)
            {
                char[] separator;
                separator = new[] {'|'};
                var strArray2 = reader.ReadLine().Split(separator);
                if (uiImgUrl.ContainsKey(int.Parse(strArray2[0])))
                {
                    var list = uiImgUrl[int.Parse(strArray2[0])];
                    for (var i = 1; i < strArray2.Length; i++)
                        if (strArray2[i] != "")
                            list.Add(strArray2[i]);
                }
            }

            UpdataList(sender, e);
        }

        private string GetHttpData(string lib, string name)
        {
            string[] textArray1;
            textArray1 = new[] {"https://code.csdn.net/snippets/", GetUrl(name), "/master/", lib, "/raw"};
            return GetHttpWebRequest(string.Concat(textArray1));
        }

        private string GetHttpWebRequest(string url)
        {
            var requestUri = new Uri(url);
            var request = (HttpWebRequest) WebRequest.Create(requestUri);
            request.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
            request.Accept = "*/*";
            request.KeepAlive = true;
            request.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            var response = (HttpWebResponse) request.GetResponse();
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            var str = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();
            response.Close();
            return str;
        }

        private string GetUrl(string name)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            string[] textArray1;
            textArray1 = new[] {
                "http://foreverxip.com/PCNFTS/GetUrl.php?id=", userId, "&pass=", userPass, "&lib=", uiLibrary[name]
            };
            if (uiPCNFTS[name] == "9527最帅!!!破解狗吃屎!!!")
            {
                System.GC.Collect();
                var request = (HttpWebRequest) WebRequest.Create(string.Concat(textArray1));
                var response = (HttpWebResponse) request.GetResponse();
                var str = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();
                switch (str)
                {
                    case "1":
                        MessageBox.Show("你需要先登录！");
                        Close();
                        break;

                    case "2":
                        MessageBox.Show("请联系9527购买！");
                        Close();
                        break;

                    case "3":
                        MessageBox.Show("请重新登录！");
                        Close();
                        break;
                }
 
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();

                }
                uiPCNFTS[name] = str;
                return str;
            }

            return uiPCNFTS[name];
        }

        private void InitializeComponent()
        {
            var manager = new ComponentResourceManager(typeof(Form1));
            listBox1 = new ListBox();
            button2 = new Button();
            button4 = new Button();
            button7 = new Button();
            label5 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            button1 = new Button();
            button3 = new Button();
            button5 = new Button();
            SuspendLayout();
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 0x1f);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(260, 0xfb);
            listBox1.TabIndex = 1;
            listBox1.Click += listBox1_Click;
            listBox1.DrawItem += listBox1_DrawItem;
            listBox1.DoubleClick += listBox1_DoubleClick;
            listBox1.Leave += listBox1_Leave;
            button2.Location = new Point(12, 0x125);
            button2.Name = "button2";
            button2.Size = new Size(260, 0x17);
            button2.TabIndex = 2;
            button2.Text = "安装/卸载";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            button4.Location = new Point(12, 0x1ac);
            button4.Name = "button4";
            button4.Size = new Size(260, 0x17);
            button4.TabIndex = 5;
            button4.Text = "登录";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            button7.Location = new Point(12, 0x142);
            button7.Name = "button7";
            button7.Size = new Size(260, 0x17);
            button7.TabIndex = 7;
            button7.Text = "选择YDWE";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            label5.AutoSize = true;
            label5.Location = new Point(10, 0x15c);
            label5.Name = "label5";
            label5.Size = new Size(0x4d, 12);
            label5.TabIndex = 8;
            label5.Text = "选择YDWE路径";
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(12, 9);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(0x30, 0x10);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "事件";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += UpdataList;
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(120, 9);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(0x30, 0x10);
            checkBox2.TabIndex = 10;
            checkBox2.Text = "函数";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += UpdataList;
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(0x42, 9);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(0x30, 0x10);
            checkBox3.TabIndex = 11;
            checkBox3.Text = "动作";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += UpdataList;
            checkBox4.AutoSize = true;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Location = new Point(0xae, 9);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(0x30, 0x10);
            checkBox4.TabIndex = 12;
            checkBox4.Text = "系统";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += UpdataList;
            button1.Location = new Point(12, 0x18f);
            button1.Name = "button1";
            button1.Size = new Size(0x66, 0x17);
            button1.TabIndex = 13;
            button1.Text = "赞助";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button3.Location = new Point(170, 0x18f);
            button3.Name = "button3";
            button3.Size = new Size(0x66, 0x17);
            button3.TabIndex = 14;
            button3.Text = "Bug/建议反馈";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            button5.Enabled = false;
            button5.Location = new Point(170, 370);
            button5.Name = "button5";
            button5.Size = new Size(0x66, 0x17);
            button5.TabIndex = 15;
            button5.Text = "自定义安装";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Aqua;
            ClientSize = new Size(0x11c, 0x1cf);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label5);
            Controls.Add(button7);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = Properties.Resources.Icon;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NFTSv2.1 QQ:969352269";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void Install(string name, string lib)
        {
            string str4;
            button2.Enabled = false;
            button2.Text = "安装中";
            if (yd == 1)
            {
                string[] textArray1;
                textArray1 = new[] {ydUrl, "/share/", url_url, "/9527/", lib};
                if (!Directory.Exists(string.Concat(textArray1)))
                    Directory.CreateDirectory(ydUrl + "/share/" + url_url + "/9527/" + lib);
            }
            else if (yd == 2 && !Directory.Exists(ydUrl + "/ui/9527/" + lib))
            {
                Directory.CreateDirectory(ydUrl + "/ui/9527/" + lib);
            }

            var list = new List<string>();
            var path = "";
            var str2 = "";
            var str3 = "";
            if (yd == 1)
            {
                path = ydUrl + "/jass/9527/" + lib;
                str2 = path;
                string[] textArray3;
                textArray3 = new[] {ydUrl, "/share/", url_url, "/9527/", lib};
                str3 = string.Concat(textArray3);
            }
            else if (yd == 2)
            {
                path = ydUrl + "/ui/9527/" + lib;
                str2 = ydUrl + "/ui/9527/" + lib + "/jass";
                if (!Directory.Exists(str2)) Directory.CreateDirectory(str2);
                str2 = str2 + "/9527";
                if (!Directory.Exists(str2)) Directory.CreateDirectory(str2);
                str2 = str2 + "/" + lib;
                str3 = ydUrl + "/ui/9527/" + lib;
            }

            if (uiEditionl[name] == 4)
            {
                string str5;
                if (!Directory.Exists(ydUrl + "/logs/9527")) Directory.CreateDirectory(ydUrl + "/logs/9527");
                if (!Directory.Exists(ydUrl + "/logs/9527/" + lib))
                    Directory.CreateDirectory(ydUrl + "/logs/9527/" + lib);
                CreateFile(ydUrl + "/logs/" + lib + ".f", GetHttpData(lib + ".f", name));
                var reader3 = new StreamReader(ydUrl + "/logs/" + lib + ".f");
                while ((str5 = reader3.ReadLine()) != null)
                    if (str5 != "")
                        CreateFile(ydUrl + "/logs/9527/" + lib + "/" + str5, GetHttpData(str5, name));
                reader3.Close();
                CreateFile(str2 + ".j", GetHttpData(lib + ".j", name));
                CreateFile(str2 + ".cfg", GetHttpData(lib + ".cfg", name));
            }
            else if (uiEditionl[name] == 3)
            {
                string str6;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                if (yd == 2 && !Directory.Exists(str2)) Directory.CreateDirectory(str2);
                CreateFile(ydUrl + "/logs/" + lib + ".l", GetHttpData(lib + ".l", name));
                var reader4 = new StreamReader(ydUrl + "/logs/" + lib + ".l");
                while ((str6 = reader4.ReadLine()) != null)
                    if (str6 != "")
                    {
                        CreateFile(str2 + "/" + str6 + ".j", GetHttpData(str6 + ".j", name));
                        CreateFile(str2 + "/" + str6 + ".cfg", GetHttpData(str6 + ".cfg", name));
                        list.Add(str6);
                    }

                reader4.Close();
            }
            else
            {
                CreateFile(str2 + ".j", GetHttpData(lib + ".j", name));
                CreateFile(str2 + ".cfg", GetHttpData(lib + ".cfg", name));
            }

            if (uiEditionl[name] == 1 || uiEditionl[name] == 2)
            {
                string str7;
                var list3 = new List<string>();
                var reader5 = new StreamReader(ydUrl + Function);
                while ((str7 = reader5.ReadLine()) != null)
                {
                    if (str7 == "# /*") break;
                    if (str7 != "") list3.Add(str7);
                }

                reader5.Close();
                DeleteFile(ydUrl + Function);
                var stream2 = new FileStream(ydUrl + Function, FileMode.Create);
                var writer2 = new StreamWriter(stream2);
                for (var j = 0; j < list3.Count; j++) writer2.WriteLine(list3[j]);
                if (uiEditionl[name] == 1) writer2.WriteLine(GetHttpData(lib + ".i", name));
                if (uiEditionl[name] == 2) writer2.WriteLine(GetHttpData(lib + ".h", name));
                writer2.Flush();
                writer2.Close();
                stream2.Close();
            }

            CreateFile(str3 + "/action.txt", GetHttpData("action.txt", name));
            CreateFile(str3 + "/call.txt", GetHttpData("call.txt", name));
            CreateFile(str3 + "/define.txt", GetHttpData("define.txt", name));
            CreateFile(str3 + "/event.txt", GetHttpData("event.txt", name));
            var list2 = new List<string>();
            var reader = new StreamReader(cofurl);
            while ((str4 = reader.ReadLine()) != null)
                if (str4 != "")
                    list2.Add(str4);
            reader.Close();
            DeleteFile(cofurl);
            var stream = new FileStream(cofurl, FileMode.Create);
            var writer = new StreamWriter(stream);
            for (var i = 0; i < list2.Count; i++) writer.WriteLine(list2[i]);
            writer.WriteLine(@"9527\" + lib);
            writer.Flush();
            writer.Close();
            stream.Close();
            var request = (HttpWebRequest) WebRequest.Create("http://foreverxip.com/PCNFTS/Install.php?lib=" + lib);
            var response = (HttpWebResponse) request.GetResponse();
            var reader2 = new StreamReader(response.GetResponseStream());
            ydConfig.Add(lib);
            UpdataList2();
            button2.Text = "安装";
            button2.Enabled = true;
            MessageBox.Show("安装成功", name);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            var str = listBox1.SelectedItem.ToString();
            if (uiEditionl[str] > Edition)
            {
                button2.Text = "请安装最新版本";
                button2.Visible = false;
            }
            else
            {
                button2.Visible = true;
                if (!ydConfig.Contains(uiLibrary[str]))
                {
                    if (uiPrice[str] > 0)
                    {
                        if (userList.Contains(uiLibrary[str]))
                            button2.Text = "安装";
                        else
                            button2.Text = "购买(滑稽币：" + uiPrice[str] + ")";
                    }
                    else
                    {
                        button2.Text = "安装";
                    }
                }
                else
                {
                    button2.Text = "卸载";
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var name = listBox1.SelectedItem.ToString();
            var form = new Form3
            {
                Visible = true
            };
            Form3.ShowBox(name, uiDescription[name], uiComment[name], uiImgUrl[int.Parse(uiNumber[name])]);
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var s = listBox1.Items[e.Index].ToString();
            if (!ydConfig.Contains(uiLibrary[s]))
            {
                if (uiPrice[s] > 0)
                {
                    if (userList.Contains(uiLibrary[s]))
                        e.Graphics.DrawString(s, e.Font, new SolidBrush(Color.FromArgb(200, 200, 200)), e.Bounds);
                    else
                        e.Graphics.DrawString(s, e.Font, new SolidBrush(Color.Red), e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(s, e.Font, new SolidBrush(Color.FromArgb(200, 200, 200)), e.Bounds);
                }
            }
            else
            {
                e.Graphics.DrawString(s, e.Font, new SolidBrush(Color.Black), e.Bounds);
            }
        }

        private void listBox1_Leave(object sender, EventArgs e)
        {
        }

        public static void Login(string id, string user, string gold, string pass)
        {
            userId = id;
            userName = user;
            userGold = int.Parse(gold);
            userPass = pass;
            userBt.Text = user + "(滑稽币：" + gold + ")";
            userBt.Enabled = false;
        }

        public static string MD5(string encryptString)
        {
            var bytes = Encoding.Default.GetBytes(encryptString);
            MD5 md = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md.ComputeHash(bytes)).Replace("-", "").ToLower();
        }

        private void Shop(string name, string lib)
        {
            if (userId == "")
            {
                MessageBox.Show("请先登录");
            }
            else if (userGold < uiPrice[name])
            {
                MessageBox.Show("滑稽币不足");
            }
            else
            {
                var request =
                    (HttpWebRequest) WebRequest.Create("http://foreverxip.com/PCNFTS/Shop.php?user=" + userId +
                                                       "&lib=" + lib);
                var response = (HttpWebResponse) request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                if (reader.ReadToEnd() == "0")
                {
                    MessageBox.Show("购买失败");
                }
                else
                {
                    userGold -= uiPrice[name];
                    userList.Add(lib);
                    object objArray1;
                    objArray1 = new IComparable[] {userName, "(滑稽币：", userGold, ")"};
                    userBt.Text = string.Concat(objArray1);
                    UpdataList2();
                }
            }
        }

        private void Uninstall(string name, string lib)
        {
            string str4;
            button2.Enabled = false;
            button2.Text = "卸载中";
            var dictionary = new Dictionary<string, string>();
            var path = "";
            var str2 = "";
            var str3 = "";
            if (yd == 1)
            {
                path = ydUrl + "/jass/9527/" + lib;
                str2 = path;
                string[] textArray1;
                textArray1 = new[] {ydUrl, "/share/", url_url, "/9527/", lib};
                str3 = string.Concat(textArray1);
            }
            else if (yd == 2)
            {
                path = ydUrl + "/ui/9527/" + lib;
                str2 = ydUrl + "/ui/9527/" + lib + "/jass";
                if (!Directory.Exists(str2)) Directory.CreateDirectory(str2);
                str2 = str2 + "/9527/" + lib;
                str3 = ydUrl + "/ui/9527/" + lib;
            }

            if (uiEditionl[name] == 4)
            {
                string str5;
                CreateFile(ydUrl + "/logs/" + lib + ".f", GetHttpData(lib + ".f", name));
                var reader2 = new StreamReader(ydUrl + "/logs/" + lib + ".f");
                while ((str5 = reader2.ReadLine()) != null)
                    if (str5 != "")
                        DeleteFile(ydUrl + "/logs/9527/" + lib + "/" + str5);
                reader2.Close();
                if (Directory.Exists(ydUrl + "/logs/9527/" + lib)) Directory.Delete(ydUrl + "/logs/9527/" + lib);
                DeleteFile(ydUrl + "/logs/" + lib + ".f");
                DeleteFile(str2 + ".j");
                DeleteFile(str2 + ".cfg");
            }
            else if (uiEditionl[name] == 3)
            {
                string str6;
                CreateFile(ydUrl + "/logs/" + lib + ".l", GetHttpData(lib + ".l", name));
                var reader3 = new StreamReader(ydUrl + "/logs/" + lib + ".l");
                while ((str6 = reader3.ReadLine()) != null)
                    if (str6 != "")
                    {
                        DeleteFile(str2 + "/" + str6 + ".j");
                        DeleteFile(str2 + "/" + str6 + ".cfg");
                        dictionary.Add(str6, str6);
                    }

                reader3.Close();
                DeleteFile(ydUrl + "/logs/" + lib + ".l");
                if (yd == 1 && Directory.Exists(path)) Directory.Delete(path);
            }
            else
            {
                DeleteFile(str2 + ".j");
                DeleteFile(str2 + ".cfg");
            }

            if (uiEditionl[name] == 1 || uiEditionl[name] == 2)
            {
                string str7;
                var list2 = new List<string>();
                var reader4 = new StreamReader(ydUrl + Function);
                while ((str7 = reader4.ReadLine()) != null)
                    if (str7 != "")
                    {
                        var strArray = str7.Split(" ".ToCharArray());
                        if (strArray[0] == "#include")
                        {
                            char[] separator;
                            separator = new[] {'/'};
                            char[] chArray2;
                            chArray2 = new[] {'.'};
                            var str8 = strArray[1].Split(separator)[1].Split(chArray2)[0];
                            if (str8 != lib) list2.Add(str7);
                        }
                        else if (strArray[0] == "#define")
                        {
                            char[] separator;
                            separator = new[] {'('};
                            var str9 = strArray[1].Split(separator)[0];
                            if (str9 != lib) list2.Add(str7);
                        }
                    }

                reader4.Close();
                DeleteFile(ydUrl + Function);
                var stream2 = new FileStream(ydUrl + Function, FileMode.Create);
                var writer2 = new StreamWriter(stream2);
                for (var j = 0; j < list2.Count; j++) writer2.WriteLine(list2[j]);
                writer2.Flush();
                writer2.Close();
                stream2.Close();
            }

            DeleteFile(str3 + "/action.txt");
            DeleteFile(str3 + "/call.txt");
            DeleteFile(str3 + "/define.txt");
            DeleteFile(str3 + "/event.txt");
            if (yd == 2)
            {
                if (Directory.Exists(str2)) Directory.Delete(str2);
                if (Directory.Exists(ydUrl + "/ui/9527/" + lib + "/jass/9527"))
                    Directory.Delete(ydUrl + "/ui/9527/" + lib + "/jass/9527");
                if (Directory.Exists(ydUrl + "/ui/9527/" + lib + "/jass"))
                    Directory.Delete(ydUrl + "/ui/9527/" + lib + "/jass");
            }

            if (Directory.Exists(str3)) Directory.Delete(str3);
            var list = new List<string>();
            var reader = new StreamReader(cofurl);
            while ((str4 = reader.ReadLine()) != null)
                if ((str4.Length <= 5 || str4.Substring(5) != lib) && str4 != "")
                    list.Add(str4);
            reader.Close();
            DeleteFile(cofurl);
            var stream = new FileStream(cofurl, FileMode.Create);
            var writer = new StreamWriter(stream);
            for (var i = 0; i < list.Count; i++) writer.WriteLine(list[i]);
            writer.Flush();
            writer.Close();
            stream.Close();
            ydConfig.Remove(lib);
            UpdataList2();
            button2.Text = "卸载";
            button2.Enabled = true;
            MessageBox.Show("卸载成功", name);
        }

        private void UpdataList(object sender, EventArgs e)
        {
            Box.Items.Clear();
            foreach (var str in uiType.Keys)
                if (uiType[str] == 1 && CB3.Checked)
                    Box.Items.Add(str);
                else if (uiType[str] == 2 && CB2.Checked)
                    Box.Items.Add(str);
                else if (uiType[str] == 3 && CB1.Checked)
                    Box.Items.Add(str);
                else if (uiType[str] == 4 && CB4.Checked) Box.Items.Add(str);
        }

        public static void UpdataList2()
        {
            Box.Items.Clear();
            foreach (var str in uiType.Keys)
                if (uiType[str] == 1 && CB3.Checked)
                    Box.Items.Add(str);
                else if (uiType[str] == 2 && CB2.Checked)
                    Box.Items.Add(str);
                else if (uiType[str] == 3 && CB1.Checked)
                    Box.Items.Add(str);
                else if (uiType[str] == 4 && CB4.Checked) Box.Items.Add(str);
        }
    }
}