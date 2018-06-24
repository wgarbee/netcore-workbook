using BaseProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Data.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [UIHint("textarea")]
        public string Description { get; set; }
        public Status Status { get; set; }

        // Foreign constraint properties
        public int? AssigneeId { get; set; }

        // Navigation Properties
        public User Assignee { get; set; }
        public ICollection<IssueTask> Tasks { get; set; }
    }
}
