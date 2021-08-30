using System;
using System.Text.RegularExpressions;

namespace ExcelReader {
    public class ExcelHelper {
        private const string Alphabetical = @"^[a-zA-Z]+";
        private const string Decimal = @"[0-9]+$";

        public static (int, int) CalculateCellDistance(string cell1, string cell2) {
            return CalculateCellDistance(ConvertCellNameToIndices(cell1), ConvertCellNameToIndices(cell2));
        }

        public static (int, int) CalculateCellDistance((int, int) cell1, (int, int) cell2) {
            return (Math.Abs(cell1.Item1 - cell2.Item1), Math.Abs(cell1.Item2 - cell2.Item2));
        }

        public static string ConvertCellIndicesToName(int column, int row) {
            return $"{ConvertColumnNumberToName(column)}{row}";
        }

        public static (int, int) ConvertCellNameToIndices(string name) {
            string columnString = Regex.Match(name, Alphabetical).Value;
            string rowString = Regex.Match(name, Decimal).Value;

            int column = ConvertColumnNameToNumber(columnString);
            if (!int.TryParse(rowString, out int row)) {
                throw new InvalidCastException("Row string can not be parsed as int!");
            }

            return (column, row);
        }

        public static int ConvertColumnNameToNumber(string columnName) {
            if (string.IsNullOrEmpty(columnName)) {
                throw new ArgumentNullException("Column name can not be empty!");
            }

            columnName = columnName.ToUpperInvariant();
            int sum = 0;

            for (int i = 0; i < columnName.Length; i++) {
                sum *= 26;
                sum += columnName[i] - 'A' + 1;
            }

            return sum;
        }

        public static string ConvertColumnNumberToName(int columnNumber) {
            int dividend = columnNumber;
            string columnName = string.Empty;
            int modulo;

            while (dividend > 0) {
                modulo = (dividend - 1) % 26;
                columnName = $"{Convert.ToChar(65 + modulo)}{columnName}";
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
    }
}