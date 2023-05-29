namespace FlowLayoutPanelDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 使用者輸入預設值
            numInput.Value = 10;
            // FlowDirection 預設值為 LedtToRight 
            flowDemo.FlowDirection = FlowDirection.TopDown;
            // WrapContents 預設值為 true
            flowDemo.WrapContents = true;
            // 設定 FlowLayoutPanel 具有 Scroll 功能
            flowDemo.AutoScroll = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            decimal count = numInput.Value;
            if (count <= 0) return;

            // 避免重覆操作，無法顯示正確結果
            flowDemo.Controls.Clear();

            for (int i = 1; i <= count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Name = "cb" + i;
                cb.Text = "cb" + i;
                cb.CheckedChanged += cb_CheckedChanged;

                // 預設是碰到 FlowLayoutPanel Border 時會自動折行
                // 在這改用每 5 個控件就進行折行
                if (i % 5 == 0)
                    flowDemo.SetFlowBreak(cb, true);

                flowDemo.Controls.Add(cb);
            }
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Format("{0} 正被勾選", ((CheckBox)sender).Name);
        }

    }
}