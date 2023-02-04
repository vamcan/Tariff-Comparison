using MediatR;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Core.Application.Tariff.CompareTariffs;
using TariffComparison.Core.Domain.Dto;

namespace TariffComparison.Web.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;


        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCompareTariffs")]
        [ProducesResponseType(typeof(List<TariffComparisonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompareTariffs(decimal consumption)
        {
            var request = new CompareTariffsRequest() { Consumption = consumption };
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
