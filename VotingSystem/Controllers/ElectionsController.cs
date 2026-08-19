using Application.DTOs.Elections;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/elections")]
    public class ElectionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ElectionResponse>> GetAll() {
            var elections = new List<ElectionResponse>
            {
                new(
                    Guid.NewGuid(),
                    "Electiones Generales",
                    "Primera eleccion",
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(7),
                    true
                )
            };

            return Ok(elections);

        }
    }
}
