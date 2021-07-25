using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using Converter = ExcelParser.ExcelCellNameConverter;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace ExcelParser {
    public class ExcelReader : IDisposable {
        private readonly Excel.Application xlApp;
        private readonly Excel.Workbooks xlWorkbooks;
        private readonly Excel.Workbook xlWorkbook;
        private readonly Excel.Sheets xlWorksheets;
        private readonly Excel._Worksheet xlWorksheet;
        private readonly List<Excel.Range> xlRanges;

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

        public List<string> GetKanjis(string cell) {
            List<string> cellValues = GetRangeValues("B4", "B5");
            
            return cellValues;
        }

        public string GetKanji(int column, int row) {
            return GetCellValue(Converter.ExcelCellIndicesToName(column, row));
        }

        public List<string> GetRangeValues(string from, string to) {
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

        public string GetCellValue(string cell) {
            Excel.Range xlRange = xlWorksheet.get_Range(cell);
            xlRanges.Add(xlRange);

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

            isDisposed = true;
        }
    }
}