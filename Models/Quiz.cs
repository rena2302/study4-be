using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<Question>();
        }

        public int QuizzesId { get; set; }
        public string? Title { get; set; }
        public string? DescriptionQuizzes { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ContainerId { get; set; }

        public virtual Container? Container { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
