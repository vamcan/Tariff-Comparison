using MediatR;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Core.Application.Tariff.CompareTariffs;
using TariffComparison.Web.Api.Base;

namespace TariffComparison.Web.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;


        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCompareTariffs")]

        public async Task<IActionResult> GetCompareTariffs(decimal consumption)
        {
            var request = new CompareTariffsRequest() { Consumption = consumption };
            var query = await _mediator.Send(request);

            return base.OperationResult(query);
        }
    }
}
