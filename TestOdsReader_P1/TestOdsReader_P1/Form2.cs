using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip; // 加入參考，選擇 Ionic.Zip.dll
using System.IO;
using System.Xml;
using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestOdsReader_P1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        // ********************************************************************************
        // 下面程式碼取自
        // How to Read and Write ODFODS Files (OpenDocument Spreadsheets)
        // https://www.codeproject.com/articles/38425/how-to-read-and-write-odf-ods-files-opendocument-s%20here%20for%20reference
        // 解壓後取自 OdsReaderWriter.cs 檔案內容
        // 一些用不上的已刪除
        // ********************************************************************************
        private static string[,] namespaces = new string[,]
        {
            {"table", "urn:oasis:names:tc:opendocument:xmlns:table:1.0"},
            {"office", "urn:oasis:names:tc:opendocument:xmlns:office:1.0"},
            {"style", "urn:oasis:names:tc:opendocument:xmlns:style:1.0"},
            {"text", "urn:oasis:names:tc:opendocument:xmlns:text:1.0"},
            {"draw", "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0"},
            {"fo", "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0"},
            {"dc", "http://purl.org/dc/elements/1.1/"},
            {"meta", "urn:oasis:names:tc:opendocument:xmlns:meta:1.0"},
            {"number", "urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0"},
            {"presentation", "urn:oasis:names:tc:opendocument:xmlns:presentation:1.0"},
            {"svg", "urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0"},
            {"chart", "urn:oasis:names:tc:opendocument:xmlns:chart:1.0"},
            {"dr3d", "urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0"},
            {"math", "http://www.w3.org/1998/Math/MathML"},
            {"form", "urn:oasis:names:tc:opendocument:xmlns:form:1.0"},
            {"script", "urn:oasis:names:tc:opendocument:xmlns:script:1.0"},
            {"ooo", "http://openoffice.org/2004/office"},
            {"ooow", "http://openoffice.org/2004/writer"},
            {"oooc", "http://openoffice.org/2004/calc"},
            {"dom", "http://www.w3.org/2001/xml-events"},
            {"xforms", "http://www.w3.org/2002/xforms"},
            {"xsd", "http://www.w3.org/2001/XMLSchema"},
            {"xsi", "http://www.w3.org/2001/XMLSchema-instance"},
            {"rpt", "http://openoffice.org/2005/report"},
            {"of", "urn:oasis:names:tc:opendocument:xmlns:of:1.2"},
            {"rdfa", "http://docs.oasis-open.org/opendocument/meta/rdfa#"},
            {"config", "urn:oasis:names:tc:opendocument:xmlns:config:1.0"}
        };


        public string _insert2 = "";
        public string _insert3 = "";
        public string _insert4 = "";
        public string _insert5 = "";
        public string _insert6 = "";
        public string _insert7 = "";
        public string _insert8 = "";
        public string _insert9 = "";
        public string _insert10 = "";
        public string _insert11 = "";
        public string _insert12 = "";
        public string _insert13 = "";
        public string _insert14 = "";
        public string _insert15 = "";
        public string _insert16 = "";
        public string _insert17 = "";
        public bool isAllNull = false;


        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "D:\\TestFolder\\TestOdsReader_P1\\TestOdsReader_P1\\bin\\Debug\\odsfile.ods";
            openFileDialog1.Filter = "ods files (*.ods)|*.ods|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        label1.Text = openFileDialog1.FileName; // 包含路徑
                        string inputFilePath = openFileDialog1.FileName; // 包含路徑
                        DataSet ds = ReadOdsFile(inputFilePath);

                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = ds; // dataset
                        dataGridView1.DataSource = ds.Tables[0];

                        //using (myStream)
                        //{
                        //    // Insert code to read the stream here.
                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


        // Read zip file (.ods file is zip file).
        private ZipFile GetZipFile(string inputFilePath)
        {
            return ZipFile.Read(inputFilePath);
        }


        private XmlDocument GetContentXmlFile(ZipFile zipFile)
        {
            // Get file(in zip archive) that contains data ("content.xml").
            ZipEntry contentZipEntry = zipFile["content.xml"];

            // Extract that file to MemoryStream.
            Stream contentStream = new MemoryStream();
            contentZipEntry.Extract(contentStream);
            contentStream.Seek(0, SeekOrigin.Begin);

            // Create XmlDocument from MemoryStream (MemoryStream contains content.xml).
            XmlDocument contentXml = new XmlDocument();
            contentXml.Load(contentStream);

            return contentXml;
        }


        private XmlNamespaceManager InitializeXmlNamespaceManager(XmlDocument xmlDocument)
        {
            XmlNamespaceManager nmsManager = new XmlNamespaceManager(xmlDocument.NameTable);

            for (int i = 0; i < namespaces.GetLength(0); i++)
                nmsManager.AddNamespace(namespaces[i, 0], namespaces[i, 1]);

            return nmsManager;
        }

        /// <summary>
        /// Read .ods file and store it in DataSet.
        /// </summary>
        /// <param name="inputFilePath">Path to the .ods file.</param>
        /// <returns>DataSet that represents .ods file.</returns>
        public DataSet ReadOdsFile(string inputFilePath)
        {
            ZipFile odsZipFile = this.GetZipFile(inputFilePath);

            // Get content.xml file
            XmlDocument contentXml = this.GetContentXmlFile(odsZipFile);

            // Initialize XmlNamespaceManager
            XmlNamespaceManager nmsManager = this.InitializeXmlNamespaceManager(contentXml);

            DataSet odsFile = new DataSet(Path.GetFileName(inputFilePath));

            foreach (XmlNode tableNode in this.GetTableNodes(contentXml, nmsManager))
                odsFile.Tables.Add(this.GetSheet(tableNode, nmsManager));

            return odsFile;
        }


        // In ODF sheet is stored in table:table node
        private XmlNodeList GetTableNodes(XmlDocument contentXmlDocument, XmlNamespaceManager nmsManager)
        {
            return contentXmlDocument.SelectNodes("/office:document-content/office:body/office:spreadsheet/table:table", nmsManager);
        }


        private DataTable GetSheet(XmlNode tableNode, XmlNamespaceManager nmsManager)
        {
            DataTable sheet = new DataTable(tableNode.Attributes["table:name"].Value);
            /*
                        XmlNodeList rowNodes = tableNode.SelectNodes("table:table-row", nmsManager);

                        int rowIndex = 0;

                        foreach (XmlNode rowNode in rowNodes)
                            this.GetRow(rowNode, sheet, nmsManager, ref rowIndex);
            */

            if (sheet.ToString() == "{匯入範例}" || sheet.ToString() == "匯入範例")
            {

                XmlNodeList rowNodes = tableNode.SelectNodes("table:table-row", nmsManager);

                int rowIndex = 0; // ori = 0
                foreach (XmlNode rowNode in rowNodes)
                {
                    this.GetRow(rowNode, sheet, nmsManager, ref rowIndex);

                    if ((rowIndex >= 2) && isAllNull == true)
                    {
                        //Trace.WriteLine("rowIndex: " + rowIndex);
                        //Trace.WriteLine("總共" + (rowIndex - 1) + "行");
                        break;
                    }
                    else
                    {
                        //Trace.WriteLine(" not null rowIndex: " + (rowIndex - 1));
                    }
                    ReSetInsertParam();
                }
            }
            // 匯入範例

            return sheet;
        }


        private void GetRow(XmlNode rowNode, DataTable sheet, XmlNamespaceManager nmsManager, ref int rowIndex)
        {
            XmlAttribute rowsRepeated = rowNode.Attributes["table:number-rows-repeated"];
            if (rowsRepeated == null || Convert.ToInt32(rowsRepeated.Value, CultureInfo.InvariantCulture) == 1)
            {
                while (sheet.Rows.Count < rowIndex)
                    sheet.Rows.Add(sheet.NewRow());

                DataRow row = sheet.NewRow();

                XmlNodeList cellNodes = rowNode.SelectNodes("table:table-cell", nmsManager);

                int cellIndex = 0;
                // ori
                //foreach (XmlNode cellNode in cellNodes)
                //    this.GetCell(cellNode, row, nmsManager, ref cellIndex);

                // 20211101 modify  ///////////////////////////////////////////////////////
                foreach (XmlNode cellNode in cellNodes)
                {
                    this.GetCell(cellNode, row, nmsManager, ref cellIndex, rowIndex.ToString());
                }
                ////////////////////////////////////////////////////////////////////////////////

                sheet.Rows.Add(row);

                rowIndex++;

                Trace.WriteLine("-----------------start---------------------------第" + rowIndex + "行開始");
                Trace.WriteLine("_insert2:" + _insert2);
                Trace.WriteLine(",_insert3:" + _insert3);
                Trace.WriteLine(",_insert4:" + _insert4);
                Trace.WriteLine(",_insert5:" + _insert5);
                Trace.WriteLine(",_insert6:" + _insert6);
                Trace.WriteLine(",_insert7:" + _insert7);
                Trace.WriteLine(",_insert8:" + _insert8);
                Trace.WriteLine(",_insert9:" + _insert9);
                Trace.WriteLine(",_insert10:" + _insert10);
                Trace.WriteLine(",_insert11:" + _insert11);
                Trace.WriteLine(",_insert12:" + _insert12);
                Trace.WriteLine(",_insert13:" + _insert13);
                Trace.WriteLine(",_insert14:" + _insert14);
                Trace.WriteLine(",_insert15:" + _insert15);
                Trace.WriteLine(",_insert16:" + _insert16);
                Trace.WriteLine(",_insert17:" + _insert17);
                Trace.WriteLine("--------------------end--------------------------第" + rowIndex + "行結束");
            }
            else
            {
                rowIndex += Convert.ToInt32(rowsRepeated.Value, CultureInfo.InvariantCulture);
            }

            // sheet must have at least one cell
            if (sheet.Rows.Count == 0)
            {
                sheet.Rows.Add(sheet.NewRow());
                sheet.Columns.Add();
            }
        }


        /// <summary>
        /// ori GetCell
        /// </summary>
        /// <param name="cellNode"></param>
        /// <param name="row"></param>
        /// <param name="nmsManager"></param>
        /// <param name="cellIndex"></param>
        private void GetCell(XmlNode cellNode, DataRow row, XmlNamespaceManager nmsManager, ref int cellIndex)
        {
            XmlAttribute cellRepeated = cellNode.Attributes["table:number-columns-repeated"];

            if (cellRepeated == null)
            {
                DataTable sheet = row.Table;

                while (sheet.Columns.Count <= cellIndex)
                    sheet.Columns.Add();

                row[cellIndex] = this.ReadCellValue(cellNode);

                cellIndex++;
            }
            else
            {
                cellIndex += Convert.ToInt32(cellRepeated.Value, CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// ori ReadCellValue
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string ReadCellValue(XmlNode cell)
        {
            XmlAttribute cellVal = cell.Attributes["office:value"];

            if (cellVal == null)
                return String.IsNullOrEmpty(cell.InnerText) ? null : cell.InnerText;
            else
                return cellVal.Value;
        }


        /////////////////// Modify Function Version //////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// // 20211101 modify
        /// </summary>
        /// <param name="cellNode"></param>
        /// <param name="row"></param>
        /// <param name="nmsManager"></param>
        /// <param name="cellIndex"></param>
        /// <param name="rowindex"></param>
        private void GetCell(XmlNode cellNode, DataRow row, XmlNamespaceManager nmsManager, ref int cellIndex, string rowindex)
        {
            XmlAttribute cellRepeated = cellNode.Attributes["table:number-columns-repeated"];
            string _row = "";
            _row = rowindex;

            if (cellRepeated == null)
            {
                DataTable sheet = row.Table;

                while (sheet.Columns.Count <= cellIndex)
                    sheet.Columns.Add();

                //row[cellIndex] = this.ReadCellValue(cellNode);
                row[cellIndex] = this.ReadCellValue(cellNode, cellIndex.ToString(), _row.ToString());
                cellIndex++;
            }
            else
            {
                cellIndex += Convert.ToInt32(cellRepeated.Value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// // 20211101 modify
        /// cellIndex: 0開始
        /// 0:門市代號
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        private string ReadCellValue(XmlNode cell, string cellIndex, string row)
        {
            XmlAttribute cellVal = cell.Attributes["office:value"];

            string _cellIndex = "";
            _cellIndex = cellIndex;

            string _row = "";
            _row = row;

            string _value = "";

            /* cellIndex: 欄位代表含意
                * 2 廠商名稱(必填)
                * 3 廠商類型1(必填)
                * 4 廠商類型2(必填)
                * 5 負責人
                * 6 統一編號
                * 7 販賣業執照號碼
                * 8 連絡電話
                * 9 分機號碼
                * 10 聯絡人姓名
                * 11 聯絡人職稱
                * 12 行動電話
                * 13 地址(縣市)
                * 14 地址(鄉鎮區)
                * 15 地址(詳細路名門牌樓數)
                * 16 傳真機號碼
                * 17 電子信箱
             */

            //if (cellVal == null)
            //    return String.IsNullOrEmpty(cell.InnerText) ? null : cell.InnerText;
            //else
            //    return cellVal.Value;


            // cellIndex:要從廠商名稱開始抓, _row要從使用者填寫那邊開始 去除sample範例
            // 取值出來判斷是否有填寫或是 填寫錯誤 要從這邊開始檢查 第三行開始
            if ((int.Parse(_row.ToString()) >= 2) && (int.Parse(_cellIndex.ToString()) >= 2) && (int.Parse(_cellIndex.ToString()) < 18))
            {
                string _tmpvalue = "";
                if (cellVal == null)
                {
                    _tmpvalue = String.IsNullOrEmpty(cell.InnerText) ? "" : cell.InnerText;
                }
                else
                {
                    _tmpvalue = cellVal.Value;
                }


                if (_cellIndex.ToString() == "2")
                {
                    _insert2 = _tmpvalue;
                    //Trace.WriteLine("_insert2: " + _insert2);
                    if (string.IsNullOrEmpty(_insert2.ToString().Trim()))
                    {
                        Trace.WriteLine("廠商名稱(必填),");
                    }
                    else
                    {
                        Trace.WriteLine("_insert2: " + _insert2);
                    }
                }
                if (_cellIndex.ToString() == "3")
                {
                    _insert3 = _tmpvalue;

                    if (string.IsNullOrEmpty(_insert3.ToString().Trim()))
                    {
                        Trace.WriteLine("廠商類型1(必填), ");
                    }
                    else
                    {
                        Trace.WriteLine("_insert3: " + _insert3);
                    }
                }
                if (_cellIndex.ToString() == "4")
                {
                    _insert4 = _tmpvalue;

                    if (string.IsNullOrEmpty(_insert4.ToString().Trim()))
                    {
                        Trace.WriteLine("廠商類型2(必填), ");
                    }
                    else
                    {
                        Trace.WriteLine("_insert4: " + _insert4);
                    }
                }
                if (_cellIndex.ToString() == "5")
                {
                    _insert5 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "6")
                {
                    _insert6 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "7")
                {
                    _insert7 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "8")
                {
                    _insert8 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "9")
                {
                    _insert9 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "10")
                {
                    _insert10 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "11")
                {
                    _insert11 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "12")
                {
                    _insert12 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "13")
                {
                    _insert13 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "14")
                {
                    _insert14 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "15")
                {
                    _insert15 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "16")
                {
                    _insert16 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "17")
                {
                    _insert17 = _tmpvalue;
                }

                _value = _tmpvalue;
                Checkisallnull();
            }
            else if((int.Parse(_row.ToString()) < 2) && (int.Parse(_cellIndex.ToString()) < 2))
            {
                isAllNull = false;
            }
            //else
            //{
            //    isAllNull = true;
            //}
            //else
            //{
            //    string _tmpvalue2 = "";
            //    if (cellVal == null)
            //    {
            //        _tmpvalue2 = String.IsNullOrEmpty(cell.InnerText) ? null : cell.InnerText;
            //    }
            //    else
            //    {
            //        _tmpvalue2 = cellVal.Value;
            //    }
            //    _value = _tmpvalue2;
            //}
            return _value;

        }


        public void Checkisallnull()
        {
            if ((_insert2 == "") && (_insert3 == "") && (_insert4 == "") && (_insert5 == "") && (_insert6 == "") && (_insert7 == "") && (_insert8 == "") && (_insert9 == "") && (_insert10 == "") && (_insert11 == "") && (_insert12 == "") && (_insert13 == "") && (_insert14 == "") && (_insert15 == "") && (_insert16 == "") && (_insert17 == ""))
            {
                isAllNull = true;
            }
            else
            {
                isAllNull = false;
            }
        }


        /// <summary>
        /// 換 row 就清空
        /// </summary>
        public void ReSetInsertParam()
        {
            _insert2 = "";
            _insert3 = "";
            _insert4 = "";
            _insert5 = "";
            _insert6 = "";
            _insert7 = "";
            _insert8 = "";
            _insert9 = "";
            _insert10 = "";
            _insert11 = "";
            _insert12 = "";
            _insert13 = "";
            _insert14 = "";
            _insert15 = "";
            _insert16 = "";
            _insert17 = "";
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 20210608
        /// 身份證號驗證, 新居留證驗證
        /// 參考 dotblogs.com.tw/ChentingW/2020/03/28/205520
        /// </summary>
        /// <param name="idNo">證號</param>
        /// <param name="IdNoType">類別 1.身分證 2.新式居留證</param>
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="IdNoType"></param>
        /// <returns></returns>
        public static bool IsIdNo(string idNo, int IdNoType = 1)
        {
            if (idNo == null || idNo.Length != 10)
            {
                return false;
            }
            idNo = idNo.ToUpper();

            string IdNoTypeString = "";
            if (IdNoType == 1)
                IdNoTypeString = @"^([A-Z])([1-2]\d{8})$";
            else if (IdNoType == 2)
                IdNoTypeString = @"^([A-Z])([8-9]\d{8})$";

            Regex regex = new Regex(IdNoTypeString);
            Match match = regex.Match(idNo);
            if (!match.Success)
            {
                return false;
            }

            ///建立字母對應表(A~Z)
            ///A=10 B=11 C=12 D=13 E=14 F=15 G=16 H=17 J=18 K=19 L=20 M=21 N=22
            ///P=23 Q=24 R=25 S=26 T=27 U=28 V=29 X=30 Y=31 W=32  Z=33 I=34 O=35 
            string alphabet = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            string transferIdNo = alphabet.IndexOf(match.Groups[1].Value) + 10 + match.Groups[2].Value;
            int[] idNoArray = transferIdNo.ToCharArray()
                                          .Select(c => Convert.ToInt32(c.ToString()))
                                          .ToArray();
            int sum = idNoArray[0];
            int[] weight = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 1 };
            for (int i = 0; i < weight.Length; i++)
            {
                sum += weight[i] * idNoArray[i + 1];
            }
            return (sum % 10 == 0);
        }


        /// <summary>
        /// 新舊居留證驗證
        /// 參考 dotblogs.com.tw/ChentingW/2020/03/29/001426
        /// </summary>
        /// <param name="ResNo">居留證</param>
        /// <returns></returns>
        public static bool IsResNo(string ResNo)
        {
            if (ResNo == null)
            {
                return false;
            }
            ResNo = ResNo.ToUpper();
            Regex regex = new Regex(@"^([A-Z])(A|B|C|D|8|9)(\d{8})$");
            Match match = regex.Match(ResNo);
            if (!match.Success)
                return false;
            if ("ABCD".IndexOf(match.Groups[2].Value) >= 0)
                return IsResNoOld(ResNo);
            else
                return IsIdNo(ResNo, 2);
        }


        /// <summary>
        /// 舊居留證號驗證
        /// </summary>
        /// <param name="arg_ResNo"></param>
        /// <returns></returns>
        public static bool IsResNoOld(string arg_ResNo)
        {

            var d = false;
            if (arg_ResNo.Length == 10)
            {

                arg_ResNo = arg_ResNo.ToUpper();
                if (arg_ResNo[0] >= 0x41 && arg_ResNo[0] <= 0x5A)  //同身份證號
                {
                    if (arg_ResNo[1] >= 0x41 && arg_ResNo[1] <= 0x44)  //第二碼必是 ABCD
                    {
                        //首二個英文字母同身分證字號檢核轉換成數字，取第一個英文字母轉換後的全部數字及第二個英文字母轉換後的個位數字，將此三個數字再與證號後八碼數字，可串成11個數字。

                        var a = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
                        var b = new int[11];

                        b[0] = a[(arg_ResNo[0]) - 65] / 10;
                        b[1] = a[(arg_ResNo[0]) - 65] % 10;
                        b[2] = a[(arg_ResNo[1]) - 65] % 10;

                        var c = b[0];  //*1  
                        c += b[1] * 9;  //*9

                        for (var i = 2; i <= 9; i++)   //*8-1
                        {
                            b[i + 1] = arg_ResNo[i] - 48;
                            c += b[i] * (10 - i);
                        }
                        //1,9,8,7,6,5,4,3,2,1,1
                        if (((c % 10) + b[10]) % 10 == 0)
                        {
                            d = true;
                        }
                    }
                }
            }
            return d;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
