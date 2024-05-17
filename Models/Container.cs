using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Container
    {
        public int ContainerId { get; set; }
        public int? FlashCardId { get; set; }
        public int? TrainerId { get; set; }
        public int? VideoId { get; set; }
        public int? VocabId { get; set; }
        public int? GrammarId { get; set; }
        public int? LessonId { get; set; }

        public virtual Lesson? Lesson { get; set; }
    }
}
