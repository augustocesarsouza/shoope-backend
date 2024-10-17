using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public UserController(IUserManagementService userManagementService, IUserAuthenticationService userAuthenticationService,
            IBaseController baseController,
            ICurrentUser currentUser)
        {
            _userManagementService = userManagementService;
            _userAuthenticationService = userAuthenticationService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/user/get-user-by-id/{userId}")]
        public async Task<IActionResult> GetByIdInfoUser([FromRoute] string userId)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _userAuthenticationService.GetByIdInfoUser(userId);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/user/get-user/{phone}")]
        public async Task<IActionResult> GetUser([FromRoute] string phone)
        {
            var result = await _userManagementService.CheckEmailAlreadyExists(phone);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/public/user/login/{phone}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string phone, [FromRoute] string password)
        {
            var result = await _userAuthenticationService.Login(phone, password);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/user/verify-password/{phone}/{password}")]
        public async Task<IActionResult> VerifyPasswordUser([FromRoute] string phone, [FromRoute] string password)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _userAuthenticationService.VerifyPasswordUser(phone, password);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPost("v1/user/change-password")]
        public async Task<IActionResult> ChangePasswordUser([FromBody] UserChangePasswordDTO userChangePasswordDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var results = await _userAuthenticationService.ChangePasswordUser(userChangePasswordDTO);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [HttpPost("v1/public/user/verific")]
        public async Task<IActionResult> Verfic([FromBody] UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            var results = await _userAuthenticationService.Verfic(userConfirmCodeEmailDTO);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [Authorize]
        [HttpPost("v1/user/confirm-email-send-code")]
        public async Task<IActionResult> ConfirmEmailSendCode([FromBody] UserDTO userDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _userAuthenticationService.SendCodeEmail(userDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/user/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserDTO userDTO)
        {
            var result = await _userManagementService.Create(userDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("v1/user/update-user")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateFillDTO userUpdateFillDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _userManagementService.UpdateUser(userUpdateFillDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("v1/user/update-user-all")]
        public async Task<IActionResult> UpdateAllAsync([FromBody] UserUpdateAllDTO userUpdateAllDTO)
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _userManagementService.UpdateUserAll(userUpdateAllDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
