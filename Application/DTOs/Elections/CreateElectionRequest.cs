using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Elections
{
    public record CreateElectionRequest
    (
        string Name,
        string? Description,
        DateTime StartDate,
        DateTime EndDate
    );
}
