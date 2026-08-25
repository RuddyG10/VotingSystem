using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Candidates
{
    public record CreateCandidateRequest
    (
        string Name,
        string? Description
    );
}
