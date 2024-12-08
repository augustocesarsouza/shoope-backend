using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace Shoope.Application.Services
{
    public class UserConfirmationService : IUserConfirmationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheRedisUti _cacheRedisUti;

        public UserConfirmationService(IUserRepository userRepository, IUnitOfWork unitOfWork, ICacheRedisUti cacheRedisUti)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _cacheRedisUti = cacheRedisUti;
        }

        public async Task<ResultService<TokenAlreadyVisualizedDTO>> GetConfirmToken(string token)
        {
            // isso para criação de uma nova conta
            var tokenDecoder = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken;
            try
            {
                securityToken = (JwtSecurityToken)tokenDecoder.ReadToken(token);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<TokenAlreadyVisualizedDTO>($"erro relacionado a crição do 'JwtSecurityToken' {ex.Message}");
            }


            if (securityToken != null)
            {
                string idClaimValue = "";
                string expClaimValue = "";

                foreach (var claim in securityToken.Claims)
                {
                    if (claim.Type == "id")
                    {
                        idClaimValue = claim.Value;
                    }
                    else if (claim.Type == "exp")
                    {
                        expClaimValue = claim.Value;
                    }
                }

                if (expClaimValue.Length > 0)
                {
                    var time = long.Parse(expClaimValue);
                    DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
                    if (!(dt.DateTime > DateTime.UtcNow))
                        return ResultService.Fail<TokenAlreadyVisualizedDTO>("Token Expirou");
                }
                else
                    return ResultService.Fail<TokenAlreadyVisualizedDTO>("O valor expiração não foi fornecido");

                if (idClaimValue.Length > 0)
                {
                    try
                    {
                        await _unitOfWork.BeginTransaction();
                        var id = Guid.Parse(idClaimValue);

                        var chaveKey = "TokenString" + id;
                        var cache = await _cacheRedisUti.GetStringAsyncWrapper(chaveKey);
                        if (!string.IsNullOrEmpty(cache))
                        {
                            _cacheRedisUti.RemoveWrapper(chaveKey);
                        }
                        else
                            return ResultService.Ok(new TokenAlreadyVisualizedDTO { TokenAlreadyVisualized = 1 });

                        var user = await _userRepository.GetUserById(id);
                        if (user == null)
                            return ResultService.Fail<TokenAlreadyVisualizedDTO>("banco retornou null");

                        user.ConfirmedEmail(1);
                        var userBanco = await _userRepository.UpdateAsync(user);
                        if (userBanco == null)
                            return ResultService.Fail<TokenAlreadyVisualizedDTO>("erro ao atualiza o usuario no banco");

                        await _unitOfWork.Commit();
                        return ResultService.Ok(new TokenAlreadyVisualizedDTO { TokenAlreadyVisualized = 0, ErroMessage = "tudo certo" });
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.Rollback();
                        return ResultService.Fail<TokenAlreadyVisualizedDTO>(ex.Message);
                    }
                }
                else
                    return ResultService.Fail<TokenAlreadyVisualizedDTO>("Valor do id claim não foi informado");

            }
            else
                return ResultService.Fail<TokenAlreadyVisualizedDTO>("Token Invalido");
        }
    }
}
