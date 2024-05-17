using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            Audios = new HashSet<Audio>();
            Containers = new HashSet<Container>();
            Quizzes = new HashSet<Quiz>();
            Vocabularies = new HashSet<Vocabulary>();
        }

        public int LessonsId { get; set; }
        public int? CoursesId { get; set; }
        public string? LessonsTitle { get; set; }
        public string? Content { get; set; }

        public virtual Course? Courses { get; set; }
        public virtual ICollection<Audio> Audios { get; set; }
        public virtual ICollection<Container> Containers { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
