using Shoope.Domain.Validations;

namespace Shoope.Domain.Entities
{
    public class User
    {
        public Guid? Id { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Gender { get; private set; }
        public string? Phone { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? Salt { get; private set; }
        public string? Cpf { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string? UserImage { get; private set; }

        //public string? Token { get; private set; }
        //public int? ConfirmEmail { get; private set; }

        public User()
        {
        }

        public User(Guid? id, string? name, string? email, string? gender, string? phone, 
            string? passwordHash, string? salt, string? cpf, DateTime? birthDate, string? userImage)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
            Phone = phone;
            PasswordHash = passwordHash;
            Salt = salt;
            Cpf = cpf;
            BirthDate = birthDate;
            UserImage = userImage;
        }

        public User(string? name, string? email, string? gender, string? phone)
        {
            Name = name;
            Email = email;
            Gender = gender;
            Phone = phone;
        }

        public User(string? passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetValueUpdateUser(string? name, string? email, string? gender, string? phone, string? userImage)
        {
            Name = name;
            Email = email;
            Gender = gender;
            Phone = phone;
            UserImage = userImage;
        }

        //public void ValidatorToken(string token)
        //{
        //    DomainValidationException.When(string.IsNullOrEmpty(token), "Token not generated");
        //    Token = token;
        //}

        public void ConfirmedEmail(int value)
        {
            //ConfirmEmail = value;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetCpf(string cpf)
        {
            Cpf = cpf;
        }

        public void SetBirthdate(DateTime birthDate)
        {
            BirthDate = birthDate;
        }

        public void SetGender(string gender)
        {
            Gender = gender;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetSalt(string salt)
        {
            Salt = salt;
        }

        public void SetGuidId(Guid? userId)
        {
            Id = userId;
        }

        public string GetPasswordHash()
        {
            if(PasswordHash != null)
            {
                return PasswordHash;
            }

            return "";
        }

        public string GetSalt()
        {
            if (Salt != null)
            {
                return Salt;
            }

            return "";
        }

        public Guid? GetId()
        {
            return Id;
        }
    }
}
