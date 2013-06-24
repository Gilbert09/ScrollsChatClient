using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ScrollsChatClient
{
    public partial class Form1 : Form
    {
        private string publicKeyXml = "<RSAKeyValue><Modulus>mFCubVhPGG+euHuVQbNObqod/Ji0kRe+oh2OCFR7aV09xYiOklqFQ8jgIgAHvyCcM1JowqfFeJ5jV9up0Lh0eIiv3FPRu14aQS35kMdBLMebSW2DNBkfVsOF3l498WWQS9/THIqIaxbqwRDUxba5btBLTN0/A2y6WWiXl05Xu1c=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public Form1()
        {
            InitializeComponent();
        }

        private TcpClient client = null;
        private NetworkStream ns;
        StreamReader reader;
        StreamWriter writer;

        private Thread pingThread;
        private Thread readThread;

        private bool running = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                running = false;
                writer.Close();
                reader.Close();
                ns.Close();
                client.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogout.Enabled = true;
            btnLogin.Enabled = false;
            btnJoinRoom.Enabled = true;
            if (txtEmail.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please Enter a Email and Password");
                return;
            }
            string encryptedEmail = GetLoginEncrypted(txtEmail.Text);
            string encryptedPassword = GetLoginEncrypted(txtPassword.Text);

            client = new TcpClient();
            client.Connect(IPAddress.Parse("54.208.22.193"), 8081);
            ns = client.GetStream();
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);
            running = true;

            readThread = new Thread(new ThreadStart(ReadStream));
            readThread.Start();

            WriteMessage("{\"msg\":\"SignIn\", \"email\":\"" + encryptedEmail + "\", \"password\":\"" + encryptedPassword + "\"}");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                running = false;
                writer.Close();
                reader.Close(); 
                ns.Close();
                client.Close();
            }
            btnLogout.Enabled = false;
            btnLogin.Enabled = true;
            btnJoinRoom.Enabled = false;
            lblLoggedIn.Text = "Not Logged In";
            tabControl1.TabPages.Clear();
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (txtRoomName.Text.Trim() == "") return;

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
                if (tabControl1.TabPages[i].Text == txtRoomName.Text)
                {
                    txtRoomName.Text = "";
                    return;
                }
            
            WriteMessage("{ \"roomName\" : \"" + txtRoomName.Text + "\", \"msg\" : \"RoomEnter\" }");
            txtRoomName.Text = "";
        }

        private void TabPageButton_Click(object sender, EventArgs e)
        {
            TabPage tp = (TabPage)((Button)sender).Parent;
            TextBox tb = (TextBox)tp.Controls[tp.Text + "SendBox"];
            string msg = tb.Text;
            WriteMessage("{ \"text\" : \"" + msg + "\", \"roomName\" : \"" + tp.Text + "\", \"msg\" : \"RoomChatMessage\" }");
            tb.Text = "";
        }

        private void TabPageChat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TabPage tp = (TabPage)((TextBox)sender).Parent;
                Button b = (Button)tp.Controls[tp.Text + "Button"];
                b.PerformClick();
            }
        }

        private void txtRoomName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnJoinRoom.PerformClick();
            }
        }

        private void ReadStream()
        {
            while (running)
            {
                try
                {
                    string msg = reader.ReadLine();
                    if (msg == null) continue;
                    if (msg.Trim() == "") continue;
                    var t = new Thread(() => ProcessMessage(msg));
                    t.Start();
                }
                catch { }
            }
        }

        private void WriteMessage(string message)
        {
            writer.Write(message);
            writer.Flush();
        }

        private void ProcessMessage(string message)
        {
            if (message == null) return;
            if (message.Trim() == "") return;
            StandardJsonResponse o = JsonConvert.DeserializeObject<StandardJsonResponse>(message);
            string msg = o.msg;

            if (msg == "ServerInfo")
            {
                //Do nothing for now
            }
            else if (msg == "Ok")
            {
                Op op = JsonConvert.DeserializeObject<Op>(message);
                if (op.op == "SignIn")
                {
                    pingThread = new Thread(new ThreadStart(PingServer));
                    pingThread.Start();
                    lblLoggedIn.Invoke((MethodInvoker)delegate()
                    {
                        lblLoggedIn.Text = "Logged In";
                    });
                }
                else if (op.op == "RoomChatMessage")
                {

                }
                else if (op.op == "RoomExit")
                {

                }
            }
            else if (msg == "ProfileInfo")
            {
                //Do nothing for now
            }
            else if (msg == "ProfileDataInfo")
            {
                //Do nothing for now
            }
            else if (msg == "RoomEnter")
            {
                RoomEnter roomEnter = JsonConvert.DeserializeObject<RoomEnter>(message);
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                    if (tabControl1.TabPages[i].Text == roomEnter.roomName)
                    {
                        txtRoomName.Text = "";
                        return;
                    }

                tabControl1.Invoke((MethodInvoker)delegate()
                {
                    TabPage tp = new TabPage(roomEnter.roomName);
                    tp.Name = roomEnter.roomName + "Page";
                    tp.Size = new Size(516, 420);

                    ListBox listBox = new ListBox();
                    listBox.Name = roomEnter.roomName + "ListBox";
                    listBox.Size = new Size(120, 420);
                    listBox.Location = new Point(0, 0);
                    tp.Controls.Add(listBox);

                    RichTextBox chatBox = new RichTextBox();
                    chatBox.Name = roomEnter.roomName + "ChatBox";
                    chatBox.Multiline = true;
                    chatBox.Location = new Point(127, 4);
                    chatBox.Size = new Size(383, 381);
                    tp.Controls.Add(chatBox);

                    TextBox sendBox = new TextBox();
                    sendBox.Name = roomEnter.roomName + "SendBox";
                    sendBox.Location = new Point(127, 393);
                    sendBox.Size = new Size(305, 20);
                    sendBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TabPageChat_KeyUp);
                    tp.Controls.Add(sendBox);

                    Button button = new Button();
                    button.Name = roomEnter.roomName + "Button";
                    button.Text = "Send";
                    button.Location = new Point(438, 391);
                    button.Size = new Size(75, 23);
                    button.Click += new System.EventHandler(this.TabPageButton_Click);
                    tp.Controls.Add(button);

                    tabControl1.TabPages.Add(tp);
                });
            }
            else if (msg == "RoomExit")
            {
                RoomExit roomExit = JsonConvert.DeserializeObject<RoomExit>(message);
            }
            else if (msg == "RoomInfo")
            {
                RoomInfo roomInfo = JsonConvert.DeserializeObject<RoomInfo>(message);
                tabControl1.Invoke((MethodInvoker)delegate()
                {
                    while (true)
                    {
                        try
                        {
                            ListBox list = (ListBox)this.Controls.Find(roomInfo.roomName + "ListBox", true)[0];
                            if (list.Items.Count == roomInfo.profiles.Count) return;

                            list.Items.Clear();
                            foreach (Profile p in roomInfo.profiles)
                            {
                                list.Items.Add(p.name);
                            }

                            break;

                        }
                        catch { }
                    }
                });

            }
            else if (msg == "RoomChatMessage")
            {
                RoomChatMessage roomChatMessage = JsonConvert.DeserializeObject<RoomChatMessage>(message);
                tabControl1.Invoke((MethodInvoker)delegate()
                {
                    while (true)
                    {
                        try
                        {
                            RichTextBox textBox = (RichTextBox)this.Controls.Find(roomChatMessage.roomName + "ChatBox", true)[0];
                            int length = textBox.Text.Length;
                            textBox.AppendText(roomChatMessage.from + ": " + roomChatMessage.text + "\r\n");
                            textBox.Select(length, roomChatMessage.from.Length);
                            textBox.SelectionFont = new Font(textBox.Font, FontStyle.Bold);
                            textBox.ScrollToCaret();
                            break;
                        }
                        catch { Application.DoEvents(); }
                    }
                });
            }
        }

        private void PingServer()
        {
            while (running)
            {
                WriteMessage("{ \"msg\" : \"Ping\" }");
                Thread.Sleep(10000);
            }
        }

        public string GetLoginEncrypted(string value)
        {
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
            ((RSA)cryptoServiceProvider).FromXmlString(publicKeyXml);
            return Convert.ToBase64String(cryptoServiceProvider.Encrypt(Encoding.ASCII.GetBytes(value), false));
        }
    }
}