namespace Shoope.Application.DTOs
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? Cpf { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Token { get; set; }
        public string? UserImage { get; set; }
        public string? Base64ImageUser { get; set; }

        public UserDTO(Guid? id, string? name, string? email, string? gender, string? phone, string? password, string? passwordHash, string? salt, string? cpf, 
            DateTime? birthDate, string? token, string? userImage)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
            Phone = phone;
            Password = password;
            PasswordHash = passwordHash;
            Salt = salt;
            Cpf = cpf;
            BirthDate = birthDate;
            Token = token;
            UserImage = userImage;
        }

        public UserDTO(string? name, string? email)
        {
            Name = name;
            Email = email;
        }

        public UserDTO(string? name, string? email, string? password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public UserDTO()
        {
        }

        public void SetName(string? name)
        {
            Name = name;
        }

        public void SetEmail(string? email)
        {
            Email = email;
        }

        public void SetId(Guid? id)
        {
            Id = id;
        }

        public void SetPhone(string? phone)
        {
            Phone = phone;
        }

        public void SetToken(string? token)
        {
            Token = token;
        }

        public Guid? GetId()
        {
            return Id;
        }

        public string? GetEmail()
        {
            return Email;
        }
    }
}
