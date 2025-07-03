using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace 食物抽選機.Pages;

public class FoodItem
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    // 竹北市常見食物清單（含店家名稱與地址）
    public List<FoodItem> FoodList = new List<FoodItem>
    {
        new FoodItem { Name = "煉瓦良茶", Address = "新竹縣竹北市勝利一路156號" },
        new FoodItem { Name = "嘉鄉豆腐店", Address = "新竹縣竹北市光明三路62號" },
        new FoodItem { Name = "健康盒餐", Address = "新竹縣竹北市縣政二路177號" },
        new FoodItem { Name = "那山那茶", Address = "新竹縣竹北市光明三路106-1號" },
        new FoodItem { Name = "甲文青-竹北博愛店", Address = "新竹縣竹北市北崙里博愛街283號" },
        new FoodItem { Name = "黑豆拌麵", Address = "新竹縣竹北市農會" },
        new FoodItem { Name = "黑豆植物奶", Address = "新竹縣竹北市農會" },
        new FoodItem { Name = "沖泡炊粉/冬粉", Address = "新竹縣竹北市農會" },
        new FoodItem { Name = "地瓜", Address = "新竹縣竹北市農會" },
        new FoodItem { Name = "十全特好食品", Address = "新竹縣竹北市" },
        // ...可依需求再擴充...
    };

    [BindProperty]
    public FoodItem? Result { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public void OnPost()
    {
        var random = new Random();
        int idx = random.Next(FoodList.Count);
        Result = FoodList[idx];
    }
}
