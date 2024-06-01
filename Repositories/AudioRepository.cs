using Microsoft.EntityFrameworkCore;
using study4_be.Models;

namespace study4_be.Repositories
{
    public class AudioRepository
    {
        private readonly STUDY4Context _context = new STUDY4Context();
        public async Task<IEnumerable<Audio>> GetAllAudiosAsync()
        {
            return await _context.Audios.ToListAsync();
        }
        public async Task DeleteAllAudiosAsync()
        {
            var audios = await _context.Audios.ToListAsync();
            _context.Audios.RemoveRange(audios);
            await _context.SaveChangesAsync();
        }
    }
}
