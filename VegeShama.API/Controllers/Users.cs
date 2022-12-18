using Microsoft.AspNetCore.Mvc;
using VegeShama.Common.APIModels;
using VegeShama.Domain.Services.Interfaces;

namespace VegeShama.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Users : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IAuthService _authService;
        private readonly IOrdersService _ordersService;
        public Users(IUsersService usersService, IAuthService authService, IOrdersService ordersService)
        {
            _usersService = usersService;
            _authService = authService;
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _usersService.GetUser(id));
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var userId = await _authService.Login(model);
            if (userId.HasValue)
                return Ok(userId);
            else
                return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            return Created("/{id}", await _usersService.RegisterUser(model));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserModel model)
        {
            return Ok(await _usersService.UpdateUser(id, model));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _usersService.DeleteUser(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id:Guid}/orders")]
        public async Task<IActionResult> GetForUser(Guid id)
        {
            var orders = await _ordersService.GetForUser(id);
            if (orders is null)
                return NotFound();
            return Ok(orders);
        }
    }
}