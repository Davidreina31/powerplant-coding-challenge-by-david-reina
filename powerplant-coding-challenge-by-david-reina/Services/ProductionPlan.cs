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

            for (int i = 0; i < powerPlantsSortedByPrice.Count; i++)
            {
                double production;

                if (powerPlantsSortedByPrice[i].Type == "windturbine")
                {
                    if (adjustment > powerPlantsSortedByPrice[i].Pmax)
                    {
                        production = payload.Fuels.Wind * powerPlantsSortedByPrice[i].Pmax / 100.0;

                        if (i + 1 < powerPlantsSortedByPrice.Count)
                        {
                            if (adjustment - production >= powerPlantsSortedByPrice[i + 1].Pmin)
                                adjustment -= production;

                            else
                            {
                                adjustment = powerPlantsSortedByPrice[i + 1].Pmin;
                                production = payload.Load - adjustment >= powerPlantsSortedByPrice[i].Pmin ? payload.Load - adjustment : 0;
                            }
                        }
                    }

                    else if (adjustment >= powerPlantsSortedByPrice[i].Pmin)
                    {
                        production = adjustment;
                        adjustment -= production;
                    }

                    else
                        production = 0;
                }
                else
                {
                    if (adjustment > powerPlantsSortedByPrice[i].Pmax)
                    {
                        production = powerPlantsSortedByPrice[i].Pmax;

                        if (i + 1 < powerPlantsSortedByPrice.Count)
                        {
                            if (adjustment - production >= powerPlantsSortedByPrice[i + 1].Pmin)
                                adjustment -= production;

                            else
                            {
                                adjustment = powerPlantsSortedByPrice[i + 1].Pmin;
                                production = payload.Load - adjustment >= powerPlantsSortedByPrice[i].Pmin ? payload.Load - adjustment : 0;
                            }
                        }
                    }

                    else if (adjustment >= powerPlantsSortedByPrice[i].Pmin)
                    {
                        production = adjustment;
                        adjustment -= production;
                    }

                    else
                        production = 0;
                }

                production = Math.Round(production, 1); // Round to the nearest 0.1 MW
                productionResults.Add(new ProductionResult { Name = powerPlantsSortedByPrice[i].Name, P = production });
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
