using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace 飲料抽選器.Pages;

public class IndexModel : PageModel
{
    private static readonly List<string> Drinks = new()
    {
        "甘蔗の媽媽-竹北勝利店",
        "煉瓦良茶-正經八百の鮮奶茶",
        "搗壺DAOHU DAY",
        "微平方茶飲竹北三民店",
        "DrinKing 飲品制作所",
        "御私藏鮮奶茶專賣店竹北中正東店",
        "鶖茶",
        "原一茶宴",
        "NOOM 晌暮",
        "那山那茶",
        "林記燒甘蔗",
        "有茶氏",
        "河堤上的貓",
        "飲川",
        "名鄉冰菓店",
        "億客來-竹北店",
        "天香食品量販門市-經國店",
        "悅饌懷舊便當",
        "興隆號",
        "自由聯盟-南寮店"
    };

    [BindProperty]
    public string? Result { get; set; }

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // 初始頁面不顯示結果
    }

    public void OnPost()
    {
        var random = new Random();
        int index = random.Next(Drinks.Count);
        Result = Drinks[index];
    }
}
