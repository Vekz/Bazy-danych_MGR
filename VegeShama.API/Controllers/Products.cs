using Microsoft.AspNetCore.Mvc;
using VegeShama.Common.APIModels;
using VegeShama.Domain.Services.Interfaces;

namespace VegeShama.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Products : ControllerBase
    {
        private readonly IProductsService _productsService;
        public Products(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productsService.GetAll());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _productsService.GetProduct(id));
        }

        [HttpPost]
        public async Task<IActionResult> Register(AddProductModel model)
        {
            return Created("/{id}", await _productsService.AddProduct(model));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductModel model)
        {
            return Ok(await _productsService.UpdateProduct(id, model));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productsService.DeleteProduct(id);
            return Ok();
        }
    }
}
