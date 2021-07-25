using System;
using System.Text.RegularExpressions;

namespace ExcelParser {
    public class ExcelCellNameConverter {
        private const string Alphabetical = @"^[a-zA-Z]+";
        private const string Decimal = @"[0-9]+$";

        public static string ExcelCellIndicesToName(int column, int row) {
            return $"{ExcelColumnNumberToName(column)}{row}";
        }

        public static (int, int) ExcelCellNameToIndices(string name) {
            string columnString = Regex.Match(name, Alphabetical).Value;
            string rowString = Regex.Match(name, Decimal).Value;

            int column = ExcelColumnNameToNumber(columnString);
            if (!int.TryParse(rowString, out int row)) {
                throw new InvalidCastException("Row string can not be parsed as int!");
            }

            return (column, row);
        }

        public static int ExcelColumnNameToNumber(string columnName) {
            if (string.IsNullOrEmpty(columnName)) {
                throw new ArgumentNullException("Column name can not be empty!");
            }

            columnName = columnName.ToUpperInvariant();
            int sum = 0;

            for (int i = 0; i < columnName.Length; i++) {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }

        public static string ExcelColumnNumberToName(int columnNumber) {
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