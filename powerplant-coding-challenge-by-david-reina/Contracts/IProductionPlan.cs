using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Contracts
{
    public interface IProductionPlan
    {
        List<ProductionResult> CalulateProduction(Payload payload);
    }
}
