using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockGiftSearch.Models;
using StockGiftSearch.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockGiftSearch.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly StockGiftService _stockGiftService;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _stockGiftService = new StockGiftService();
    }

    /// <summary>
    /// 查詢結果資料集。
    /// </summary>
    public List<StockGiftInfo> StockGifts { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Query { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Sort { get; set; }

    public async Task OnGetAsync()
    {
        var all = await _stockGiftService.FetchStockGiftListAsync();
        IEnumerable<StockGiftInfo> filtered = all;
        if (!string.IsNullOrWhiteSpace(Query))
        {
            filtered = filtered.Where(x => x.Code.Contains(Query) || x.Name.Contains(Query) || x.GiftName.Contains(Query));
        }
        if (Sort == "desc")
        {
            filtered = filtered.OrderByDescending(x => x.Code);
        }
        else
        {
            filtered = filtered.OrderBy(x => x.Code);
        }
        StockGifts = filtered.ToList();
    }
}
