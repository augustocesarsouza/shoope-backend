namespace Shoope.Application.DTOs
{
    public class UserConfirmCodeEmailDTO
    {
        public string? Code { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }

        public UserConfirmCodeEmailDTO(string? code, string? userId, string? email)
        {
            Code = code;
            UserId = userId;
            Email = email;
        }

        public UserConfirmCodeEmailDTO()
        {
        }
    }
}
