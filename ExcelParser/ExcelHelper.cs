using System;
using Converter = ExcelParser.ExcelCellNameConverter;

namespace ExcelParser {
    public class ExcelHelper {
        public static (int, int) CalculateCellDistance(string cell1, string cell2) {
            return CalculateCellDistance(Converter.ExcelCellNameToIndices(cell1), Converter.ExcelCellNameToIndices(cell2));
        }

        public static (int, int) CalculateCellDistance((int, int) cell1, (int, int) cell2) {
            return (Math.Abs(cell1.Item1 - cell2.Item1), Math.Abs(cell1.Item2 - cell2.Item2));
        }
    }
}