using AutoMapper;
using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlan _productionPlan;
        private readonly IMapper _mapper;

        public ProductionPlanController(IProductionPlan productionPlan, IMapper mapper)
        {
            _productionPlan = productionPlan;
            _mapper = mapper;
        }
        [HttpPost("/productionplan")]
        public ActionResult<IEnumerable<ProductionResult>> CalculateProductionPlan([FromBody] Payload payload)
        {
           return Ok(_productionPlan.CalculateProduction(_mapper.Map<Domain.Payload>(payload)));
        }
    }
    
}
