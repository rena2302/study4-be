using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Container
    {
        public Container()
        {
            Audios = new HashSet<Audio>();
            Quizzes = new HashSet<Quiz>();
            Translates = new HashSet<Translate>();
            Vocabularies = new HashSet<Vocabulary>();
        }

        public int ContainerId { get; set; }
        public int? CoursesId { get; set; }

        public virtual Course? Courses { get; set; }
        public virtual ICollection<Audio> Audios { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Translate> Translates { get; set; }
        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
