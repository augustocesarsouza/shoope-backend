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
        public byte DefaultAddress { get; private set; }

        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public Address()
        {
        }

        public Address(Guid id, string fullName, string phoneNumber, string cep, string stateCity, 
            string neighborhood, string street, string numberHome, string complement, byte defaultAddress, Guid userId)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Cep = cep;
            StateCity = stateCity;
            Neighborhood = neighborhood;
            Street = street;
            NumberHome = numberHome;
            DefaultAddress = defaultAddress;
            Complement = complement;
            UserId = userId;
        }

        public Address(string fullName, string phoneNumber, string cep, string stateCity, string neighborhood, string street, string numberHome, string complement,
            byte defaultAddress)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Cep = cep;
            StateCity = stateCity;
            Neighborhood = neighborhood;
            Street = street;
            NumberHome = numberHome;
            Complement = complement;
            DefaultAddress = defaultAddress;
        }

        public void SetValueToUpdateAddress(string fullName, string phoneNumber, string cep, string stateCity, string neighborhood, string street, string numberHome, 
            string complement)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Cep = cep;
            StateCity = stateCity;
            Neighborhood = neighborhood;
            Street = street;
            NumberHome = numberHome;
            Complement = complement;
        }

        public void SetValueToCreateAddress(Guid id, string fullName, string phoneNumber, string cep, string stateCity,
            string neighborhood, string street, string numberHome, string complement, byte defaultAddress, Guid userId)
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
            DefaultAddress = defaultAddress;
            UserId = userId;
        }

        public void SetFullName(string fullName)
        {
            FullName = fullName;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetCep(string cep)
        {
            Cep = cep;
        }

        public void SetStateCity(string stateCity)
        {
            StateCity = stateCity;
        }

        public void SetNeighborhood(string neighborhood)
        {
            Neighborhood = neighborhood;
        }

        public void SetStreet(string street)
        {
            Street = street;
        }

        public void SetNumberHome(string numberHome)
        {
            NumberHome = numberHome;
        }

        public void SetComplement(string complement)
        {
            Complement = complement;
        }

        public void SetDefaultAddress(byte defaultAddress)
        {
            DefaultAddress = defaultAddress;
        }
    }
}
