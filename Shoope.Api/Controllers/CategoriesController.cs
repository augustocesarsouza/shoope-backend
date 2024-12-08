using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly ICategoriesService _categoriesService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public CategoriesController(ICategoriesService categoriesService, IBaseController baseController, ICurrentUser currentUser)
        {
            _categoriesService = categoriesService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpGet("v1/get-categories-by-id/{categoryId}")]
        public async Task<IActionResult> GetCategoriesById([FromRoute] string categoryId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _categoriesService.GetCategoriesById(Guid.Parse(categoryId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await _categoriesService.GetAllCategories();

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/categories/create")]
        public async Task<IActionResult> CreateAsync([FromBody] CategoriesDTO categoriesDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _categoriesService.CreateAsync(categoriesDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
