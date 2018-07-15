using IssueTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.DataAccess.Contracts
{
    internal class Issue
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public decimal Estimate { get; set; }
        public string Description { get; set; }
        public IssueType Type { get; set; }
    }
}
