using powerplant_coding_challenge.Contracts;
using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Services
{
    public class ProductionPlan : IProductionPlan
    {
        public List<ProductionResult> CalulateProduction(Payload payload)
        {
            List<ProductionResult> productionResults = new List<ProductionResult>();
            var powerPlantsSortedByPrice = CalculateFuelCost(payload);

            double adjustment = payload.Load;

            foreach (var powerplant in powerPlantsSortedByPrice)
            {
                double production;

                if (powerplant.Type == "windturbine")
                {
                    production = payload.Fuels.Wind * powerplant.Pmax / 100.0;
                    adjustment -= production;
                }
                else
                {
                    if (adjustment > powerplant.Pmax)
                    {
                        production = powerplant.Pmax;
                        adjustment -= production;
                    }

                    else if (adjustment >= powerplant.Pmin)
                    {
                        production = adjustment;
                        adjustment -= production;
                    }

                    else
                        production = 0;
                }

                production = Math.Round(production, 1); // Round to the nearest 0.1 MW
                productionResults.Add(new ProductionResult { Name = powerplant.Name, P = production });
            }

            return productionResults;
        }

        private static List<Powerplant> CalculateFuelCost(Payload payload)
        {
            var costsList = new List<Powerplant>();

            costsList = payload.Powerplants.OrderBy(x =>
            {
                if (x.Type == "gasfired")
                    return (1 / x.Efficiency) * payload.Fuels.Gas;
                else if (x.Type == "turbojet")
                    return (1 / x.Efficiency) * payload.Fuels.Kerosine;
                else
                    return 0;
            }).ToList();

            return costsList;
        }
    }
}
