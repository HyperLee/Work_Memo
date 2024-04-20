using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1_測試dg按鈕
{
    public partial class Form1 : Form
    {
        // 要注意要宣告為 static, 否則資料取空
        public static string p_data = "";
        public static string p_reason = "";

        public UserControl1 uc;


        /// <summary>
        /// 新增
        /// 接收 form2 傳送過來資料 與顯示
        /// </summary>
        /// <param name="data"></param>
        public Form1(string data, string reason)
        {
            InitializeComponent();
            // 使用接收到的資料進行初始化
            MessageBox.Show("form1 - 接收 form2 資料: " + data + ", tb: " + reason.ToString().Trim());
            p_data = data;
            p_reason = reason;

        }


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
                table.Columns.Add("Type ", typeof(int));
                table.Columns.Add("Birthday", typeof(DateTime));
                table.Columns.Add("ID ", typeof(int));
                table.Columns.Add("edit", typeof(string));
                table.Columns.Add("Change", typeof(string));
                //table.Columns.Add("button",typeof(DataGridViewButtonColumn));

                uc = new UserControl1();
                uc.setMemberIdNo("ss");

                // Add rows. value
                table.Rows.Add(uc, "Y", 0, DateTime.Now, 1, "--", "變更");
                table.Rows.Add("測試2", "N", 1, DateTime.Now, 2, "--", "變更");
                table.Rows.Add("測試3", "N", 0, DateTime.Now, 3, "--", "變更");
                table.Rows.Add("測試4", "Y", 1, DateTime.Now, 4, "--", "變更");

                return table;
            }
        }


        /// <summary>
        /// init
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // 把創建好資料 塞到 DG顯示
            dataGridView1.DataSource = sampleData();
            // 時間欄位加寬
            dataGridView1.Columns[3].Width = 210;

        }


        /// <summary>
        /// 點選變更 觸發 行為
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 確認點擊的是按鈕列，而不是其他列或表頭
            if (e.ColumnIndex == dataGridView1.Columns["Change"].Index && e.RowIndex >= 0)
            {
                // 在這裡處理按鈕點擊事件，你可以根據需要執行相應的操作
                //MessageBox.Show("按鈕被點擊了，行索引：" + e.RowIndex + ", 列索引: " + e.ColumnIndex);

                // 取出 id 數值 代表不同筆資料
                string _idvalue = "";
                if(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Trim() != "")
                {
                    _idvalue = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
                }
                else
                {
                    _idvalue = "";
                }

                MessageBox.Show("id: " + _idvalue);

                var editForm = new Form2(_idvalue);
                editForm.ShowDialog();

                // 把從 form2 接收到的資料 寫入cell value
                if(p_reason.ToString().Trim() != "")
                {
                    dataGridView1.CurrentRow.Cells["edit"].Value = p_reason;
                }
                else if(p_data.ToString().Trim() != "")
                {
                    dataGridView1.CurrentRow.Cells["edit"].Value = p_data;
                }
                else
                {
                    // 沒接收到 資料寫入 no - data
                    dataGridView1.CurrentRow.Cells["edit"].Value = "no-data";
                }
            }

        }

    }
}
