using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Lessons = new HashSet<Lesson>();
        }

        public string TagId { get; set; } = null!;

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
