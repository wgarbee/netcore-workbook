using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Description("First Name")]
        public string FirstName { get; set; }

        [Description("Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                    return $"{LastName}, {FirstName}";
                if (string.IsNullOrWhiteSpace(LastName))
                    return FirstName ?? null;
                return LastName;
            }
        }

        // Navigation Properties
        public ICollection<Issue> Issues { get; set; }
    }
}
