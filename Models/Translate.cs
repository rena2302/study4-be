using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Translate
    {
        public int TranslateId { get; set; }
        public string? Hint { get; set; }
        public string? Text { get; set; }
        public string? Answer { get; set; }
        public int? LessonId { get; set; }

        public virtual Lesson? Lesson { get; set; }
    }
}
