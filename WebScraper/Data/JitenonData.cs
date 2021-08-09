using System.Collections.Generic;

namespace WebScraper.Data {
    public class JitenonData {
        public List<string> Parts { get; private set; }

        public JitenonData(List<string> parts) {
            Parts = parts;
        }
    }
}