using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Entity;
using Models.Request;

namespace InvestmentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("GetAllStocks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Stock>> GetAllStocks()
        {
            var stocks = _stockService.GetAllStocks();

            if (stocks != null && stocks.Count > 0)
            {
                return Ok(stocks);
            }
            return NotFound("No stocks found");
        }

        [HttpPost("InsertTransaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult InsertTransaction(TransactionRequest req)
        {
            try
            {
                _stockService.InsertTransaction(req);
                return Ok("Transaction inserted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

