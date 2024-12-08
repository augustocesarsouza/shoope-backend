namespace Shoope.Domain.Entities
{
    public class UserSellerProduct
    {
        public Guid? Id { get; private set; }
        public string? Name { get; private set; }
        public string? ImgPerfil { get; private set; }
        public string? ImgPerfilPublicId { get; private set; }
        public string? ImgFloating { get; private set; }
        public string? ImgFloatingPublicId { get; private set; }
        public DateTime? LastLogin { get; private set; }

        public double? Reviews { get; private set; }
        public int? ChatResponseRate { get; private set; }
        public DateTime? AccountCreationDate { get; private set; }
        public double? QuantityOfProductSold { get; private set; }
        public string? UsuallyRespondsToChatIn { get; private set; }
        public double? Followers { get; private set; }//Tem que criar uma tabela que segura quem está following who

        public UserSellerProduct(Guid? id, string? name, string? imgPerfil, string? imgPerfilPublicId, string? imgFloating, string? imgFloatingPublicId, DateTime? lastLogin, 
            double? reviews, int? chatResponseRate, DateTime? accountCreationDate, double? quantityOfProductSold, string? usuallyRespondsToChatIn, double? followers)
        {
            Id = id;
            Name = name;
            ImgPerfil = imgPerfil;
            ImgPerfilPublicId = imgPerfilPublicId;
            ImgFloating = imgFloating;
            ImgFloatingPublicId = imgFloatingPublicId;
            LastLogin = lastLogin;
            Reviews = reviews;
            ChatResponseRate = chatResponseRate;
            AccountCreationDate = accountCreationDate;
            QuantityOfProductSold = quantityOfProductSold;
            UsuallyRespondsToChatIn = usuallyRespondsToChatIn;
            Followers = followers;
        }

        public UserSellerProduct()
        {
        }
    }
}
