using BoxMirror.Service.Models;
using BoxMirror.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoxedMirror.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class LaserController : ControllerBase
    {
        private readonly BoxLaserService _boxLaserService;

        public LaserController(BoxLaserService boxLaserService)
        {
            _boxLaserService = boxLaserService;
        }

        [Route("/Laser/Process")]
        [HttpPost]
        public Room Process([FromBody]BoxInput boxInput)
        {
           return  _boxLaserService.ProcessLaser(boxInput);
        }

    }
}
