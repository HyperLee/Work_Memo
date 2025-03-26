using System;
using System.Windows.Forms;
using System.Drawing;

public partial class Form1 : Form
{
    private IdleMonitor idleMonitor;
    private Label lblIdleTime;

    public Form1()
    {
        InitializeComponent();
        InitializeIdleMonitoring();
    }

    private void InitializeIdleMonitoring()
    {
        // 建立顯示倒數的Label
        lblIdleTime = new Label
        {
            AutoSize = true,
            Location = new Point(10, 10),
            Font = new Font("Arial", 12)
        };
        this.Controls.Add(lblIdleTime);

        // 初始化閒置監控
        idleMonitor = new IdleMonitor(this, lblIdleTime);

        // 註冊事件以重設計時器
        this.MouseMove += (s, e) => idleMonitor.ResetTimers();
        this.KeyDown += (s, e) => idleMonitor.ResetTimers();
    }
}
