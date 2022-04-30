using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Files
{
    internal static class TxtFile
    {
        public static bool Read(string path, out string[,] dataList, int rowRange, int columnRange)
        {
            if (rowRange < 1 || columnRange < 1)
            {
                dataList = new string[0, 0];
                return false;
            }

            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                if (sr == null)
                {
                    dataList = new string[0, 0];
                    return false;
                }

                dataList = new string[rowRange, columnRange];

                for (int i = 0; i < rowRange; i++)
                {
                    string line = sr.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    string[] data = line.Split(" ");

                    for (int j = 0; j < columnRange; j++)
                    {
                        if (data.Length <= j)
                        {
                            continue;
                        }

                        dataList[i, j] = data[j];
                    }
                }

            }

            return true;
        }

        public static bool Write(string path, string[,] dataList, int startRow = 0, int startColumn = 0)
        {
            if (startRow < 0 || startColumn < 0)
            {
                return false;
            }

            using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
            {
                if (sw == null)
                {
                    return false;
                }

                for (int i = 0; i < startColumn; i++)
                { 
                    sw.WriteLine();
                }

                string columnShift = "";

                for (int i = 0; i < startColumn; i++)
                {
                    columnShift += " ";
                }

                int countRows = dataList.GetUpperBound(0) + 1;
                int countColumns = dataList.Length / countRows;

                for (int i = 0; i < countRows; i++)
                {
                    sw.Write(columnShift);

                    string data = dataList[i, 0] == "" ? " " : dataList[i, 0];
                    sw.Write(data);

                    for (int j = 1; j < countColumns; j++)
                    {
                        data = dataList[i, j] == "" ? " " : dataList[i, j];
                        sw.Write(" " + data);
                    }

                    sw.WriteLine();
                }
            }

            return true;
        }
    }
}
