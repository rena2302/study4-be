namespace study4_be.Services.Request
{
    public class GetUserCoursesRequest
    {
        public string userId { get; set; } = string.Empty;
        public int amountOutstanding {  get; set; } = 0; // Default value set to 0
    }
}
