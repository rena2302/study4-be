using Microsoft.EntityFrameworkCore;
using study4_be.Models;

namespace study4_be.Repositories
{
    public class QuizzRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();
        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }
        public async Task DeleteAllQuizzesAsync()
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            _context.Quizzes.RemoveRange(quizzes);
            await _context.SaveChangesAsync();
        }
    }
}
