using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegularExpression
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ans = false;
            string ID=textBox1.Text;
            char c=Convert.ToChar(ID.Substring(0,1).ToLower());

            if (!(ID.Length != 10))            
                if(c>='a'&& c<='z')
                    if (ID.Substring(1, 1) == "1" || ID.Substring(1, 1) == "2")
                    {
                        int ii;
                        if(int.TryParse(ID.Substring(2),out ii))
                            ans=true;
                    }            
            if (ans)
                label1.Text = "正確";
            else
                label1.Text = "錯誤";            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //*代表{0,}    +代表{1,}   ?代表{0,1} .代表任意字元
            //小括弧()表示群組 大寫表示不包含 小寫表示包含
            //Regex reg = new Regex("^[ACE][XYZ][0-9]$"); 
            //Regex reg = new Regex(@"^[A-Za-z][12]\d{8}$"); //身分證
            //Regex reg = new Regex(@"^\d{4}[-\./][01]?[0-9]\d{1}[-\./][0-3]?\d{1}$"); //輸入日期
            //Regex reg = new Regex(@"^\d{4}([-\./])[01]?[0-9]\d{1}\1[0-3]?\d{1}$"); // \1表示同前面第一個群組
            Regex reg = new Regex(@"^([A-Za-z0-9]+)[@]\1[\.]\1[\.]\1$"); 

            Match ans=reg.Match(textBox1.Text);
            if(ans.Success)
                label1.Text="正確";
            else 
                label1.Text="錯誤";

        }
    }
}
