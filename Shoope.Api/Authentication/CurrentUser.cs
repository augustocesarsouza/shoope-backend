using Shoope.Domain.Authentication;

namespace Shoope.Api.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        public string? Phone { get; private set; }
        public bool IsValid { get; private set; }

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            InitializeFromHttpContext(httpContext);
        }

        private void InitializeFromHttpContext(IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext == null)
                return;

            var claims = httpContext.HttpContext.User.Claims;
            string headerValue = httpContext.HttpContext.Request.Headers["uid"];

            if (claims != null && claims.Any(x => x.Type == "Phone"))
            {
                try
                {
                    var guidId = claims.First(x => x.Type == "userID").Value; // se não mandar valor ele lança exception 
                    Phone = claims.First(x => x.Type == "Phone").Value;

                    if(headerValue == null)
                    {
                        IsValid = false;
                        return;
                    }

                    if (!headerValue.Equals(guidId))
                        IsValid = false;
                    else
                    {
                        var time = long.Parse(claims.First(x => x.Type == "exp").Value);
                        DateTimeOffset dt = DateTimeOffset.FromUnixTimeSeconds(time);
                        IsValid = dt.DateTime > DateTime.UtcNow;
                    }
                }
                catch (Exception ex)
                {
                    IsValid = false;
                }
            }

            //if (claims != null && claims.Any(x => x.Type == "Password"))
            //{
            //    Password = claims.First(x => x.Type == "Password").Value;
            //}
        }

        public void SetPhone(string? phone)
        {
            Phone = phone;
        }

        public void SetIsValid(bool isValid)
        {
            IsValid = isValid;
        }
    }
}
