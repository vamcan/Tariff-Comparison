namespace TariffComparison.Core.Domain.Base
{
    public interface IProduct
    {
        public string Name { get; }
        decimal GetAnnualCost(decimal consumption);
    }
}
