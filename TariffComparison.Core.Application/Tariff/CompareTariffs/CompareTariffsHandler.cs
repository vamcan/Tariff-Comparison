using MediatR;
using TariffComparison.Core.Application.Base.Common;
using TariffComparison.Core.Application.Services;
using TariffComparison.Core.Domain.Contracts.Repository;
using TariffComparison.Core.Domain.Dto;

namespace TariffComparison.Core.Application.Tariff.CompareTariffs
{
    public class CompareTariffsHandler : IRequestHandler<CompareTariffsRequest, OperationResult<List<TariffComparisonDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICompareTariffsService _compareTariffsService;

        public CompareTariffsHandler(IProductRepository productRepository, ICompareTariffsService compareTariffsService)
        {
            _productRepository = productRepository;
            _compareTariffsService = compareTariffsService;
        }

        public async Task<OperationResult<List<TariffComparisonDto>>> Handle(CompareTariffsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                if (!products.Any())
                {
                    return OperationResult<List<TariffComparisonDto>>.NotFoundResult("Products not found!");
                }
                var result = _compareTariffsService.CompareTariffs(products, request.Consumption).ToList();
                return OperationResult<List<TariffComparisonDto>>.SuccessResult(result);
            }
            catch (Exception e)
            {
                return OperationResult<List<TariffComparisonDto>>.FailureResult(e.Message);
            }
        }
    }
}
