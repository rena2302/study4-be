using study4_be.Models;

namespace study4_be.Services
{
    public class UnitDetailResponse
    {
        public int unitId { get; set; }
        public string unitName { get; set; } = string.Empty;
        public IEnumerable<Container> Containers { get; set; }
    }
}
