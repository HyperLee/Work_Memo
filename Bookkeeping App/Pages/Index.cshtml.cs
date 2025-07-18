using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookkeeping_App.Pages;

/// <summary>
/// Index 頁面 PageModel。資料暫存於前端，後端僅維持最小結構。
/// </summary>
public class IndexModel : PageModel
{
    /// <summary>
    /// GET 處理，無需資料處理。
    /// </summary>
    public void OnGet()
    {
        // 無需後端資料，所有記帳資料於前端暫存
    }
}
