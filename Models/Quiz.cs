using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Answers = new HashSet<Answer>();
            Questions = new HashSet<Question>();
        }

        public int QuizzesId { get; set; }
        public int? LessonsId { get; set; }
        public string? Title { get; set; }
        public string? DescriptionQuizzes { get; set; }
        public DateTime? CreatedTime { get; set; }

        public virtual Lesson? Lessons { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
