namespace study4_be.Services
{
    public class BuyCourseRequest
    {
        public int OrderId { get; set; }
        public string? UsersId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Address { get; set; }
    }
}
