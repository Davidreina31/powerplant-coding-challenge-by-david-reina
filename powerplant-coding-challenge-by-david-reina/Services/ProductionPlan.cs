using powerplant_coding_challenge.Contracts;
using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Services
{
    public class ProductionPlan : IProductionPlan
    {
        public IEnumerable<ProductionResult> CalculateProduction(Payload payload)
        {
            List<ProductionResult> productionResults = new List<ProductionResult>();
            var powerPlantsSortedByPrice = CalculateFuelCost(payload);

            double adjustment = payload.Load;

            for (int i = 0; i < powerPlantsSortedByPrice.Count; i++)
            {
                double production = CalculateProductionForPlant(payload, powerPlantsSortedByPrice, i, ref adjustment);
                productionResults.Add(new ProductionResult { Name = powerPlantsSortedByPrice[i].Name, P = production });
            }

            return productionResults;
        }

        private static double CalculateProductionForPlant(Payload payload, List<Powerplant> powerPlantsSortedByPrice, int index, ref double adjustment)
        {
            double production;

            if (powerPlantsSortedByPrice[index].Type == "windturbine")
            {
                production = payload.Fuels.Wind * powerPlantsSortedByPrice[index].Pmax / 100.0;

                if (adjustment >= production)
                {
                    HandleWindTurbineAdjustment(payload, powerPlantsSortedByPrice, ref adjustment, index, ref production);
                }
                else
                {
                    production = 0;
                }
            }
            else
            {
                production = 0;
                HandleNonWindTurbineAdjustment(payload, powerPlantsSortedByPrice, ref adjustment, index, ref production);
            }

            production = Math.Round(production, 1); // Round to the nearest 0.1 MW
            return production;
        }

        private static void HandleWindTurbineAdjustment(Payload payload, List<Powerplant> powerPlantsSortedByPrice, ref double adjustment, int index, ref double production)
        {
            if (index + 1 < powerPlantsSortedByPrice.Count)
            {
                if (adjustment - production >= powerPlantsSortedByPrice[index + 1].Pmin)
                    adjustment -= production;
                else
                {
                    adjustment = powerPlantsSortedByPrice[index + 1].Pmin;
                    production = payload.Load - adjustment >= powerPlantsSortedByPrice[index].Pmin ? payload.Load - adjustment : 0;
                }
            }
        }

        private static void HandleNonWindTurbineAdjustment(Payload payload, List<Powerplant> powerPlantsSortedByPrice, ref double adjustment, int index, ref double production)
        {
            if (adjustment > powerPlantsSortedByPrice[index].Pmax)
            {
                production = powerPlantsSortedByPrice[index].Pmax;

                if (index + 1 < powerPlantsSortedByPrice.Count)
                {
                    HandleWindTurbineAdjustment(payload, powerPlantsSortedByPrice, ref adjustment, index, ref production);
                }
            }
            else if (adjustment >= powerPlantsSortedByPrice[index].Pmin)
            {
                production = adjustment;
                adjustment -= production;
            }
            else
            {
                production = 0;
            }
        }

        private static List<Powerplant> CalculateFuelCost(Payload payload)
        {
            var windTurbine = CalculateWindTurbineCost(payload);

            var remaining = payload.Load - windTurbine;

            return payload.Powerplants.OrderBy(x =>
            {
                if (x.Type == "gasfired")
                    return x.Pmin <= remaining ? (1 / x.Efficiency) * payload.Fuels.Gas * remaining : 1 / (x.Efficiency) * payload.Fuels.Gas * x.Pmin;
                else if (x.Type == "turbojet")
                    return (1 / x.Efficiency) * payload.Fuels.Kerosine * remaining;
                else
                    return 0;
            }).ToList();
        }

        private static double CalculateWindTurbineCost(Payload payload)
        {
            return payload.Powerplants.Where(x => x.Type == "windturbine").Sum(item => payload.Fuels.Wind * item.Pmax / 100);
        }
    }
}
