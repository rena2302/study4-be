using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Vocabulary
    {
        public int VocabId { get; set; }
        public int? LessonsId { get; set; }
        public string? Type { get; set; }
        public string? Vocab { get; set; }
        public string? Mean { get; set; }
        public string? Example { get; set; }
        public string? Explanation { get; set; }
        public string? AudioUrl { get; set; }

        public virtual Lesson? Lessons { get; set; }
    }
}
