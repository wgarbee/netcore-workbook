using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; internal set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        // Navigation Properties
        public ICollection<Issue> Issues { get; set; }
    }
}
