using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StockGiftSearch.Models;

namespace StockGiftSearch.Services;

/// <summary>
/// 股東會紀念品資料擷取服務。
/// </summary>
public class StockGiftService
{
    private const string SourceUrl = "https://histock.tw/stock/gift.aspx";

    private static List<StockGiftInfo>? _cache;
    private static DateTime _cacheTime;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

    /// <summary>
    /// 從來源網頁擷取股東會紀念品資料。
    /// </summary>
    /// <returns>股東會紀念品資料清單</returns>
    public async Task<List<StockGiftInfo>> FetchStockGiftListAsync()
    {
        if (_cache != null && (DateTime.Now - _cacheTime) < CacheDuration)
        {
            return _cache;
        }
        var result = new List<StockGiftInfo>();
        using var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(SourceUrl);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // TODO: 解析網頁表格，填入 result
        // 需根據實際網頁結構調整 XPath
        var tableNodes = doc.DocumentNode.SelectNodes("//table[contains(@class,'tb-stock')]");
        if (tableNodes is not null)
        {
            int tableIndex = 0;
            foreach (var table in tableNodes)
            {
                var rows = table.SelectNodes(".//tr");
                if (rows is null) continue;
                int rowIndex = 0;
                foreach (var row in rows)
                {
                    var cells = row.SelectNodes(".//td");
                    if (cells is null || cells.Count < 4)
                    {
                        rowIndex++;
                        continue;
                    }
                    var info = new StockGiftInfo
                    {
                        Code = cells[0].InnerText.Trim(),
                        Name = cells[1].InnerText.Trim(),
                        MeetingDate = cells[2].InnerText.Trim(),
                        GiftName = cells[3].InnerText.Trim()
                    };
                    // 除錯用: 輸出到 Console
                    Console.WriteLine($"[Table {tableIndex}][Row {rowIndex}] {info.Code} {info.Name} {info.MeetingDate} {info.GiftName}");
                    result.Add(info);
                    rowIndex++;
                }
                tableIndex++;
            }
        }
        _cache = result;
        _cacheTime = DateTime.Now;
        return result;
    }
}
