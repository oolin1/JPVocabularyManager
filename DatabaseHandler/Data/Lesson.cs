using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseHandler.Data {
    public class Lesson {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<LessonEntry> Entries { get; set; }
    }
}