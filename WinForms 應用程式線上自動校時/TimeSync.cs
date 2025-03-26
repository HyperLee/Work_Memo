using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Timers;

public class TimeSync
{
    private static TimeSync instance;
    private static readonly object lockObject = new object();
    private bool isRunning;

    private readonly string[] ntpServers = new string[]
    {
        "time.windows.com",
        "time.google.com",
        "pool.ntp.org",
        "time.stdtime.gov.tw"
    };
    
    private System.Timers.Timer timer;
    private readonly int checkIntervalHours;
    /// <summary>
    /// 誤差超過此值時顯示警告
    /// 誤差超過20分鐘出現警告
    /// </summary>
    private const int MaxTimeDifferenceMinutes = 20;

    /// <summary>
    /// 初始化 TimeSync 並設定檢查時間間隔
    /// 預設一小時檢查一次
    /// </summary>
    /// <param name="checkIntervalHours"></param>
    // 讓建構函式變為私有
    private TimeSync(int checkIntervalHours = 1)
    {
        this.checkIntervalHours = checkIntervalHours;
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        timer = new System.Timers.Timer(checkIntervalHours * 60 * 60 * 1000); // 轉換為毫秒
        timer.Elapsed += async (sender, e) => await CheckTimeAsync();
        timer.AutoReset = true;
    }

    // 提供全域存取點
    public static TimeSync Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new TimeSync();
                    }
                }
            }
            return instance;
        }
    }

    public void Start()
    {
        if (isRunning) return; // 如果已經在執行就不要重複啟動
        
        isRunning = true;
        timer.Start();
        Task.Run(CheckTimeAsync);
    }

    public void Stop()
    {
        if (!isRunning) return;
        
        isRunning = false;
        timer.Stop();
    }

    private async Task CheckTimeAsync()
    {
        foreach (string server in ntpServers)
        {
            try
            {
                DateTime networkTime = await GetNetworkTimeAsync(server);
                TimeSpan difference = DateTime.Now - networkTime;

                if (Math.Abs(difference.TotalMinutes) > MaxTimeDifferenceMinutes)
                {
                    ShowTimeDifferenceWarning(difference);
                    break;
                }
                return; // 如果成功取得時間且誤差在範圍內，直接返回
            }
            catch
            {
                continue; // 如果當前伺服器失敗，嘗試下一個
            }
        }
    }

    private async Task<DateTime> GetNetworkTimeAsync(string ntpServer)
    {
        const int ntpPort = 123;
        byte[] ntpData = new byte[48];
        ntpData[0] = 0x1B; // LeapIndicator = 0, Version = 3, Mode = 3 (Client)

        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            socket.ReceiveTimeout = 3000;
            socket.SendTimeout = 3000;
            await socket.ConnectAsync(ntpServer, ntpPort);
            await socket.SendAsync(new ArraySegment<byte>(ntpData), SocketFlags.None);
            await socket.ReceiveAsync(new ArraySegment<byte>(ntpData), SocketFlags.None);
        }

        ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | ntpData[43];
        ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | ntpData[47];
        ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

        DateTime networkDateTime = new DateTime(1900, 1, 1).AddMilliseconds((long)milliseconds);
        return networkDateTime.ToLocalTime(); // 轉換為 UTC+8
    }

    private void ShowTimeDifferenceWarning(TimeSpan difference)
    {
        MessageBox.Show(
            "電腦時間誤差過大，請手動調整或是去電腦時間設定使用立即同步。\n" +
            $"目前誤差：{Math.Abs(difference.TotalMinutes):F1} 分鐘",
            "時間同步警告",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }
}


//--------------------然後在不同的檔案中，您可以這樣使用：
// In Program.cs
TimeSync.Instance.Start();

// In Main1.cs
TimeSync.Instance.Start(); // 不會重複啟動，因為已經在執行中
//----------------------

// 在應用程式啟動時
public class Program
{
    static void Main()
    {
        // 設定檢查間隔為30分鐘
        TimeSync.Instance.Start();
        
        Application.Run(new MainForm());
    }
}

// 在需要停止服務時
TimeSync.Instance.Stop();
