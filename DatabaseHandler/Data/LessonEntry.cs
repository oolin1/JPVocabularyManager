using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseHandler.Data {
    public class LessonEntry {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Phrase { get; set; }
        public string Reading { get; set; }
        public string Meaning { get; set; }
        public string Comment { get; set; }
    }
}