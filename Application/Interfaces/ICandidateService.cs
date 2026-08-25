using Application.DTOs.Candidates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICandidateService
    {
        Task<IReadOnlyList<CandidateResponse>> GetByElectionIdAsync(Guid electionId);
        Task<CandidateResponse?> GetByIdAsync(Guid id);
        Task<CandidateResponse?> CreateAsync(Guid id,CreateCandidateRequest request);
        Task<CandidateResponse?> UpdateAsync(Guid id, UpdateCandidateRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
