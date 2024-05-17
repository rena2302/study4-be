using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public string? UsersId { get; set; }
        public int? CoursesId { get; set; }
        public DateTime? RatingDate { get; set; }
        public short? RatingValue { get; set; }
        public string? Review { get; set; }

        public virtual Course? Courses { get; set; }
        public virtual User? Users { get; set; }
    }
}
