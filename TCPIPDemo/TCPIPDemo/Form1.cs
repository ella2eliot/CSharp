using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPIPDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            server=new SimpleTcpServer();
            server.Delimiter = 0x13;// enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += ServerDataReceived;
        }

        private void ServerDataReceived(object sender, SimpleTCP.Message e)
        {
            txb_Status.Invoke((MethodInvoker)delegate () {
                txb_Status.Text += e.MessageString;
                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            txb_Status.Text = "";
            txb_Status.Text += "Server starting...";
            btn_Start.Enabled = false;
            //System.Net.IPAddress ip= new System.Net.IPAddress(long.Parse(txb_Host.Text));
            System.Net.IPAddress ip= System.Net.IPAddress.Parse(txb_Host.Text);
            server.Start(ip, Convert.ToInt32(txb_Port.Text));
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                btn_Start.Enabled = true;
                txb_Status.Text += "Server stop...";
                server.Stop();
            }
        }
    }
}
