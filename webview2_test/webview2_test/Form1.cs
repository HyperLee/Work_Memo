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
        /// �w���C�L
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string sDefaultPrinter = null;
            try
            {
                // �w�]�L���
                PrintDocument printDoc = new PrintDocument();
                sDefaultPrinter = printDoc.PrinterSettings.PrinterName;
                // �L����]�w
                if (!string.IsNullOrEmpty("(�t�ιw�]�L���)"))
                {
                    myPrinters.SetDefaultPrinter("(�t�ιw�]�L���)");
                }

                //20230320
                webView21.CoreWebView2.ShowPrintUI();

                MessageBox.Show("�C�L�w������");
            }
            catch (Exception ex)
            {
                Console.WriteLine("�C�L�w�����~ ::: " + ex.Message);
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
        /// �����C�L
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void button2_Click(object sender, EventArgs e)
        {
            string sDefaultPrinter = null;
            try
            {
                // �w�]�L���
                PrintDocument printDoc = new PrintDocument();
                sDefaultPrinter = printDoc.PrinterSettings.PrinterName;
                // �L����]�w
                if (!string.IsNullOrEmpty("(�t�ιw�]�L���)"))
                {
                    myPrinters.SetDefaultPrinter("(�t�ιw�]�L���)");
                }

                // 20230320
                Microsoft.Web.WebView2.Core.CoreWebView2PrintSettings printSettings = this.webView21.CoreWebView2.Environment.CreatePrintSettings();
                printSettings.PrinterName = sDefaultPrinter;
                printSettings.MarginTop = 0.166540;
                printSettings.MarginRight = 0.166540;
                printSettings.MarginBottom = 0.395670;
                printSettings.MarginLeft = 0.166540;
                await webView21.CoreWebView2.PrintAsync(printSettings);

                MessageBox.Show("�C�L����");
            }
            catch (Exception ex)
            {
                Console.WriteLine("�C�L���~ ::: " + ex.Message);
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
                MessageBox.Show("�C�L���~: " + ee.ToString());
                //Program.WriteLog("frmSell_SellNo, �C�L���~: " + ee.ToString());
            }
        }

        /// <summary>
        /// �ۦ��J��html���
        /// �p�G���]�o�q, �N�|���� webview21.url ���
        /// </summary>
        private async void ShowBarcode()
        {
            string mainHtml = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"/><style>body{width:100%;height:100%;margin:0;padding:0;background-color:#FAFAFA;font-family:\"�L�n������\", Microsoft JhengHei;} *{box-sizing: border-box;-moz-box-sizing: border-box;} .page{width:170mm;min-height:244mm;padding:10mm;margin:10mm auto;border:1px #D3D3D3 solid;border-radius:5px;background:white;box-shadow:0 0 5px rgba(0,0,0,0.1);} .subpage{padding: 1cm;border: 5px red solid;height: 257mm;outline: 2cm #FFEAEA solid;} table{border-collapse:collapse;border-spacing:0;border:1px dotted #666;background-color:#FFF;font-size:1.15em;margin:0 0 20px 0;width:100%;} th{text-align:right;border:1px dotted #666;padding:5px;white-space:nowrap;} td{text-align:left;border:1px dotted #666;padding:5px;vertical-align:top;} @page{size:A4;margin:0;} @media print{html,body{width:210mm;height:297mm;} .page{border:initial;border-radius:initial;width:initial;min-height:initial;box-shadow:initial;background:initial;}}</style></head><body style=\"margin:0;padding:0;overflow:auto;\"><div class=\"book\"><div class=\"page\">Page 1/1<div align=\"center\" style=\"font-size:24px;\"> �ʶR�ҩ� </div><table summary=\"��ƪ��\" style=\"font-size:1em;\"><tr><td rowspan=\"2\" style=\"width:30%;text-align:center;vertical-align:middle;\"><div style=\"overflow:hidden;height:65px;\"><img src=\"D:\\Project2\\POS_2023_WebView2\\POS_710\\POS_Client\\bin\\Debug\\TempBarCode\\V9002623202307070001.gif\" style=\"max-width:215px;\"/></div></td><th style=\"width:10%;\">���a</th><td colspan=\"3\">���թ��a</td></tr><tr><th>�P����</th><td>2023-07-07 15:54:08</td><th style=\"width:15%;\">�C�L���</th><td>2023-07-07 15:57:57</td></tr></table><table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:15%;\">�P��渹</th><td style=\"width:40%;\">FFFFFF23202307070001(v1)</td><th style=\"width:15%;\">�|���m�W</th><td>ABCDEFGHIJK</td></tr><tr><th style=\"width:15%;\">�����ҩΩ~�d�Ҹ�</th><td style=\"width:18%;\">GGGG***789</td><th style=\"width:15%;\">�~��</th><td>18</td></tr><tr><th style=\"width:15%;\">�a�}</th><td style=\"vertical-align:middle;\">�O�_��������**********</td><th style=\"width:15%;\">�s���q��</th><td></td></tr></table><table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:5%;\">&nbsp;</th><th style=\"text-align:center;\">�Ī����~���X�ΦW��</th><th style=\"text-align:center;width:7%;font-size:.85em;\">���</th><th style=\"text-align:center;width:7%;font-size:.85em;\">�ƶq</th><th style=\"text-align:center;width:7%;font-size:.85em;\">����</th><th style=\"text-align:center;width:7%;font-size:.85em;\">�X�p</th><th style=\"text-align:center; width:15%; font-size:.85em;\">�ϥνd��</th><th style=\"text-align:center; width:15%; font-size:.85em;\">�ϥλ���</th></tr><tr><td class=\"aCenter\">1</td><td class=\"title\"><div class=\"text-overflow of2\"><div class=\"code\">4710474670000</div><div class=\"productname\">[�i����-RB�D0.005  (%)-�t�y�Ƥu�ѥ��������q]<br/><span class=\"unit\">PP�콦�U200���J</span></div></div></td><td class=\"number\">200</td><td class=\"number\">1</td><td class=\"number\">0</td><td class=\"number total\">200</td><td class=\"number total\">�@����x����</td><td class=\"usenote\">�}������: </td></tr></table><table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:15%;\">�`�p</th><td style=\"width:18%;\">200</td><th style=\"width:15%;\">�`������</th><td style=\"width:18%;\">0</td><th style=\"width:15%;\" rowspan=\"2\">���O�`�B</th><td rowspan=\"2\" style=\"vertical-align:middle;\">200</td></tr><tr><th>�~��</th><td>1</td><th>�ƶq</th><td>1</td></tr><tr><th>����</th><td colspan=\"3\">�{��:200<span style=\"font-size:.85em;vertical-align:middle;\">(���b:0)</span></td><th>��s</th><td>0</td></td></tr></table><table summary=\"��ƪ��\" style=\"font-size:1em;\"><tr><td style=\"width:30%;text-align:center;vertical-align:middle;\"><div>���ʶR���X�G</div><div style=\"overflow:hidden;height:65px;\"><img src=\"D:\\Project2\\POS_2023_WebView2\\POS_710\\POS_Client\\bin\\Debug\\TempBarCode2\\4856789ABCD0.gif\" style=\"max-width:215px;\"/></div><div>�ʶR�X�ܥ����X�Y�K�������ҡA�æP�N�c��~�̥Ѩt�αa�J�ӤH��ơC</div></td></table></div></div></body></html>";

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