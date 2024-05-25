using System;
using System.Collections.Generic;

namespace study4_be.Models
{
    public partial class User
    {
        public User()
        {
            Ratings = new HashSet<Rating>();
            UserCourses = new HashSet<UserCourse>();
        }

        public string UsersId { get; set; } = null!;
        public string? UsersName { get; set; }
        public string? UsersEmail { get; set; }
        public string? UsersPassword { get; set; }
        public string? UsersDescription { get; set; }
        public string? UsersImage { get; set; }
        public string? UsersBanner { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}
