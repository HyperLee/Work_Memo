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
            /*
            if (e.ColumnIndex == dataGridView1.Columns["btnuse"].Index && e.RowIndex >= 0)
            {
                //MessageBox.Show("btn click，RowIndex：" + e.RowIndex + ", ColumnIndex: " + e.ColumnIndex);
                
                if(e.ColumnIndex == dataGridView1.Columns["btnuse"].Index)
                {
                    dataGridView1.Font = new Font(e.CellStyle.Font, FontStyle.Underline);
                }
            }
            */

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


        /// <summary>
        /// 文字加上底線
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 确保是第六列（假设你要添加底线的列）
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // 獲取文字的範圍
                Rectangle textRect = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                textRect.Height = e.CellBounds.Height - 1; // 减去一些以避免重叠

                // 獲取文字
                string cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // 獲取文字的大小
                Size textSize = TextRenderer.MeasureText(e.Graphics, cellValue, e.CellStyle.Font);

                // 計算底線的位置
                int underlineY = textRect.Y + textSize.Height + 6; // + 6 = > 手動調整底線高度(位置), 需要視情況而定

                // 畫線
                e.Graphics.DrawLine(Pens.Black, textRect.X, underlineY, textRect.X + textSize.Width, underlineY);

                e.Handled = true;
            }
        }
    }
}
