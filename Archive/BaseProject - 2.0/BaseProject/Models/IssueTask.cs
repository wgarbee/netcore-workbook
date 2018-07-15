using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Models
{
    public class IssueTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

        // Foreign constraint properties
        public int? AssigneeId { get; set; }
        public int IssueId { get; set; }

        // Navigation Properties
        public User Assignee { get; set; }
        public Issue Issue { get; set; }
    }
}
