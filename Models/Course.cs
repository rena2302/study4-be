using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Course
    {
        public Course()
        {
            Containers = new HashSet<Container>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
        }

        public int CoursesId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public string? CourseImage { get; set; }
        public string? CourseTag { get; set; }
        public double? CoursePrice { get; set; }

        public virtual ICollection<Container> Containers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
