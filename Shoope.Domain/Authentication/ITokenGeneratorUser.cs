using Shoope.Domain.Entities;
using Shoope.Domain.InfoErrors;

namespace Shoope.Domain.Authentication
{
    public interface ITokenGeneratorUser
    {
        InfoErrors<TokenOutValue> Generator(User user);
    }
}
