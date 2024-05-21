using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Lesson
    {
        public int LessonsId { get; set; }
        public int? CoursesId { get; set; }
        public string? LessonsTitle { get; set; }
        public string? Content { get; set; }
    }
}
