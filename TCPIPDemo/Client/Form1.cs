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

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            btn_Connect.Enabled = false;
            client.Connect(txb_Host.Text, Convert.ToInt32(txb_Port.Text));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client= new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txb_Status.Invoke((MethodInvoker)delegate () {
                txb_Status.Text += e.MessageString;
            });
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            client.WriteLineAndGetReply(txb_Message.Text, TimeSpan.FromSeconds(3));
        }
    }
}
