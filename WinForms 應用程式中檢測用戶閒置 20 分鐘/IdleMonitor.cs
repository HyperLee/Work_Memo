using System;
using System.Windows.Forms;

public class IdleMonitor
{
    private readonly Timer idleTimer;
    private readonly Timer displayTimer;
    private readonly Form mainForm;
    private readonly Label timeLabel;
    private int remainingSeconds;
    private const int IDLE_TIME = 20 * 60; // 20分鐘換算成秒數

    public IdleMonitor(Form form, Label label)
    {
        mainForm = form;
        timeLabel = label;
        remainingSeconds = IDLE_TIME;

        idleTimer = new Timer();
        idleTimer.Interval = 1000; // 每秒檢查一次
        idleTimer.Tick += IdleTimer_Tick;

        displayTimer = new Timer();
        displayTimer.Interval = 1000;
        displayTimer.Tick += DisplayTimer_Tick;

        StartMonitoring();
    }

    private void IdleTimer_Tick(object sender, EventArgs e)
    {
        remainingSeconds--;
        if (remainingSeconds <= 0)
        {
            ShowIdleMessage();
        }
    }

    private void DisplayTimer_Tick(object sender, EventArgs e)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int minutes = remainingSeconds / 60;
        int seconds = remainingSeconds % 60;
        timeLabel.Text = $"閒置倒數: {minutes:00}:{seconds:00}";
    }

    private void ShowIdleMessage()
    {
        idleTimer.Stop();
        displayTimer.Stop();
        MessageBox.Show("您已閒置超過20分鐘", "閒置提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        ResetTimers();
    }

    public void ResetTimers()
    {
        remainingSeconds = IDLE_TIME;
        UpdateDisplay();
        idleTimer.Start();
        displayTimer.Start();
    }

    private void StartMonitoring()
    {
        idleTimer.Start();
        displayTimer.Start();
    }
}
