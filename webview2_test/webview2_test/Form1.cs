using System.Drawing.Printing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace webview2_test
{
    public partial class Form1 : Form
    {
        private static string _ExePath;
        private static string _ExePath2;

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 預覽列印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string sDefaultPrinter = null;
            try
            {
                // 預設印表機
                PrintDocument printDoc = new PrintDocument();
                sDefaultPrinter = printDoc.PrinterSettings.PrinterName;
                // 印表機設定
                if (!string.IsNullOrEmpty("(系統預設印表機)"))
                {
                    myPrinters.SetDefaultPrinter("(系統預設印表機)");
                }

                //20230320
                webView21.CoreWebView2.ShowPrintUI();

                MessageBox.Show("列印預覽完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine("列印預覽錯誤 ::: " + ex.Message);
            }
            finally
            {
                if (!string.IsNullOrEmpty(sDefaultPrinter))
                {
                    myPrinters.SetDefaultPrinter(sDefaultPrinter);
                }
            }
        }


        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);
        }


        /// <summary>
        /// 直接列印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void button2_Click(object sender, EventArgs e)
        {
            string sDefaultPrinter = null;
            try
            {
                // 預設印表機
                PrintDocument printDoc = new PrintDocument();
                sDefaultPrinter = printDoc.PrinterSettings.PrinterName;
                // 印表機設定
                if (!string.IsNullOrEmpty("(系統預設印表機)"))
                {
                    myPrinters.SetDefaultPrinter("(系統預設印表機)");
                }

                // 20230320
                Microsoft.Web.WebView2.Core.CoreWebView2PrintSettings printSettings = this.webView21.CoreWebView2.Environment.CreatePrintSettings();
                printSettings.PrinterName = sDefaultPrinter;
                printSettings.MarginTop = 0.166540;
                printSettings.MarginRight = 0.166540;
                printSettings.MarginBottom = 0.395670;
                printSettings.MarginLeft = 0.166540;
                await webView21.CoreWebView2.PrintAsync(printSettings);

                MessageBox.Show("列印完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine("列印錯誤 ::: " + ex.Message);
            }
            finally
            {
                if (!string.IsNullOrEmpty(sDefaultPrinter))
                {
                    myPrinters.SetDefaultPrinter(sDefaultPrinter);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _ExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //ShowBarcode();
            }
            catch (Exception ee)
            {
                MessageBox.Show("列印錯誤: " + ee.ToString());
                //Program.WriteLog("frmSell_SellNo, 列印錯誤: " + ee.ToString());
            }
        }

        /// <summary>
        /// 自行輸入的html資料
        /// 如果不跑這段, 就會執行 webview21.url 資料
        /// </summary>
        private async void ShowBarcode()
        {
            string mainHtml = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"/><style>body{width:100%;height:100%;margin:0;padding:0;background-color:#FAFAFA;font-family:\"微軟正黑體\", Microsoft JhengHei;} *{box-sizing: border-box;-moz-box-sizing: border-box;} .page{width:170mm;min-height:244mm;padding:10mm;margin:10mm auto;border:1px #D3D3D3 solid;border-radius:5px;background:white;box-shadow:0 0 5px rgba(0,0,0,0.1);} .subpage{padding: 1cm;border: 5px red solid;height: 257mm;outline: 2cm #FFEAEA solid;} table{border-collapse:collapse;border-spacing:0;border:1px dotted #666;background-color:#FFF;font-size:1.15em;margin:0 0 20px 0;width:100%;} th{text-align:right;border:1px dotted #666;padding:5px;white-space:nowrap;} td{text-align:left;border:1px dotted #666;padding:5px;vertical-align:top;} @page{size:A4;margin:0;} @media print{html,body{width:210mm;height:297mm;} .page{border:initial;border-radius:initial;width:initial;min-height:initial;box-shadow:initial;background:initial;}}</style></head><body style=\"margin:0;padding:0;overflow:auto;\"><div class=\"book\"><div class=\"page\">Page 1/1<div align=\"center\" style=\"font-size:24px;\"> 購買證明 </div><table summary=\"資料表格\" style=\"font-size:1em;\"><tr><td rowspan=\"2\" style=\"width:30%;text-align:center;vertical-align:middle;\"><div style=\"overflow:hidden;height:65px;\"><img src=\"D:\\Project2\\POS_2023_WebView2\\POS_710\\POS_Client\\bin\\Debug\\TempBarCode\\V9002623202307070001.gif\" style=\"max-width:215px;\"/></div></td><th style=\"width:10%;\">店家</th><td colspan=\"3\">測試店家</td></tr><tr><th>銷售日期</th><td>2023-07-07 15:54:08</td><th style=\"width:15%;\">列印日期</th><td>2023-07-07 15:57:57</td></tr></table><table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:15%;\">銷售單號</th><td style=\"width:40%;\">FFFFFF23202307070001(v1)</td><th style=\"width:15%;\">會員姓名</th><td>ABCDEFGHIJK</td></tr><tr><th style=\"width:15%;\">身分證或居留證號</th><td style=\"width:18%;\">GGGG***789</td><th style=\"width:15%;\">年齡</th><td>18</td></tr><tr><th style=\"width:15%;\">地址</th><td style=\"vertical-align:middle;\">臺北市中正區**********</td><th style=\"width:15%;\">連絡電話</th><td></td></tr></table><table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:5%;\">&nbsp;</th><th style=\"text-align:center;\">藥物產品條碼及名稱</th><th style=\"text-align:center;width:7%;font-size:.85em;\">售價</th><th style=\"text-align:center;width:7%;font-size:.85em;\">數量</th><th style=\"text-align:center;width:7%;font-size:.85em;\">折讓</th><th style=\"text-align:center;width:7%;font-size:.85em;\">合計</th><th style=\"text-align:center; width:15%; font-size:.85em;\">使用範圍</th><th style=\"text-align:center; width:15%; font-size:.85em;\">使用說明</th></tr><tr><td class=\"aCenter\">1</td><td class=\"title\"><div class=\"text-overflow of2\"><div class=\"code\">4710474670000</div><div class=\"productname\">[可滅鼠-RB．0.005  (%)-宇慶化工股份有限公司]<br/><span class=\"unit\">PP塑膠袋200公克</span></div></div></td><td class=\"number\">200</td><td class=\"number\">1</td><td class=\"number\">0</td><td class=\"number total\">200</td><td class=\"number total\">作物園x野鼠</td><td class=\"usenote\">稀釋倍數: </td></tr></table><table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:15%;\">總計</th><td style=\"width:18%;\">200</td><th style=\"width:15%;\">總價折讓</th><td style=\"width:18%;\">0</td><th style=\"width:15%;\" rowspan=\"2\">消費總額</th><td rowspan=\"2\" style=\"vertical-align:middle;\">200</td></tr><tr><th>品項</th><td>1</td><th>數量</th><td>1</td></tr><tr><th>收款</th><td colspan=\"3\">現金:200<span style=\"font-size:.85em;vertical-align:middle;\">(賒帳:0)</span></td><th>找零</th><td>0</td></td></tr></table><table summary=\"資料表格\" style=\"font-size:1em;\"><tr><td style=\"width:30%;text-align:center;vertical-align:middle;\"><div>跨店購買條碼：</div><div style=\"overflow:hidden;height:65px;\"><img src=\"D:\\Project2\\POS_2023_WebView2\\POS_710\\POS_Client\\bin\\Debug\\TempBarCode2\\4856789ABCD0.gif\" style=\"max-width:215px;\"/></div><div>購買出示本條碼即免身分驗證，並同意販賣業者由系統帶入個人資料。</div></td></table></div></div></body></html>";

            /*
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping("TempBarCode", _ExePath + "\\TempBarCode\\", Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
            webView21.NavigateToString(mainHtml.Replace(_ExePath + "\\TempBarCode\\", "http://TempBarCode/"));
            */

            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping("TempBarCode", _ExePath + "\\TempBarCode\\", Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping("TempBarCode2", _ExePath + "\\TempBarCode2\\", Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
            webView21.NavigateToString(mainHtml.Replace(_ExePath + "\\TempBarCode\\", "http://TempBarCode/").Replace(_ExePath + "\\TempBarCode2\\", "http://TempBarCode2/"));

        }

    }
}