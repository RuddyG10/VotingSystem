using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IElectionRepository
    {   
        Task<IReadOnlyList<Election>> GetAllAsync();
        Task<Election?> GetByIdAsync(Guid id);
        Task AddAsync(Election election);

        Task UpdateAsync(Election election);
        Task DeleteAsync(Guid id);
    }
}
