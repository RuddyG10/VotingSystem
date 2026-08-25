using Application.DTOs.Elections;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ElectionService : IElectionService
    {

        private readonly IElectionRepository _electionRepository;

        public ElectionService(IElectionRepository electionRepository)
        {
            _electionRepository = electionRepository;
        }
        public async Task<ElectionResponse> CreateAsync(CreateElectionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name)) { 
                throw new ArgumentException("Election name cannot be empty.");
            }

            if(request.EndDate <= request.StartDate)
            {
                throw new ArgumentException("End date must be after start date.");
            }

            var election = new Election
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = true
            };

            await _electionRepository.AddAsync(election);

            return new ElectionResponse(
                election.Id,
                election.Name,
                election.Description,
                election.StartDate,
                election.EndDate,
                election.IsActive
            );
        }

        public async Task<IReadOnlyList<ElectionResponse>> GetAllAsync()
        {
            var elections = await _electionRepository.GetAllAsync();

            return elections
                .Select(e => new ElectionResponse(
                    e.Id,
                    e.Name,
                    e.Description,
                    e.StartDate,
                    e.EndDate,
                    e.IsActive
                ))
                .ToList();
        }

        public async Task<ElectionResponse> GetByIdAsync(Guid id)
        {
            var election = await _electionRepository.GetByIdAsync(id);

            if (election is null)
            {
                throw new KeyNotFoundException($"Election with ID {id} not found.");
            }

            return new ElectionResponse(
                election.Id,
                election.Name,
                election.Description,
                election.StartDate,
                election.EndDate,
                election.IsActive
            );
        }
    }
}
