using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
using OdsReadWrite;

namespace TestOdsReader_P1
{
    public partial class Form1 : Form
    {
        private DataSet data;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //載入excel 
            OpenFileDialog OpenFileDig = new OpenFileDialog();
            //限制副檔名為 *.xls
            //OpenFileDig.Filter = "xls Files|*.xls";
            //OpenFileDig.Filter = "ods files (*.ods)|*.ods|All files (*.*)|*.*"; //ods
            //OpenFileDig.Filter = "ods files (*.ods)|*.ods|xls Files|*.xls"; //ods + xls

            OpenFileDig.Filter = "xls Files|*.xls|ods files (*.ods)|*.ods";
            OpenFileDig.Title = "請選擇匯入檔案";

            if (OpenFileDig.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FileName = OpenFileDig.FileName.ToString();
                    string aa = "";
                    if (FileName.Contains("ods"))
                    {
                        aa = "ods";
                        ImportOdsFile(FileName);
                    }
                    else
                    {
                        aa = "excel";
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
            }
        }

        /// <summary>
        /// 20211102
        /// 匯入ods file
        /// </summary>
        public void ImportOdsFile(string filepath)
        {
            data = new OdsReaderWriter().ReadOdsFile(filepath);

        }


    }
}
