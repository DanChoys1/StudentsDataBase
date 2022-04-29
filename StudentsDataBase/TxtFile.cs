/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Files
{
    internal static class TxtFile
    {
        public static bool Read(string path, out double a, out double xBorder, out double step)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                try
                {
                    a = Convert.ToDouble(sr.ReadLine());
                    xBorder = Convert.ToDouble(sr.ReadLine());
                    step = Convert.ToDouble(sr.ReadLine());
                }
                catch (Exception)
                {
                    a = 0;
                    xBorder = 0;
                    step = 0;

                    return false;
                }

                return true;
            };
        }

        public static bool Write(string path, PointD[] points, double a, double xBorder, double step)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
                {
                    sw.WriteLine(a);
                    sw.WriteLine(xBorder);
                    sw.WriteLine(step);

                    foreach (PointD point in points)
                    {
                        string pointValues = point.X.ToString() + " " + point.Y.ToString();
                        sw.WriteLine(pointValues);
                    }
                }
            }
            catch (IOException)
            {
                return false;
            }
            
            return true;
        }
    }
}
*/