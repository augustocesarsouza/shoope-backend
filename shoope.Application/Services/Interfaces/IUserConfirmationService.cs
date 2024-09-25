using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IUserConfirmationService
    {
        public Task<ResultService<TokenAlreadyVisualizedDTO>> GetConfirmToken(string token);
    }
}
