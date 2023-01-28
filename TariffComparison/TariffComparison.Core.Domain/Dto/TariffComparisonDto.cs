namespace TariffComparison.Core.Domain.Dto
{
    public record TariffComparisonDto
    {
        public string TariffName { get; set; }
        public decimal AnnnualCost { get; set; }
    }
}
