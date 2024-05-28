using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? TotalAmount { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool? State { get; set; }

        public virtual Course? Course { get; set; }
    }
}
