using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Audio
    {
        public int AudioId { get; set; }
        public string? AudioUrl { get; set; }
        public string? AudioDescription { get; set; }
        public int? LessonId { get; set; }

        public virtual Lesson? Lesson { get; set; }
    }
}
