using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseHandler.Data {
    public class Kanji {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; /*?*/ }
        public int HeisingID { get; set; }
        public string HeisingMeaning { get; set; }
        [Required]
        public string Symbol { get; set; }
        public ICollection<Word> Meanings { get; set; }
        public ICollection<Word> KunReadings { get; set; }
        public ICollection<Word> OnReadings { get; set; }
        public ICollection<Word> Parts { get; set; }
    }
}