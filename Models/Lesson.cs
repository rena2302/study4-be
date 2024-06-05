using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace study4_be.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            Audios = new HashSet<Audio>();
            Quizzes = new HashSet<Quiz>();
            Translates = new HashSet<Translate>();
            Vocabularies = new HashSet<Vocabulary>();
        }

        public int LessonId { get; set; }
        public string? LessonType { get; set; }
        public string? LessonTitle { get; set; }
        public int? ContainerId { get; set; }

        public virtual Container? Container { get; set; }
        public virtual ICollection<Audio> Audios { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Translate> Translates { get; set; }
        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
