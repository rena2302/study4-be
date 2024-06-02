using Microsoft.EntityFrameworkCore;
using study4_be.Models;

namespace study4_be.Repositories
{
    public class TranslateRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();
        public async Task<IEnumerable<Translate>> GetAllTranslateAsync()
        {
            return await _context.Translates.ToListAsync();
        }
        public async Task DeleteAllTranslatesAsync()
        {
            var translates = await _context.Translates.ToListAsync();
            _context.Translates.RemoveRange(translates);
            await _context.SaveChangesAsync();
        }
    }
}
