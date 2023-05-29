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
            // �ϥΪ̿�J�w�]��
            numInput.Value = 10;
            // FlowDirection �w�]�Ȭ� LedtToRight 
            flowDemo.FlowDirection = FlowDirection.TopDown;
            // WrapContents �w�]�Ȭ� true
            flowDemo.WrapContents = true;
            // �]�w FlowLayoutPanel �㦳 Scroll �\��
            flowDemo.AutoScroll = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            decimal count = numInput.Value;
            if (count <= 0) return;

            // �קK���оާ@�A�L�k��ܥ��T���G
            flowDemo.Controls.Clear();

            for (int i = 1; i <= count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Name = "cb" + i;
                cb.Text = "cb" + i;
                cb.CheckedChanged += cb_CheckedChanged;

                // �w�]�O�I�� FlowLayoutPanel Border �ɷ|�۰ʧ��
                // �b�o��ΨC 5 �ӱ���N�i����
                if (i % 5 == 0)
                    flowDemo.SetFlowBreak(cb, true);

                flowDemo.Controls.Add(cb);
            }
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Format("{0} ���Q�Ŀ�", ((CheckBox)sender).Name);
        }

    }
}