using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using Converter = ExcelParser.ExcelCellNameConverter;
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

            xlApp.ScreenUpdating = false;
        }

        public List<string> GetKanjis(string cell) {
            List<string> cellValues = new List<string>();

            for (int i = 4; i <= 4; i++) { // burnt in values for now
                cellValues.Add(GetKanji(2, i));
            }

            return cellValues;
        }

        public string GetKanji(int column, int row) {
            return GetKanji(Converter.ExcelCellIndicesToName(column, row));
        }

        public string GetKanji(string range) {
            xlRange = xlWorksheet.get_Range(range);
            var cellValueVar = xlRange.Value;
            
            if (cellValueVar != null) {
                return cellValueVar.ToString();
            }

            return null;
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