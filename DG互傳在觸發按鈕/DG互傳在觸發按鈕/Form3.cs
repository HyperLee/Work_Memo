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
    public partial class Form3 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form3()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="temp"></param>
        public Form3(DataGridView temp)
        {
            InitializeComponent();

            try
            {
                //dataGridView1.DataSource = sampleData();
                if (temp.Rows.Count > 0)
                {
                    for (int index = 0; index < temp.Rows.Count; index++)
                    {
                        dataGridView1.Rows.Add(temp.Rows[index].Cells[0].Value, temp.Rows[index].Cells[1].Value, temp.Rows[index].Cells[2].Value, temp.Rows[index].Cells[3].Value, temp.Rows[index].Cells[4].Value, "---", "變更");
                        this.dataGridView1.Rows[index].Cells["btnuse"].Style.ForeColor = Color.Blue;
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Form3 error: " + ee.ToString());
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.ColumnIndex == dataGridView1.Columns["btnuse"].Index && e.RowIndex >= 0)
            {
                MessageBox.Show("btn click，RowIndex：" + e.RowIndex + ", ColumnIndex: " + e.ColumnIndex);
            }
            */


            if (e.RowIndex >= 0)
            {
                MessageBox.Show("btn click，RowIndex：" + e.RowIndex + ", ColumnIndex: " + e.ColumnIndex);
            }
        }


        /// <summary>
        /// 加上底線
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["btnuse"].Index && e.RowIndex >= 0)
            {
                //MessageBox.Show("btn click，RowIndex：" + e.RowIndex + ", ColumnIndex: " + e.ColumnIndex);
                
                if(e.ColumnIndex == dataGridView1.Columns["btnuse"].Index)
                {
                    dataGridView1.Font = new Font(e.CellStyle.Font, FontStyle.Underline);
                }
            }

            /*
            if (e.ColumnIndex == dataGridView1.Columns["btnuse"].Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["btnuse"].Value.ToString().StartsWith("btnuse"))
                {
                    // apply your formatting
                    dataGridView1.Font = new Font(e.CellStyle.Font, FontStyle.Underline);
                }
            }
            */
        }
    }
}
