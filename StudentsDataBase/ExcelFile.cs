using System;
using System.Collections.Generic;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;
using System.IO;

namespace Files
{
    internal static class ExcelFile
    {
        private static int _row;
        public static int Row 
        { 
            get { return _row; }

            set
            {
                if (value > 1)
                {
                    _row = value;
                }
            } 
        }

        private static int _column;
        public static int Column
        {
            get {  return _column; }

            set
            {
                if (value > 1)
                {
                    _column = value;
                }
            }
        }

        static ExcelFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Row = 1;
            Column = 1;
        }

        public static bool Read(string path, out List<List<string>> datas)
        {
            datas = new List<List<string>>();
            datas.Add(new List<string>());

            try
            {
                FileInfo file = new FileInfo(path);

                using (ExcelPackage excelPackage = new ExcelPackage(file))
                {
                    //ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Декартов лист");
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                    do
                    {
                        string h = worksheet.Cells[1, 2].Value.ToString();
                    }
                    while ();
                }
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
  
        public static bool Write(string path)
        {
/*            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    excelPackage.Workbook.Properties.Author = "DanChoys";
                    excelPackage.Workbook.Properties.Title = "Графики";
                    excelPackage.Workbook.Properties.Subject = "Построение графиков";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;
                    
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Декартов лист");
                    
                    worksheet.Cells[_rowParametrsIndex, _aColumnIndex].Value = "a =";
                    worksheet.Cells[_rowParametrsIndex, _aColumnIndex + 1].Value = a;
                    worksheet.Cells[_rowParametrsIndex, _xBorderColumnIndex].Value = "xBorder =";
                    worksheet.Cells[_rowParametrsIndex, _xBorderColumnIndex + 1].Value = xBorder;
                    worksheet.Cells[_rowParametrsIndex, _stepColumnIndex].Value = "step =";
                    worksheet.Cells[_rowParametrsIndex, _stepColumnIndex + 1].Value = step;

                    worksheet.Cells[_startRowForPoints, _xColumnIndex].Value = "X";
                    worksheet.Cells[_startRowForPoints, _yColumnIndex].Value = "Y";

                    for (int i = 0; i < points.Length; i++)
                    {
                        worksheet.Cells[_startRowForPoints + i + 1, _xColumnIndex].Value = points[i].X;
                        worksheet.Cells[_startRowForPoints + i + 1, _yColumnIndex].Value = points[i].Y;
                    }

                    ExcelScatterChart сhart = worksheet.Drawings.AddChart("chart", eChartType.XYScatterLinesNoMarkers) as ExcelScatterChart;

                    сhart.Title.Text = "Декартов лист";

                    ExcelRange range1 = worksheet.Cells[_startRowForPoints + 1, _xColumnIndex, 
                                                 _startRowForPoints + points.Length, _xColumnIndex];
                    ExcelRange range2 = worksheet.Cells[_startRowForPoints + 1, _yColumnIndex,
                                                 _startRowForPoints + points.Length, _yColumnIndex];

                    сhart.Series.Add(range1, range2);
                    сhart.Series[0].Header = "Декартов лист";

                    сhart.SetSize(1000, 500);
                    сhart.SetPosition(5, 20, 10, 20);

                    FileInfo fi = new FileInfo(path);
                    excelPackage.SaveAs(fi);
                }
            }
            catch (IOException)
            {
                return false;
            }*/

            return true;
        }
    }
}
