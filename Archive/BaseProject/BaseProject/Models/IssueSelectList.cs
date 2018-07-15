using BaseProject.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BaseProject.Models
{
    public class IssueSelectList : SelectList
    {
        public IssueSelectList(IEnumerable<Issue> items, int selected) : base(items, nameof(Issue.Id), nameof(Issue.Description), selected)
        {

        }
        public IssueSelectList(IEnumerable<Issue> items) : base(items, nameof(Issue.Id), nameof(Issue.Description))
        {

        }
    }
}
