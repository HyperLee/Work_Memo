using System.Collections.Generic;
using System.Threading.Tasks;
using StockGiftSearch.Models;

namespace StockGiftSearch.Services;

/// <summary>
/// 提供網路搜尋功能，將搜尋結果轉為 StockGiftInfo。
/// </summary>
public class WebSearchService
{
    /// <summary>
    /// 以指定關鍵字搜尋網路，並回傳預設資料。
    /// </summary>
    /// <param name="query">搜尋關鍵字</param>
    /// <returns>StockGiftInfo 預設資料清單</returns>
    public async Task<List<StockGiftInfo>> FetchDefaultStockGiftListAsync(string query)
    {
        // TODO: 實作 #vscode-websearchforcopilot_webSearch 呼叫，並將結果轉為 StockGiftInfo
        // 這裡僅回傳假資料作為範例
        await Task.Delay(100); // 模擬非同步呼叫
        return new List<StockGiftInfo>
        {
            new StockGiftInfo { Code = "0001", Name = "網路搜尋預設1", GiftName = "預設禮品A" },
            new StockGiftInfo { Code = "0002", Name = "網路搜尋預設2", GiftName = "預設禮品B" }
        };
    }
}
