using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Candidates
{
    public record CandidateResponse
    (
        Guid Id,
        string Name,
        string? Description,
        Guid ElectionId
    );
}
