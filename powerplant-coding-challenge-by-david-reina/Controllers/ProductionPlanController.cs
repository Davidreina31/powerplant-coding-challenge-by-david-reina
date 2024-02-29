using Microsoft.AspNetCore.Mvc;
using powerplant_coding_challenge.Contracts;
using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlan _productionPlan;

        public ProductionPlanController(IProductionPlan productionPlan)
        {
            _productionPlan = productionPlan;
        }
        [HttpPost("/productionplan")]
        public ActionResult<IEnumerable<ProductionResult>> CalculateProductionPlan([FromBody] Payload payload)
        {
           return Ok(_productionPlan.CalculateProduction(payload));
        }
    }
    
}
