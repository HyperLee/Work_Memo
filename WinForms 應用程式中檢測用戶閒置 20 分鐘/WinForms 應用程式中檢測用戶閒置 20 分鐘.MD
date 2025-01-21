要在 C# WinForms 應用程式中檢測用戶閒置 20 分鐘，可以使用計時器和 Windows API `GetLastInputInfo`。以下是一個範例程式碼：

1. **引用必要的命名空間**：
   ```csharp
   using System;
   using System.Runtime.InteropServices;
   using System.Windows.Forms;
   ```

2. **定義 `GetLastInputInfo` 結構和方法**：
   ```csharp
   public struct LASTINPUTINFO
   {
       public uint cbSize;
       public uint dwTime;
   }

   [DllImport("user32.dll")]
   public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
   ```

3. **設置計時器來檢查閒置時間**：
   ```csharp
   public partial class MainForm : Form
   {
       private Timer idleTimer;
       private const int IdleThreshold = 20 * 60 * 1000; // 20 分鐘

       public MainForm()
       {
           InitializeComponent();
           idleTimer = new Timer();
           idleTimer.Interval = 1000; // 每秒檢查一次
           idleTimer.Tick += IdleTimer_Tick;
           idleTimer.Start();
       }

       private void IdleTimer_Tick(object sender, EventArgs e)
       {
           if (GetIdleTime() >= IdleThreshold)
           {
               // 用戶閒置超過 20 分鐘
               MessageBox.Show("用戶已閒置 20 分鐘");
               // 在這裡添加其他處理邏輯
           }
       }

       private uint GetIdleTime()
       {
           LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
           lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
           if (GetLastInputInfo(ref lastInputInfo))
           {
               return ((uint)Environment.TickCount - lastInputInfo.dwTime);
           }
           return 0;
       }
   }
   ```

這段程式碼會每秒檢查一次用戶的閒置時間，如果超過 20 分鐘，則會顯示一個訊息框[1](https://www.codeproject.com/articles/13756/detecting-application-idleness)[2](https://github.com/fresky/IdleMonitor)。

如果您有其他問題或需要進一步的幫助，請隨時告訴我！