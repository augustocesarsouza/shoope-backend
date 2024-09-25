using Microsoft.AspNetCore.Mvc;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UserController(IUserManagementService userManagementService, IUserAuthenticationService userAuthenticationService)
        {
            _userManagementService = userManagementService;
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpGet("v1/public/user/get-user-by-id/{userId}")]
        public async Task<IActionResult> GetByIdInfoUser([FromRoute] string userId)
        {
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

        [HttpPost("v1/public/user/verific")]
        public async Task<IActionResult> Verfic([FromBody] UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            var results = await _userAuthenticationService.Verfic(userConfirmCodeEmailDTO);

            if (results.IsSucess)
                return Ok(results);

            return BadRequest(results);
        }

        [HttpPost("v1/public/user/confirm-email-send-code")]
        public async Task<IActionResult> ConfirmEmailSendCode([FromBody] UserDTO userDTO)
        {
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

        [HttpPut("v1/public/user/update-user")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateFillDTO userUpdateFillDTO)
        {
            var result = await _userManagementService.UpdateUser(userUpdateFillDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("v1/public/user/update-user-all")]
        public async Task<IActionResult> UpdateAllAsync([FromBody] UserUpdateAllDTO userUpdateAllDTO)
        {
            var result = await _userManagementService.UpdateUserAll(userUpdateAllDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
