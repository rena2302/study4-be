using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string? UsersId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Address { get; set; }

        public virtual Course? Course { get; set; }
    }
}
