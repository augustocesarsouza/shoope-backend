
using Shoope.Domain.Entities;
using Shoope.Domain.InfoErrors;

namespace Shoope.Infra.Data.SendEmailUser.Interface
{
    public interface ISendEmailUser
    {
        public Task<InfoErrors> SendEmail(User user);
        public InfoErrors SendCodeRandom(User user, int code);
    }
}
