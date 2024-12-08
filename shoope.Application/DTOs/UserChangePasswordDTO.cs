namespace Shoope.Application.DTOs
{
    public class UserChangePasswordDTO
    {
        //public Guid? Id { get; set; }
        public string Phone { get; set; }
        public string ConfirmPassword { get; set; }

        //public string NewPassword { get; set; }

        public UserChangePasswordDTO(string phone, string confirmPassword)
        {
            Phone = phone;
            ConfirmPassword = confirmPassword;
        }
    }
}
