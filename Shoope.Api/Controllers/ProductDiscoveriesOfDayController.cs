using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    public class ProductDiscoveriesOfDayController : ControllerBase
    {
        public readonly IProductDiscoveriesOfDayService productDiscoveriesOfDayService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public ProductDiscoveriesOfDayController(IProductDiscoveriesOfDayService productDiscoveriesOfDayService, 
            IBaseController baseController, ICurrentUser currentUser)
        {
            this.productDiscoveriesOfDayService = productDiscoveriesOfDayService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        // Faça um teste amanha criei um "token" com outra assinatura "keySecret" outra qualquer manda esse token com a requisição
        // seila postman da vida e ve que erro vai dar porque se for outra assinatura de key, é para dar erro
        [Authorize]
        [HttpGet("v1/get-product-discoveries-of-day-by-id/{productDiscoveriesOfDayId}")]
        public async Task<IActionResult> GetCategoriesById([FromRoute] string productDiscoveriesOfDayId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await productDiscoveriesOfDayService.GetProductDiscoveriesOfDayById(Guid.Parse(productDiscoveriesOfDayId));

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("v1/get-all-product-discoveries-of-day")]
        public async Task<IActionResult> GetAllCategories()
        {
            var userAuth = _baseController.Validator(_currentUser);
            if (userAuth == null)
                return _baseController.Forbidden();

            var result = await productDiscoveriesOfDayService.GetAllProductDiscoveriesOfDays();

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/product-discoveries-of-day/create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDiscoveriesOfDayDTO productDiscoveriesOfDayDTO)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await productDiscoveriesOfDayService.CreateAsync(productDiscoveriesOfDayDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
