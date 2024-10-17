namespace Shoope.Application.DTOs
{
    public class UserUpdateFillDTO
    {
        public string? UserId { get; set; }
        public string? Cpf { get; set; }
        public string? BirthDate { get; set; }

        public UserUpdateFillDTO(string? userId, string? cpf, string? birthDate)
        {
            UserId = userId;
            Cpf = cpf;
            BirthDate = birthDate;
        }

        public UserUpdateFillDTO()
        {
        }
    }
}
