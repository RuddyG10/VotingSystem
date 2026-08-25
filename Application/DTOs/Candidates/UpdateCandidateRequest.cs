using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Candidates
{
    public record UpdateCandidateRequest
    (
        string Name,
        string? Description
    );
}
