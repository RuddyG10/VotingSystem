using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly VotingDbContext _context;

        public CandidateRepository(VotingDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Candidate>> GetByElectionIdAsync(Guid electionId)
        {
            return await _context.Candidates
                .AsNoTracking()
                .Where(c => c.ElectionId == electionId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Candidate?> GetByIdAsync(Guid id)
        {
            return await _context.Candidates
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
