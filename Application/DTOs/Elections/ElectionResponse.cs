using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Elections
{
    public record ElectionResponse
    (
        Guid Id,
        string Name,
        string? Description,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive
    );
}
