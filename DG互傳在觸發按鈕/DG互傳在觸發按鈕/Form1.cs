using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DG互傳在觸發按鈕
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 預設數值 測資
        /// DG裡面加入 UserControl1 元件
        /// </summary>
        /// <returns></returns>
        private DataTable sampleData()
        {
            using (DataTable table = new DataTable())
            {
                // Add columns.  head
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Status", typeof(string));
                table.Columns.Add("Type", typeof(int));
                table.Columns.Add("Birthday", typeof(DateTime));
                table.Columns.Add("ID", typeof(int));
                //table.Columns.Add("edit", typeof(string));
                //table.Columns.Add("Change", typeof(string));
                //table.Columns.Add("button",typeof(DataGridViewButtonColumn));

                //table.Columns.Add("Name", typeof(string));

                // Add rows. value
                table.Rows.Add("測試1", "Y", 0, DateTime.Now, 1);
                table.Rows.Add("測試2", "N", 1, DateTime.Now, 2);
                table.Rows.Add("測試3", "N", 0, DateTime.Now, 3);
                table.Rows.Add("測試4", "Y", 1, DateTime.Now, 4);

                //table.Rows.Add("測試1", "Y");

                return table;
            }
        }


        public Form1()
        {
            InitializeComponent();

            // 把創建好資料 塞到 DG顯示
            dataGridView1.DataSource = sampleData();
            // 時間欄位加寬
            //dataGridView1.Columns[3].Width = 210;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var editForm = new Form2(dataGridView1);
            editForm.ShowDialog();
            this.Hide();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var editForm = new Form3(dataGridView1);
            editForm.ShowDialog();
            this.Hide();
            this.Close();
        }
    }
}
