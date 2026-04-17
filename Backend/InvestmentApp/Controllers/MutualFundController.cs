using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Entity;

namespace InvestmentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutualFundController : ControllerBase
    {
        private readonly IMutualFundService _mutualFundService;

        public MutualFundController(IMutualFundService mutualFundService)
        {
            _mutualFundService = mutualFundService;
        }

        [HttpGet("GetAllMutualFunds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<MutualFund>> GetAllMutualFunds()
        {
            var mutualFunds = _mutualFundService.GetAllMutualFunds();

            if (mutualFunds != null && mutualFunds.Count > 0)
            {
                return Ok(mutualFunds);
            }
            return NotFound("No mutual funds found");
        }
    }
}
