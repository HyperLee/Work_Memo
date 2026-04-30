namespace BackTrack.Problems;

/// <summary>
/// 回溯法題目共通介面：每個題目實作此介面，便於選單統一呼叫。
/// </summary>
public interface IBacktrackingProblem
{
    /// <summary>選單顯示用的中文標題（含英文術語）。</summary>
    string Title { get; }

    /// <summary>一句話描述題目。</summary>
    string Description { get; }

    /// <summary>執行示範（自帶預設輸入），將解答輸出至 Console。</summary>
    void Run();
}
