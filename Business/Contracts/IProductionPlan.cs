using Domain;

namespace Business.Contracts
{
    public interface IProductionPlan
    {
        IEnumerable<ProductionResult> CalculateProduction(Payload payload);
    }
}
