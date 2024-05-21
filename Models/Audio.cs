using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class Audio
    {
        public int AudioId { get; set; }
        public string? AudioUrl { get; set; }
        public string? AudioDescription { get; set; }
        public int? ContainerId { get; set; }

        public virtual Container? Container { get; set; }
    }
}
