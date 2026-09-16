namespace 多工處理_使用_async_和_await練習;

internal class Program
{
    /// <summary>
    /// 在 C# 中，多工處理（multithreading）是指同時執行多個執行緒（thread），每個執行緒都可以獨立執行不同的工作。
    /// 這種方式讓應用程式能夠更有效地利用系統資源，並在處理大量任務時保持響應性。
    /// C# 提供多種方式來進行多工處理，最常見的包括 Thread 類、ThreadPool、Task、async/await 和 Parallel。
    ///
    /// async 和 await 關鍵字使編寫異步代碼更容易。這些關鍵字不直接處理執行緒，而是使應用程式能夠在等待長時間操作時繼續執行其他工作。
    /// </summary>
    private static async Task Main()
    {
        int passed = 0;
        int total = 0;

        passed += await RunCaseAsync(
            "兩個工作者彙總",
            "Task 1=6; Task 2=15; Total=21",
            new WorkerInput(1, new int[] { 1, 2, 3 }),
            new WorkerInput(2, new int[] { 4, 5, 6 }));
        total++;
        passed += await RunCaseAsync(
            "空工作負載",
            "Task 3=0; Total=0",
            new WorkerInput(3, Array.Empty<int>()));
        total++;
        passed += await RunCaseAsync(
            "包含負數",
            "Task 4=4; Task 5=-3; Total=1",
            new WorkerInput(4, new int[] { -2, 6 }),
            new WorkerInput(5, new int[] { -1, -2 }));
        total++;

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 同時啟動所有工作者，等待全部完成後依輸入順序產生穩定結果。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="expected">預期彙總文字。</param>
    /// <param name="inputs">每位工作者的新輸入。</param>
    /// <returns>通過時回傳 1，否則回傳 0。</returns>
    private static async Task<int> RunCaseAsync(string name, string expected, params WorkerInput[] inputs)
    {
        Task<WorkerResult>[] tasks = inputs
            .Select(input => LongRunningTask(input.TaskId, input.Values))
            .ToArray();

        WorkerResult[] results = await Task.WhenAll(tasks);
        int grandTotal = results.Sum(result => result.Total);
        string workerText = string.Join("; ", results.Select(result => $"Task {result.TaskId}={result.Total}"));
        string actual = $"{workerText}; Total={grandTotal}";
        bool isPassed = expected == actual;

        Console.WriteLine($"Case: {name}");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Actual: {actual}");
        Console.WriteLine($"PASS-FAIL: {(isPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();
        return isPassed ? 1 : 0;
    }

    /// <summary>
    /// 非同步處理一位工作者的所有數值並回傳總和，不直接輸出排程順序。
    /// </summary>
    /// <param name="taskId">工作者識別碼。</param>
    /// <param name="values">要加總的數值。</param>
    /// <returns>包含工作者識別碼與總和的結果。</returns>
    private static async Task<WorkerResult> LongRunningTask(int taskId, IReadOnlyList<int> values)
    {
        await Task.Yield();

        int total = 0;
        foreach (int value in values)
        {
            total += value;
        }

        return new WorkerResult(taskId, total);
    }

    /// <summary>描述一位工作者的固定輸入。</summary>
    /// <param name="TaskId">工作者識別碼。</param>
    /// <param name="Values">要處理的數值。</param>
    private sealed record WorkerInput(int TaskId, IReadOnlyList<int> Values);

    /// <summary>描述一位工作者完成後的穩定結果。</summary>
    /// <param name="TaskId">工作者識別碼。</param>
    /// <param name="Total">工作者計算的總和。</param>
    private sealed record WorkerResult(int TaskId, int Total);
}