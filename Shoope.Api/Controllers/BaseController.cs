using Microsoft.AspNetCore.Mvc;
using Shoope.Api.ControllersInterface;
using Shoope.Application.DTOs;
using Shoope.Domain.Authentication;

namespace Shoope.Api.Controllers
{
    [ApiController]
    public class BaseController : IBaseController
    {
        [NonAction]
        public UserAuthDTO? Validator(ICurrentUser? currentUser)
        {
            // primeiro ele vai no "ICurrentUser" e depois aqui aparentemente
            if (currentUser != null && currentUser.IsValid == false)
                return null;

            if (currentUser == null || string.IsNullOrEmpty(currentUser.Phone))
                return null;

            if (!currentUser.IsValid)
                return null;

            if (!string.IsNullOrEmpty(currentUser.Phone))
            {
                return new UserAuthDTO { Phone = currentUser.Phone };
            }

            return null;
        }

        [NonAction]
        public IActionResult Forbidden()
        {
            var obj = new
            {
                code = "acesso_negado",
                message = "Usuario não contem as devidas informações necessarias para acesso"
            };

            return new ObjectResult(obj) { StatusCode = 403 };
        }
    }
}
