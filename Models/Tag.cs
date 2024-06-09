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

        public int TagId { get; set; }
        public string? TagTitle { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
