using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseHandler.Data {
    public class Kanji {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int HeisingID { get; set; }
        public string HeisingMeaning { get; set; }
        [Required]
        public string Symbol { get; set; }
        public ICollection<Meaning> Meanings { get; set; }
        public ICollection<KunReading> KunReadings { get; set; }
        public ICollection<OnReading> OnReadings { get; set; }
        public ICollection<KanjiPart> Parts { get; set; }
    }
}