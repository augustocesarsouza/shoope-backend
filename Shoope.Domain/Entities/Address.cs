namespace Shoope.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Cep { get; private set; } = string.Empty;
        public string StateCity { get; private set; } = string.Empty;
        public string Neighborhood { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;
        public string NumberHome { get; private set; } = string.Empty;
        public string? Complement { get; private set; }

        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public Address()
        {
        }

        public Address(Guid id, string fullName, string phoneNumber, string cep, string stateCity, 
            string neighborhood, string street, string numberHome, string complement, Guid userId)
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
        }
    }
}
