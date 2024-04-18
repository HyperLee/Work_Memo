using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinFormsApp1_測試dg按鈕
{
    public partial class Form2 : Form
    {
        public string Column_data = "";
        public Form2()
        {
            InitializeComponent();



        }


        /// <summary>
        /// 接收form1 的 id 欄位數值
        /// </summary>
        /// <param name="data"></param>
        public Form2(string data)
        {
            InitializeComponent();

            Column_data = data;

            label1.Text = "form1 - id: " + Column_data;

        }


        /// <summary>
        /// 觸發按鈕後
        /// 把資料傳輸回去給 form1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string output = "";

            if (Column_data == "1")
            {
                output = "a";
            }
            else if (Column_data == "2")
            {
                output = "b";
            }
            else if (Column_data == "3")
            {
                output = "c";
            }
            else if (Column_data == "4")
            {
                output = "d";
            }
            else if (Column_data == "5")
            {
                output = "e";
            }


            Form1 form1 = new Form1(Convert.ToChar(int.Parse(Column_data) + 64).ToString(), textBox1.Text);
            //form1.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int length = textBox1.Text.Length;

            if(length > 30)
            {
                MessageBox.Show("最多只能輸入30個字");
            }
        }
    }
}
