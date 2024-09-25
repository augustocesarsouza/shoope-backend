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

        public UserDTO(Guid? id, string? name, string? email, string? gender, string? phone, string? password, string? passwordHash, string? salt, string? cpf, 
            DateTime? birthDate, string? token)
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
        }

        public UserDTO(string? name, string? email)
        {
            Name = name;
            Email = email;
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

        public void SetId(Guid id)
        {
            Id = id;
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
