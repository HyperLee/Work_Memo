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
    public partial class UserControl1 : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdNo"></param>
        public void setMemberIdNo(string IdNo)
        {
            this.label1.Text = IdNo;
        }


        /// <summary>
        /// 
        /// </summary>
        public UserControl1()
        {
            InitializeComponent();

            //label1.Text = "333333333333";
        }
    }
}
