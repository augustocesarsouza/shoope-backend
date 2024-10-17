using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class CuponController : ControllerBase
    {
        public readonly ICuponService _cuponService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public CuponController(ICuponService cuponService, IBaseController baseController, ICurrentUser currentUser)
        {
            _cuponService = cuponService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        //[Authorize]
        [HttpGet("v1/get-cupon-by-id/{cuponId}")]
        public async Task<IActionResult> GetByUserId([FromRoute] string cuponId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _cuponService.GetCuponById(Guid.Parse(cuponId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/cupon/create")]
        public async Task<IActionResult> CreateAsync([FromBody] CuponDTO cuponDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _cuponService.CreateAsync(cuponDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
