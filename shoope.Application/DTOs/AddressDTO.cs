namespace Shoope.Application.DTOs
{
    public class AddressDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string StateCity { get;  set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string NumberHome { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public byte DefaultAddress { get; set; }

        public Guid UserId { get; set; }
        public UserDTO? UserDTO { get; set; }

        public AddressDTO()
        {
        }

        public AddressDTO(Guid id, string fullName, string phoneNumber, string cep, string stateCity, string neighborhood,
            string street, string numberHome, string complement, Guid userId, UserDTO? userDTO)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Cep = cep;
            StateCity = stateCity;
            Neighborhood = neighborhood;
            Street = street;
            NumberHome = numberHome;
            Complement = complement;
            UserId = userId;
            UserDTO = userDTO;
        }
    }
}
