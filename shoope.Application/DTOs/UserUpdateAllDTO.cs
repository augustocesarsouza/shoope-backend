namespace Shoope.Application.DTOs
{
    public class UserUpdateAllDTO
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Cpf { get; set; }
        //public DateTime? BirthDate { get; set; }
        public string? BirthDate { get; set; }
        public string? Base64StringImage { get; set; }

        public UserUpdateAllDTO(string? userId, string? name, string? email, string? gender, string? phone, string? cpf, string? birthDate, string? base64StringImage)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Gender = gender;
            Phone = phone;
            Cpf = cpf;
            BirthDate = birthDate;
            Base64StringImage = base64StringImage;
        }

        public UserUpdateAllDTO()
        {
        }
    }
}
