using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IReadOnlyList<Candidate>> GetByElectionIdAsync(Guid electionId);
        Task<Candidate?> GetByIdAsync(Guid id);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task DeleteAsync(Candidate candidate);
    }
}
