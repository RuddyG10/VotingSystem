using Application.DTOs.Elections;
using Application.Interfaces;
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
        private readonly IElectionService _electionService;
        
        public ElectionsController(IElectionService electionService)
        {
            _electionService = electionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectionResponse>>> GetAll()
        {
            var elections = await _electionService.GetAllAsync();
            return Ok(elections);
        }

        [HttpPost]
        public async Task<ActionResult<ElectionResponse>> Create(CreateElectionRequest request)
        {
            try
            {
                var election = await _electionService.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = election.Id },
                    election
                );

            }
            catch (ArgumentException ex)
            {
                // Log the exception (ex) here if needed
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ElectionResponse>> GetById(Guid id)
        {
            var election = await _electionService.GetByIdAsync(id);

            if (election is null)
            {
                return NotFound();
            }

            var response = new ElectionResponse
            (
                election.Id,
                election.Name,
                election.Description,
                election.StartDate,
                election.EndDate,
                election.IsActive
            );

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ElectionResponse>> update(
            Guid id,
            UpdateElectionRequest request)
        {
            try
            {
                var election = await _electionService.UpdateAsync(
                    id,
                    request);

                if (election is null)
                {
                    return NotFound();
                }

                return Ok(election);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _electionService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
       
    }
}
