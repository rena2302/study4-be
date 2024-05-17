using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int QuestionId { get; set; }
        public int? IdQuizzes { get; set; }
        public string? QuestionText { get; set; }
        public string? CorrectAnswer { get; set; }

        public virtual Quiz? IdQuizzesNavigation { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
