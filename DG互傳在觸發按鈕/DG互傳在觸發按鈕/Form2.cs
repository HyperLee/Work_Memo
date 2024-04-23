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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        /// <summary>
        /// receive data from form1
        /// </summary>
        public Form2(DataGridView temp)
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
                    }

                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("Form2 error: " + ee.ToString());
            }


        }


        /// <summary>
        /// btn click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["btnuse"].Index && e.RowIndex >= 0)
            {
                MessageBox.Show("btn click，RowIndex：" + e.RowIndex + ", ColumnIndex: " + e.ColumnIndex);
            }
        }
    }
}
