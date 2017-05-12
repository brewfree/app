using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelRead
{
    class ExcelReader
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        string filePath;

        Excel.Application xlApp = new Excel.Application();
        Excel.Workbook xlWorkbook;
        Excel.Worksheet xlWorksheet;
        Excel.Range xlRange;

        public ExcelReader(string filePath)
        {
            this.filePath = filePath;

            xlWorkbook = xlApp.Workbooks.Open(filePath);
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;

            this.Rows = xlRange.Rows.Count;
            this.Columns = xlRange.Columns.Count;
        }

        public string[] GetLine(int index)
        {
            if (index < 1 || index > Rows)
            {
                throw new IndexOutOfRangeException();
            }
            var s = new string[Columns];

            for (var i = 1; i <= Columns; ++i)
            {
                if (xlRange.Cells[index, i] != null && xlRange.Cells[index, i].Value2 != null)
                {
                    s[i - 1] = xlRange.Cells[index, i].Value2.ToString();
                }
            }

            return s;
        }

        public void Close()
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
