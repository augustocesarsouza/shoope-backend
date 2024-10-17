using Microsoft.AspNetCore.Mvc;
using Shoope.Application.DTOs;
using Shoope.Domain.Authentication;

namespace Shoope.Api.ControllersInterface
{
    public interface IBaseController
    {
        public UserAuthDTO? Validator(ICurrentUser? currentUser);
        public IActionResult Forbidden();
    }
}
