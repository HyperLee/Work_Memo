namespace WinFormsApp_CreateLog_Fun
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// ���� WinFormsApp 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteLog("����~~~~" + DateTime.Now.ToString());

            MessageBox.Show("���槹��,�Ьd�\ logPath");
        }

        /// <summary>
        /// Log ��Ƨ����|, �i�ۦ�ק�
        /// </summary>
        private static String logPath = "C:\\Log_Folder\\log"; //Log�ؿ�


        /// <summary>
        /// �s�W log ����
        /// �C���}�ҮɭԬ��� ���� �}�q �B�J
        /// Log_Folder
        /// </summary>
        /// <param name="logMsg"></param>
        public static void WriteLog(string logMsg)
        {
            try
            {
                //�ɮצW�� �ϥβ{�b���
                string logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".txt";

                //Log�ɤ����ɶ� �ϥβ{�b�ɶ�
                //String nowTime = int.Parse(DateTime.Now.Hour.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Minute.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Second.ToString()).ToString("00");
                string nowTime = DateTime.Now.ToString();

                if (!Directory.Exists(logPath))
                {
                    //�إ߸�Ƨ�
                    Directory.CreateDirectory(logPath);
                }

                if (!File.Exists(logPath + "\\" + logFileName))
                {
                    //�إ��ɮ�
                    File.Create(logPath + "\\" + logFileName).Close();
                }

                using (StreamWriter sw = File.AppendText(logPath + "\\" + logFileName))
                {
                    //WriteLine������ 
                    sw.Write(nowTime + "---->");
                    sw.WriteLine(logMsg);
                    sw.WriteLine("");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("���~�T��: " + ee.ToString());
            }

        }

    }
}