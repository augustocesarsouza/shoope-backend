using Shoope.Domain.Entities;
using Shoope.Domain.InfoErrors;

namespace Shoope.Infra.Data.UtilityExternal.Interface
{
    public interface ISendEmailBrevo
    {
        public InfoErrors SendEmail(User user, string url);
        public InfoErrors SendCode(User user, int codeRandon);
    }
}
