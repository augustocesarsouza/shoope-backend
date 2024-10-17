using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class UserCuponController : ControllerBase
    {
        public readonly IUserCuponService _userCuponService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public UserCuponController(IUserCuponService userCuponService, IBaseController baseController, ICurrentUser currentUser)
        {
            _userCuponService = userCuponService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/get-all-cupon-by-user-id/{userId}")]
        public async Task<IActionResult> GetAllCuponByUserId([FromRoute] string userId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userCuponService.GetAllCuponByUserId(Guid.Parse(userId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/user-cupon/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserCuponDTO userCuponDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userCuponService.Create(userCuponDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
