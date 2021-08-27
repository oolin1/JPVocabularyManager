using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace ExcelReader {
    public class ExcelReader : IDisposable {
        private Excel.Application xlApp;
        private Excel.Workbooks xlWorkbooks;
        private Excel.Workbook xlWorkbook;
        private Excel.Sheets xlWorksheets;
        private Excel._Worksheet xlWorksheet;
        private List<Excel.Range> xlRanges;

        private readonly string filePath;
        private readonly string sheetName;
        
        private bool isDisposed;

        public ExcelReader(string filePath, string sheetName) {
            this.filePath = filePath;
            this.sheetName = sheetName;

            xlApp = new Excel.Application();
            xlWorkbooks = xlApp.Workbooks;
            xlWorkbook = xlWorkbooks.Open(filePath);
            xlWorksheets = xlWorkbook.Worksheets;
            xlWorksheet = xlWorksheets[sheetName];
            xlRanges = new List<Excel.Range>();

            xlApp.ScreenUpdating = false;
        }

        public List<string> GetRangeValues(string from, string to) {
            if (from == to) {
                return GetCellValue(from);
            }

            List<string> result = new List<string>();
            Excel.Range xlRange = xlWorksheet.get_Range(from, to);
            xlRanges.Add(xlRange);
            var cellValueVar = xlRange.Value;

            (int, int) distance = ExcelHelper.CalculateCellDistance(from, to);
            for (int i = 0; i <= distance.Item2; i++) {
                for (int j = 0; j <= distance.Item1; j++) {
                    if (cellValueVar[i+1, j+1] != null) {
                        result.Add(cellValueVar[i+1, j+1].ToString());
                    }
                }
            }

            return result;
        }

        private List<string> GetCellValue(string cell) {
            Excel.Range xlRange = xlWorksheet.get_Range(cell);
            xlRanges.Add(xlRange);
            
            return xlRange.Value != null ? new List<string>() { xlRange.Value.ToString() }  : new List<string>();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (isDisposed) {
                return;
            }

            for (int i=0; i<xlRanges.Count; i++) {
                Marshal.ReleaseComObject(xlRanges[i]);
            }
            Marshal.ReleaseComObject(xlWorksheet);
            Marshal.ReleaseComObject(xlWorksheets);
            xlWorkbook.Close(true);
            Marshal.ReleaseComObject(xlWorkbook);
            Marshal.ReleaseComObject(xlWorkbooks);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            if (disposing) {
                xlApp = null;
                xlWorkbooks = null;
                xlWorkbook = null;
                xlWorksheets = null;
                xlWorksheet = null;
                xlRanges = null;
            }

            isDisposed = true;
        }

        ~ExcelReader() {
            Dispose(false);
        }
    }
}