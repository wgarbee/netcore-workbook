using BaseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Data
{
    public static class DBInitializer
    {
        public static void Initialize(IssueTrackerContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Users.Any())
            {
                return;
            }

            var users = new []
            {
                new AppUser{ FirstName = "System", LastName="Administrator", Username = "admin"}
            };

            context.Users.AddRange(users);

            context.SaveChanges();

            var issues = new[]
            {
                new Issue { Description = "First unassigned Issue", Status = Status.Defined },
                new Issue { Description = "First assigned Issue", Status = Status.Accepted, AssigneeId = users.First().Id}
            };

            context.Issues.AddRange(issues);

            context.SaveChanges();

            var tasks = new[]
            {
                new IssueTask { Description = "First unassigned issue task 1", Status = Status.Defined, IssueId = issues.First().Id},
                new IssueTask { Description = "First assigned issue task 1", Status = Status.Defined, IssueId = issues.First().Id, AssigneeId = users.First().Id}
            };

            context.IssueTasks.AddRange(tasks);

            context.SaveChanges();
        }
    }
}
