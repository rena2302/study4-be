using Microsoft.EntityFrameworkCore;
using study4_be.Models;

namespace study4_be.Repositories
{
    public class OrderRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();
        public async Task<IEnumerable<Order>> GetAllCoursesAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task DeleteAllCoursesAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();
        }
    }
}
