using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelParser {
    public class ExcelReader {
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel._Worksheet xlWorksheet;

        private string filePath;
        private string sheetName;

        public ExcelReader(string filePath, string sheetName) {
            this.filePath = filePath;
            this.sheetName = sheetName;

            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(filePath);
            xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[sheetName];
        }

        public string GetKanji(int i, int j) {
            string cellValue = null;
            var cellValueVar = (xlWorksheet.Cells[i, j] as Excel.Range).Value;
            if (cellValueVar != null) {
                cellValue = cellValueVar.ToString();
            }

            return cellValue;
        }
    }
}
