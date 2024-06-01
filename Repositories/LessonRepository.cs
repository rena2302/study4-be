using Microsoft.EntityFrameworkCore;
using study4_be.Models;

namespace study4_be.Repositories
{
    public class LessonRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();
        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            return await _context.Lessons.ToListAsync();
        }
        public async Task DeleteAllLessonsAsync()
        {
            var lessons = await _context.Lessons.ToListAsync();
            _context.Lessons.RemoveRange(lessons);
            await _context.SaveChangesAsync();
        }
    }
}
