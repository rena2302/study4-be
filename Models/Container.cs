using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace study4_be.Models
{
    public partial class Container
    {
        public Container()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int ContainerId { get; set; }
        public int? UnitId { get; set; }

        [NotMapped]
        public List<Container>? ListContainer { get; set; }
        public virtual Unit? Unit { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
