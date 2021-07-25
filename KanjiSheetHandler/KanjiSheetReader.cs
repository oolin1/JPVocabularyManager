using ExcelParser;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KanjiSheetHandler {
    public class KanjiSheetReader {
        private readonly string filePath;
        private readonly string sheetName;

        public KanjiSheetReader(string filePath, string sheetName) {
            this.filePath = filePath;
            this.sheetName = sheetName;
        }


        public List<string> ReadKanjisFromRange(string from, string to, int kanjiColumnsDistance = 5) {
            (int, int) fromCell = ExcelCellNameConverter.ExcelCellNameToIndices(from);
            (int, int) toCell = ExcelCellNameConverter.ExcelCellNameToIndices(to);

            List<string> columns = GetColumnsToRead(fromCell.Item1, toCell.Item1, kanjiColumnsDistance);
            List<string> kanjis = new List<string>();
            using (ExcelReader excelReader = new ExcelReader(Path.GetFullPath(filePath), sheetName)) {
                foreach (string column in columns) {
                    kanjis.AddRange(RemoveSpecialValues(excelReader.GetRangeValues($"{column}{fromCell.Item2}", $"{column}{toCell.Item2}")));
                }
            }

            return kanjis;
        }

        private List<string> GetColumnsToRead(int firstColumn, int lastColumn, int kanjiColumnsDistance) {
            List<string> columns = new List<string>();
            for (int i = firstColumn; i <= lastColumn; i = i + kanjiColumnsDistance) {
                columns.Add(ExcelCellNameConverter.ExcelColumnNumberToName(i));
            }

            return columns;
        }

        private IEnumerable<string> RemoveSpecialValues(List<string> list) {
            for (int i = 0; i<list.Count; i++) {
                if (list[i].Length > 1 && list[i].Contains(",")) {
                    list[i] = list[i].Split(",")[0];
                }
            }

            return list.Where(kanji => !kanji.Contains("+") && !kanji.Contains("-"));
        }
    }
}