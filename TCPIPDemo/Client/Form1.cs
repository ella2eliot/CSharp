//using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication;
using HslCommunication.ModBus;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // SimpleTcpClient
        // SimpleTcpClient client;
        ModbusTcpNet busTcpClient;
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            btn_Connect.Enabled = false;
            // SimpleTcpClient
            // client.Connect(txb_Host.Text, Convert.ToInt32(txb_Port.Text));

            busTcpClient.IpAddress = txb_Host.Text;
            busTcpClient.Port = Convert.ToInt32(txb_Port);
            busTcpClient.ConnectServer();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // SimpleTcpClient
            //client = new SimpleTcpClient();
            //client.StringEncoder = Encoding.UTF8;
            //client.DataReceived += Client_DataReceived;

            busTcpClient = new ModbusTcpNet();
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txb_Status.Invoke((MethodInvoker)delegate () {
                txb_Status.Text += e.MessageString;
            });
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            // SimpleTcpClient
            // client.WriteLineAndGetReply(txb_Message.Text, TimeSpan.FromSeconds(3));
            // var result=busTcpClient.Write(txb_Message.Text);
        }
    }
}
