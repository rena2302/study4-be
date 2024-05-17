using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string? UsersId { get; set; }
        public int? QuizzesId { get; set; }
        public string? Answer1 { get; set; }

        public virtual Question? Question { get; set; }
        public virtual Quiz? Quizzes { get; set; }
        public virtual User? Users { get; set; }
    }
}
