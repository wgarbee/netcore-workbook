using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Domain.Internal.Contracts
{
    public interface IIssueRepository
    {
        Task<List<Domain.Contracts.IIssue>> GetIssues();
        Task<Domain.Contracts.IIssue> GetIssue(int id);
        Task<Domain.Contracts.IIssue> SaveIssue(Domain.Contracts.IIssue issue);
        Task<int> NextKeyGuess();
    }
}
