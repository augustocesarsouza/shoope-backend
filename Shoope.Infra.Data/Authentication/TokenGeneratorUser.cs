using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shoope.Domain.Authentication;
using Shoope.Domain.Entities;
using Shoope.Domain.InfoErrors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shoope.Infra.Data.Authentication
{
    public class TokenGeneratorUser : ITokenGeneratorUser
    {
        private readonly IConfiguration _configuration;

        public TokenGeneratorUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public InfoErrors<TokenOutValue> Generator(User user)
        {
            if (string.IsNullOrEmpty(user.Phone))
                return InfoErrors.Fail(new TokenOutValue(), "Phone or password null check");

            var claims = new List<Claim>
            {
                new Claim("Phone", user.Phone),
                new Claim("userID", user.Id.ToString())
            };

            var keySecret = _configuration["KeyJWT"];

            if (string.IsNullOrEmpty(keySecret) || keySecret.Length < 16)
                return InfoErrors.Fail(new TokenOutValue(), "error token related");

            var expires = DateTime.UtcNow.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            var tokenValue = new TokenOutValue();
            var sucessfullyCreatedToken = tokenValue.ValidateToken(token, expires);

            if (sucessfullyCreatedToken)
            {
                return InfoErrors.Ok(tokenValue);
            }
            else
            {
                return InfoErrors.Fail(new TokenOutValue(), "error when creating token");
            }
        }
    }
}
