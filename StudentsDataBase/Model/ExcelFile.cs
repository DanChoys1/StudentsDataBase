using System;
using System.Collections.Generic;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;
using System.IO;

namespace Files
{
    internal static class ExcelFile
    {
        static ExcelFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static bool Read(string path, out string[,] dataList, int rowRange, int columnRange)
        {
            if (rowRange < 1 || columnRange < 1)
            {
                dataList = new string[0,0];
                return false;
            }

            FileInfo file = new FileInfo(path);

            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                if (excelPackage == null || excelPackage.Workbook.Worksheets.Count < 1)
                {
                    dataList = new string[0, 0];
                    return false;
                }

                dataList = new string[rowRange, columnRange];

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                for (int i = 1; i <= rowRange; i++)
                {
                    for (int j = 1; j <= columnRange; j++)
                    {               
                        if (worksheet.Cells[i, j].Value != null)
                        {
                            dataList[i - 1, j - 1] = worksheet.Cells[i, j].Value.ToString();
                        }
                    }
                }                    
            }

            return true;
        }
  
        public static bool Write(string path, string[,] dataList, int startRow = 1, int startColumn = 1)
        {
            if (startRow < 1 || startColumn < 1)
            {
                return false;
            }

            FileInfo file = new FileInfo(path);

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                if (excelPackage == null)
                {
                    return false;
                }

                excelPackage.Workbook.Properties.Created = DateTime.Now;

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(file.Name);

                int countRows = dataList.GetUpperBound(0) + 1;
                int countColumns = dataList.Length / countRows;

                for (int i = 0; i < countRows; i++)
                {
                    for (int j = 0; j < countColumns; j++)
                    {
                        worksheet.Cells[i + startRow, j + startColumn].Value = dataList[i, j];
                    }
                }

                excelPackage.SaveAs(file);
            }

            return true;
        }
    }
}
