using Microsoft.EntityFrameworkCore;
using study4_be.Models;

namespace study4_be.Repositories
{
    public class VocabRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();
        public async Task<IEnumerable<Vocabulary>> GetAllVocabAsync()
        {
            return await _context.Vocabularies.ToListAsync();
        }
        public async Task DeleteAllVocabAsync()
        {
            var vocab = await _context.Vocabularies.ToListAsync();
            _context.Vocabularies.RemoveRange(vocab);
            await _context.SaveChangesAsync();
        }
    }
}
