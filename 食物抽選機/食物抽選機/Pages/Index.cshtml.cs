using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace 食物抽選機.Pages;

public class FoodItem
{
    public string Name { get; set; } = string.Empty; // 店家名稱
    public string Address { get; set; } = string.Empty; // 地址
    public string Food { get; set; } = string.Empty; // 食物名稱
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    // 竹北市常見食物清單（含店家名稱、地址、食物名稱）
    public List<FoodItem> FoodList = new List<FoodItem>
    {
        new FoodItem { Name = "煉瓦良茶", Address = "新竹縣竹北市勝利一路156號", Food = "手搖飲" },
        new FoodItem { Name = "嘉鄉豆腐店", Address = "新竹縣竹北市光明三路62號", Food = "豆腐料理" },
        new FoodItem { Name = "健康盒餐", Address = "新竹縣竹北市縣政二路177號", Food = "健康便當" },
        new FoodItem { Name = "那山那茶", Address = "新竹縣竹北市光明三路106-1號", Food = "茶飲" },
        new FoodItem { Name = "甲文青-竹北博愛店", Address = "新竹縣竹北市北崙里博愛街283號", Food = "文青便當" },
        new FoodItem { Name = "黑豆拌麵", Address = "新竹縣竹北市農會", Food = "拌麵" },
        new FoodItem { Name = "黑豆植物奶", Address = "新竹縣竹北市農會", Food = "植物奶" },
        new FoodItem { Name = "沖泡炊粉/冬粉", Address = "新竹縣竹北市農會", Food = "冬粉" },
        new FoodItem { Name = "地瓜", Address = "新竹縣竹北市農會", Food = "地瓜" },
        new FoodItem { Name = "十全特好食品", Address = "新竹縣竹北市", Food = "味噌/醬料" },
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
