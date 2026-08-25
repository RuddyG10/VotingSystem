using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    
    public class ElectionRepository : IElectionRepository
    {
        private readonly VotingDbContext _context;

        public ElectionRepository(VotingDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Election election)
        {
            _context.Elections.Add(election);

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Election>> GetAllAsync()
        {
            return await _context.Elections
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Election?> GetByIdAsync(Guid id)
        {
            return await _context.Elections
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
