using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

            xlApp.ScreenUpdating = false;
        }

        public List<string> GetKanjis(string range) {
            List<string> cellValues = new List<string>();

            for (int i = 4; i <= 4; i++) { // burnt in values for now
                cellValues.Add(GetKanji(2, i));
            }

            return cellValues;
        }

        public string GetKanji(int column, int row) {
            return GetKanji(ConvertIndexToExcelRange(column, row));
        }

        public string GetKanji(string range) {
            xlRange = xlWorksheet.get_Range(range);
            var cellValueVar = xlRange.Value;
            
            if (cellValueVar != null) {
                return cellValueVar.ToString();
            }

            return null;
        }

        public string ConvertIndexToExcelRange(int column, int row) {
            return string.Format("{0}{1}", ExcelColumnNumberToName(column), row.ToString());
        }

        public Tuple<int, int> ConvertExcelRangeToIndex(string range) { 
            /// TODO: safety checks
            string columnString = Regex.Match(range, @"[a-zA-Z]+").Value;
            string rowString = Regex.Match(range, @"[0-9]+").Value;

            int column = ExcelColumnNameToNumber(columnString);
            int row;
            if (!Int32.TryParse(rowString, out row)) {
                throw new InvalidCastException("todo tryparse failed");
            }

            return new Tuple<int, int>(column, row);
        }

        public int ExcelColumnNameToNumber(string columnName) {
            if (string.IsNullOrEmpty(columnName)) {
                throw new ArgumentNullException("columnName");
            }

            columnName = columnName.ToUpperInvariant();
            int sum = 0;

            for (int i = 0; i < columnName.Length; i++) {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }

        public string ExcelColumnNumberToName(int columnNumber) {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0) {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
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