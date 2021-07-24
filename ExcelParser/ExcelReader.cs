using System;
using Excel = Microsoft.Office.Interop.Excel;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace ExcelParser {
    public class ExcelReader : IDisposable {
        private Excel.Application xlApp;
        private Excel.Workbooks xlWorkbooks;
        private Excel.Workbook xlWorkbook;
        private Excel.Sheets xlWorksheets;
        private Excel._Worksheet xlWorksheet;
        private Excel.Range xlRange;

        private string filePath;
        private string sheetName;
        private bool isDisposed;

        public ExcelReader(string filePath, string sheetName) {
            this.filePath = filePath;
            this.sheetName = sheetName;

            xlApp = new Excel.Application();
            xlWorkbooks = xlApp.Workbooks;
            xlWorkbook = xlWorkbooks.Open(filePath);
            xlWorksheets = xlWorkbook.Worksheets;
            xlWorksheet = xlWorksheets[sheetName];
        }

        public string GetKanji(string range) {
            string cellValue = null;
            xlRange = xlWorksheet.get_Range(range);
            var cellValueVar = xlRange.Value;
            if (cellValueVar != null) {
                cellValue = cellValueVar.ToString();
            }

            return cellValue;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (isDisposed) {
                return;
            }

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            Marshal.ReleaseComObject(xlWorksheets);
            xlWorkbook.Save();
            xlWorkbook.Close(true);
            Marshal.ReleaseComObject(xlWorkbook);
            Marshal.ReleaseComObject(xlWorkbooks);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            xlRange = null;
            xlWorksheet = null;
            xlWorksheets = null;
            xlWorkbook = null;
            xlWorkbooks = null;
            xlApp = null;

            isDisposed = true;
        }
    }
}