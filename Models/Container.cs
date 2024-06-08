using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Container
    {
        public Container()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int ContainerId { get; set; }
        public string? ContainerTitle { get; set; }
        public int? UnitId { get; set; }

        public virtual Unit? Unit { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
