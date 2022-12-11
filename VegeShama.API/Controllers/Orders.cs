using Microsoft.AspNetCore.Mvc;
using VegeShama.Common.APIModels;
using VegeShama.Domain.Services.Interfaces;

namespace VegeShama.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Orders : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public Orders(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _ordersService.GetOrder(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrderModel model)
        {
            return Created("/{id}", await _ordersService.AddOrder(model));
        }
    }
}
