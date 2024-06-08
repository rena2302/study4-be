using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int? LessonId { get; set; }
        public string? QuestionText { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }

        public virtual Lesson? Lesson { get; set; }
    }
}
