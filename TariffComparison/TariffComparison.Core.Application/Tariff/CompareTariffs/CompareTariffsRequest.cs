using MediatR;
using TariffComparison.Core.Application.Base.Common;
using TariffComparison.Core.Domain.Dto;

namespace TariffComparison.Core.Application.Tariff.CompareTariffs
{
    public record CompareTariffsRequest: IRequest<OperationResult<TariffComparisonDto>>, IRequest<OperationResult<List<TariffComparisonDto>>>
    {
        public  decimal Consumption { get; set; }
    }
}
