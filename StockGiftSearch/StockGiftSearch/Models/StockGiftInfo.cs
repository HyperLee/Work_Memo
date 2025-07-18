using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace StockGiftSearch.Models;

/// <summary>
/// 股東會紀念品資料模型。
/// </summary>
public class StockGiftInfo
{
    /// <summary>
    /// 公司代號。
    /// </summary>
    public string Code { get; set; } = string.Empty;
    /// <summary>
    /// 公司名稱。
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 股東會日期。
    /// </summary>
    public string MeetingDate { get; set; } = string.Empty;
    /// <summary>
    /// 紀念品名稱。
    /// </summary>
    public string GiftName { get; set; } = string.Empty;
}
