using study4_be.Models;

namespace study4_be.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();

    }
}
