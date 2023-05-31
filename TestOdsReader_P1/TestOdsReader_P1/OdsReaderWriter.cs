using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Ionic.Zip;
using System.Diagnostics;

namespace OdsReadWrite
{
    internal sealed class OdsReaderWriter
    {
        // Namespaces. We need this to initialize XmlNamespaceManager so that we can search XmlDocument.
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

        // Read zip stream (.ods file is zip file).
        private ZipFile GetZipFile(Stream stream)
        {
            return ZipFile.Read(stream);
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

            if (sheet.ToString() == "{匯入範例}" || sheet.ToString() == "匯入範例")
            {

                XmlNodeList rowNodes = tableNode.SelectNodes("table:table-row", nmsManager);

                int rowIndex = 0; // ori = 0
                foreach (XmlNode rowNode in rowNodes)
                {
                    this.GetRow(rowNode, sheet, nmsManager, ref rowIndex);

                    if ((rowIndex >= 2) && isAllNull == true)
                    {
                        Trace.WriteLine("rowIndex: " + rowIndex);
                        break;
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

                int cellIndex = 0; // ori = 0
                //foreach (XmlNode cellNode in cellNodes)
                //    this.GetCell(cellNode, row, nmsManager, ref cellIndex);



                // 20211101 modify 
                foreach (XmlNode cellNode in cellNodes)
                {
                    this.GetCell(cellNode, row, nmsManager, ref cellIndex, rowIndex.ToString());
                }

                sheet.Rows.Add(row);

                rowIndex++;
                //Checkisallnull();
                //Trace.WriteLine("text");
                //ReSetInsertParam(); // 20211101 reset
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
        /// cellIndex: 第幾個欄位
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

                //row[cellIndex] = this.ReadCellValue(cellNode);
                row[cellIndex] = this.ReadCellValue(cellNode, cellIndex.ToString(), row.ToString());
                cellIndex++;
            }
            else
            {
                cellIndex += Convert.ToInt32(cellRepeated.Value, CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        /// 讀取欄位 值
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
             * 0: 門市代號
             * 1: 廠商編號
             * 2: 廠商名稱
             * 3: 廠商類型1
             * 4: 廠商類型2
             * 5: 負責人
             * 6: 統一編號
             * 7: 販賣業執照號碼
             * 8: 連絡電話
             * 9: 分機號碼
             * 10: 聯絡人姓名
             * 11: 聯絡人職稱
             * 12: 行動電話 
             * 13: 地址(縣市)
             * 14: 地址(鄉鎮區)
             * 15: 地址(詳細門牌)
             * 16: 傳真機號
             * 17: 電子信箱
             */

            //if (cellVal == null)
            //    return String.IsNullOrEmpty(cell.InnerText) ? null : cell.InnerText;
            //else
            //    return cellVal.Value;


            // cellIndex:要從廠商名稱開始抓, _row要從使用者填寫那邊開始 去除sample範例
            if ((int.Parse(_row.ToString()) >= 2) && (int.Parse(_cellIndex.ToString()) >= 2) && (int.Parse(_cellIndex.ToString()) < 18))
            {
                string _tmpvalue = "";
                if (cellVal == null)
                {
                    _tmpvalue = String.IsNullOrEmpty(cell.InnerText) ? "" : cell.InnerText; ;
                }
                else
                {
                    _tmpvalue = cellVal.Value;
                }


                if (_cellIndex.ToString() == "2")
                {
                    _insert2 = _tmpvalue;
                    //Trace.WriteLine("_insert2: " + _insert2);
                }
                if (_cellIndex.ToString() == "3")
                {
                    _insert3 = _tmpvalue;
                }
                if (_cellIndex.ToString() == "4")
                {
                    _insert4 = _tmpvalue;
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
            else
            {
                string _tmpvalue2 = "";
                if (cellVal == null)
                {
                    _tmpvalue2 = String.IsNullOrEmpty(cell.InnerText) ? null : cell.InnerText; ;
                }
                else
                {
                    _tmpvalue2 = cellVal.Value;
                }
                _value = _tmpvalue2;
            }




            return _value;

        }

        /// <summary>
        /// Writes DataSet as .ods file.
        /// </summary>
        /// <param name="odsFile">DataSet that represent .ods file.</param>
        /// <param name="outputFilePath">The name of the file to save to.</param>
        public void WriteOdsFile(DataSet odsFile, string outputFilePath)
        {
            ZipFile templateFile = this.GetZipFile(Assembly.GetExecutingAssembly().GetManifestResourceStream("OdsReadWrite.template.ods"));

            XmlDocument contentXml = this.GetContentXmlFile(templateFile);

            XmlNamespaceManager nmsManager = this.InitializeXmlNamespaceManager(contentXml);

            XmlNode sheetsRootNode = this.GetSheetsRootNodeAndRemoveChildrens(contentXml, nmsManager);

            foreach (DataTable sheet in odsFile.Tables)
                this.SaveSheet(sheet, sheetsRootNode);

            this.SaveContentXml(templateFile, contentXml);

            templateFile.Save(outputFilePath);
        }

        private XmlNode GetSheetsRootNodeAndRemoveChildrens(XmlDocument contentXml, XmlNamespaceManager nmsManager)
        {
            XmlNodeList tableNodes = this.GetTableNodes(contentXml, nmsManager);

            XmlNode sheetsRootNode = tableNodes.Item(0).ParentNode;
            // remove sheets from template file
            foreach (XmlNode tableNode in tableNodes)
                sheetsRootNode.RemoveChild(tableNode);

            return sheetsRootNode;
        }

        private void SaveSheet(DataTable sheet, XmlNode sheetsRootNode)
        {
            XmlDocument ownerDocument = sheetsRootNode.OwnerDocument;

            XmlNode sheetNode = ownerDocument.CreateElement("table:table", this.GetNamespaceUri("table"));

            XmlAttribute sheetName = ownerDocument.CreateAttribute("table:name", this.GetNamespaceUri("table"));
            sheetName.Value = sheet.TableName;
            sheetNode.Attributes.Append(sheetName);

            this.SaveColumnDefinition(sheet, sheetNode, ownerDocument);

            this.SaveRows(sheet, sheetNode, ownerDocument);

            sheetsRootNode.AppendChild(sheetNode);
        }

        private void SaveColumnDefinition(DataTable sheet, XmlNode sheetNode, XmlDocument ownerDocument)
        {
            XmlNode columnDefinition = ownerDocument.CreateElement("table:table-column", this.GetNamespaceUri("table"));

            XmlAttribute columnsCount = ownerDocument.CreateAttribute("table:number-columns-repeated", this.GetNamespaceUri("table"));
            columnsCount.Value = sheet.Columns.Count.ToString(CultureInfo.InvariantCulture);
            columnDefinition.Attributes.Append(columnsCount);

            sheetNode.AppendChild(columnDefinition);
        }

        private void SaveRows(DataTable sheet, XmlNode sheetNode, XmlDocument ownerDocument)
        {
            DataRowCollection rows = sheet.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                XmlNode rowNode = ownerDocument.CreateElement("table:table-row", this.GetNamespaceUri("table"));

                this.SaveCell(rows[i], rowNode, ownerDocument);

                sheetNode.AppendChild(rowNode);
            }
        }

        private void SaveCell(DataRow row, XmlNode rowNode, XmlDocument ownerDocument)
        {
            object[] cells = row.ItemArray;

            for (int i = 0; i < cells.Length; i++)
            {
                XmlElement cellNode = ownerDocument.CreateElement("table:table-cell", this.GetNamespaceUri("table"));

                if (row[i] != DBNull.Value)
                {
                    // We save values as text (string)
                    XmlAttribute valueType = ownerDocument.CreateAttribute("office:value-type", this.GetNamespaceUri("office"));
                    valueType.Value = "string";
                    cellNode.Attributes.Append(valueType);

                    XmlElement cellValue = ownerDocument.CreateElement("text:p", this.GetNamespaceUri("text"));
                    cellValue.InnerText = row[i].ToString();
                    cellNode.AppendChild(cellValue);
                }

                rowNode.AppendChild(cellNode);
            }
        }

        private void SaveContentXml(ZipFile templateFile, XmlDocument contentXml)
        {
            templateFile.RemoveEntry("content.xml");

            MemoryStream memStream = new MemoryStream();
            contentXml.Save(memStream);
            memStream.Seek(0, SeekOrigin.Begin);

            templateFile.AddEntry("content.xml", memStream);
        }

        private string GetNamespaceUri(string prefix)
        {
            for (int i = 0; i < namespaces.GetLength(0); i++)
            {
                if (namespaces[i, 0] == prefix)
                    return namespaces[i, 1];
            }

            throw new InvalidOperationException("Can't find that namespace URI");
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

        public void Checkisallnull()
        {
            if((_insert2 == "") && (_insert3 == "") && (_insert4 == "") && (_insert5 == "") && (_insert6 == "") && (_insert7 == "") && (_insert8 == "") && (_insert9 == "") && (_insert10 == "") && (_insert11 == "") && (_insert12 == "") && (_insert13 == "") && (_insert14 == "") && (_insert15 == "") && (_insert16 == "") && (_insert17 == ""))
            {
                isAllNull = true;
            }
            else
            {
                isAllNull = false;
            }
        }


    }
}
