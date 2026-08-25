using Application.DTOs.Candidates;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("elections/{electionId:guid}/candidates")]
        public async Task<ActionResult<IEnumerable<CandidateResponse>>> GetByElectionId(Guid electionId)
        {
            var candidates = await _candidateService.GetByElectionIdAsync(electionId);
            return Ok(candidates);
        }
        [HttpGet("candidates/{id:guid}")]
        public async Task<ActionResult<CandidateResponse>> GetById(Guid id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);
            if (candidate is null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        [HttpPost("elections/{electionId:guid}/candidates")]
        public async Task<ActionResult<CandidateResponse>> Create(Guid electionId, CreateCandidateRequest request)
        {
            try
            {
                var candidate = await _candidateService.CreateAsync(electionId, request);

                if (candidate is null)
                {
                    return NotFound();
                }

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = candidate.Id },
                    candidate
                );
            }
            catch (ArgumentException ex)
            {
                // Log the exception (ex) here if needed
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("candidates/{id:guid}")]
        public async Task<ActionResult<CandidateResponse>> Update(Guid id, UpdateCandidateRequest request)
        {
            try
            {
                var candidate = await _candidateService.UpdateAsync(id, request);
                if (candidate is null)
                {
                    return NotFound();
                }
                return Ok(candidate);
            }
            catch (ArgumentException ex)
            {
                // Log the exception (ex) here if needed
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _candidateService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
