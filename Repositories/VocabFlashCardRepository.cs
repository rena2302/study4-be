using Microsoft.EntityFrameworkCore;
using study4_be.Models;

namespace study4_be.Repositories
{
    public class VocabFlashCardRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();
        public async Task<IEnumerable<Vocabulary>> GetAllVocabDependLesson(int idLesson)
        {
            return await _context.Vocabularies.Where(v => v.LessonId == idLesson).ToListAsync();
        }

    }
}
