using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Repositories;
using study4_be.Services;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order_APIController : Controller
    {
        private readonly ILogger<Order_APIController> _logger;
        public OrderRepository ordRepo = new OrderRepository();
        public Order_APIController(ILogger<Order_APIController> logger)
        {
            _logger = logger;
        }
        public STUDY4Context _context = new STUDY4Context();
        public IActionResult Index()
        {
            return View();
        }
        //development enviroment
        [HttpDelete("Delete_AllOrders")]
        public async Task<IActionResult> Delete_AllOrders()
        {
            await ordRepo.DeleteAllCoursesAsync();
            return Json(new { status = 200, message = "Delete All Orders Successful" });
        }
        [HttpPost("Buy_Course")] // thieu number phone va tru thoi gian
        public async Task<IActionResult> Buy_Course([FromBody] BuyCourseRequest request)
        {
            if (request == null || request.UsersId == null || request.CourseId == null)
            {
                return BadRequest("Invalid user or course information.");
            }

            var existingUser = await _context.Users.FindAsync(request.UsersId);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }
            var existingOrder = await _context.Orders
               .FirstOrDefaultAsync(o => o.UserId == request.UsersId && o.CourseId == request.CourseId);

            if (existingOrder != null)
            {
                return BadRequest("You have already ordered this course.");
            }

            var existingCourse = await _context.Courses.FindAsync(request.CourseId);
            if (existingCourse == null)
            {
                return NotFound("Course not found.");
            }
            var order = new Order
            {
                UserId = existingUser.UserId,
                CourseId = existingCourse.CourseId,
                OrderDate = DateTime.Now
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok("Course purchased successfully.");
        }
    }
}
