using Application.DTOs.Candidates;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IElectionRepository _electionRepository;

        public CandidateService(ICandidateRepository candidateRepository, IElectionRepository electionRepository)
        {
            _candidateRepository = candidateRepository;
            _electionRepository = electionRepository;
        }

        public async Task<CandidateResponse?> CreateAsync(Guid electionId, CreateCandidateRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Candidate name cannot be null or empty.", nameof(request.Name));
            }

            var election = await _electionRepository.GetByIdAsync(electionId);

            if(election is null)
            {
                throw new KeyNotFoundException($"Election with ID {electionId} not found.");
            }

            var candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ElectionId = electionId
            };

            await _candidateRepository.AddAsync(candidate);

            return new CandidateResponse
            (
                candidate.Id,
                candidate.Name,
                candidate.Description,
                candidate.ElectionId
            );
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);

            if(candidate is null)
            {
                return false;
            }
            await _candidateRepository.DeleteAsync(candidate);
            return true;
        }

        public async Task<IReadOnlyList<CandidateResponse>> GetByElectionIdAsync(Guid electionId)
        {
            var election = await _electionRepository.GetByIdAsync(electionId);

            if(election is null)
            {
                return Array.Empty<CandidateResponse>();
            }

            var candidates = await _candidateRepository.GetByElectionIdAsync(electionId);

            return candidates.Select(c => new CandidateResponse
            (
                c.Id,
                c.Name,
                c.Description,
                c.ElectionId
            )).ToList();
        }

        public async Task<CandidateResponse?> GetByIdAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);

            if (candidate is null)
            {
                return null;
            }

            return new CandidateResponse
            (
                candidate.Id,
                candidate.Name,
                candidate.Description,
                candidate.ElectionId
            );
        }

        public async Task<CandidateResponse?> UpdateAsync(Guid id, UpdateCandidateRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Candidate name cannot be null or empty.", nameof(request.Name));
            }

            var candidate = await _candidateRepository.GetByIdAsync(id);

            if (candidate is null)
            {
                return null;
            }

            candidate.Name = request.Name;
            candidate.Description = request.Description;

            await _candidateRepository.UpdateAsync(candidate);

            return new CandidateResponse
            (
                candidate.Id,
                candidate.Name,
                candidate.Description,
                candidate.ElectionId
            );
        }
    }
}
