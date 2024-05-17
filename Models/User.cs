using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class User
    {
        public User()
        {
            Answers = new HashSet<Answer>();
            Ratings = new HashSet<Rating>();
        }

        public string UsersId { get; set; } = null!;
        public string? UsersName { get; set; }
        public string? Email { get; set; }
        public string? UsersPassword { get; set; }
        public string? UsersDescription { get; set; }
        public string? UsersImage { get; set; }
        public string? UsersBanner { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
