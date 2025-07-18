// 這是一個用於除錯的暫時測試程式，會將原始 HTML 存檔，方便分析表格結構
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockGiftSearch.Tools;

public class HtmlDebugTool
{
    public static async Task SaveGiftPageHtmlAsync()
    {
        using var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync("https://histock.tw/stock/gift.aspx");
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gift_debug.html");
        await File.WriteAllTextAsync(filePath, html);
        Console.WriteLine($"已儲存 HTML 至 {filePath}");
    }
}
