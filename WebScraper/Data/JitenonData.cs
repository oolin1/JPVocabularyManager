using System.Collections.Generic;

namespace WebScraper.Data {
    public class JitenonData {
        public List<string> Radicals { get; private set; }

        public JitenonData(List<string> radicals) {
            Radicals = radicals;
        }
    }
}