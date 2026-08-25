using Application.DTOs.Elections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IElectionService
    {
        Task<IReadOnlyList<ElectionResponse>> GetAllAsync();
        Task<ElectionResponse> GetByIdAsync(Guid id);
        Task<ElectionResponse> CreateAsync(CreateElectionRequest request);
        Task<ElectionResponse?> UpdateAsync(Guid id, CreateElectionRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
