using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Vocabulary
    {
        public int VocabId { get; set; }
        public string? VocabType { get; set; }
        public string? Mean { get; set; }
        public string? Example { get; set; }
        public string? Explanation { get; set; }
        public string? AudioUrlUs { get; set; }
        public string? AudioUrlUk { get; set; }
        public int? ContainerId { get; set; }

        public virtual Container? Container { get; set; }
    }
}
