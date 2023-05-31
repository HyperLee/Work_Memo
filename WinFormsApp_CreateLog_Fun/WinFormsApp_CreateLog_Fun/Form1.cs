namespace WinFormsApp_CreateLog_Fun
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 此為 WinFormsApp 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteLog("測試~~~~" + DateTime.Now.ToString());

            MessageBox.Show("執行完畢,請查閱 logPath");
        }

        /// <summary>
        /// Log 資料夾路徑, 可自行修改
        /// </summary>
        private static String logPath = "C:\\Log_Folder\\log"; //Log目錄


        /// <summary>
        /// 新增 log 紀錄
        /// 每次開啟時候紀錄 驗證 開通 步驟
        /// Log_Folder
        /// </summary>
        /// <param name="logMsg"></param>
        public static void WriteLog(string logMsg)
        {
            try
            {
                //檔案名稱 使用現在日期
                string logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".txt";

                //Log檔內的時間 使用現在時間
                //String nowTime = int.Parse(DateTime.Now.Hour.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Minute.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Second.ToString()).ToString("00");
                string nowTime = DateTime.Now.ToString();

                if (!Directory.Exists(logPath))
                {
                    //建立資料夾
                    Directory.CreateDirectory(logPath);
                }

                if (!File.Exists(logPath + "\\" + logFileName))
                {
                    //建立檔案
                    File.Create(logPath + "\\" + logFileName).Close();
                }

                using (StreamWriter sw = File.AppendText(logPath + "\\" + logFileName))
                {
                    //WriteLine為換行 
                    sw.Write(nowTime + "---->");
                    sw.WriteLine(logMsg);
                    sw.WriteLine("");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("錯誤訊息: " + ee.ToString());
            }

        }

    }
}