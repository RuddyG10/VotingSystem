using Application.DTOs.Elections;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/elections")]
    public class ElectionsController : ControllerBase
    {
        private readonly VotingDbContext _context;
        
        public ElectionsController(VotingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectionResponse>>> GetAll()
        {
            var elections = await _context.Elections
                .Select(e => new ElectionResponse
                (
                    e.Id,
                   e.Name,
                    e.Description,
                    e.StartDate,
                    e.EndDate,
                   e.IsActive
                )).ToListAsync();

            return Ok(elections);
        }

        [HttpPost]
        public async Task<ActionResult<ElectionResponse>> Create(CreateElectionRequest request)
        {
            var election  = new Election
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = true
            };

            _context.Elections.Add(election);

            await _context.SaveChangesAsync();

            var response = new ElectionResponse
            (
                election.Id,
                election.Name,
                election.Description,
                election.StartDate,
                election.EndDate,
                election.IsActive
            );

            return CreatedAtAction(

                nameof(GetAll),
                new { id = election.Id },
                response
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ElectionResponse>> GetById(Guid id)
        {
            var election = await _context.Elections
                .Where(e => e.Id == id)
                .Select(e => new ElectionResponse
                (
                    e.Id,
                    e.Name,
                    e.Description,
                    e.StartDate,
                    e.EndDate,
                    e.IsActive
                )).FirstOrDefaultAsync();

            if (election is null)
            {
                return NotFound();
            }
            return Ok(election);
        }

       
    }
}
