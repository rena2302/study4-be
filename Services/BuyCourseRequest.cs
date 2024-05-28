namespace study4_be.Services
{
    public class BuyCourseRequest
    {
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? TotalAmount { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
