using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Nexus.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
 	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[EnableRateLimiting("fixed")]
	public class BaseController : ControllerBase
	{
      
    }
}
