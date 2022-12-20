using Microsoft.AspNetCore.Mvc;
using VegeShama.Domain.Services.Interfaces;

namespace VegeShama.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test : ControllerBase
    {
        private readonly ITestService _testService;
        public Test(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public IActionResult RunTests()
        {
            return Ok(_testService.RunTests());
        }
    }
}
