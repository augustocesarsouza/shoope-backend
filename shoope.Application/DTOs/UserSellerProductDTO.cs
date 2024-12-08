namespace Shoope.Application.DTOs
{
    public class UserSellerProductDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? ImgPerfil { get; set; }
        public string? ImgPerfilBase64 { get; set; }
        public string? ImgPerfilPublicId { get; set; }
        public string? ImgFloating { get; set; }
        public string? ImgFloatingBase64 { get; set; }
        public string? ImgFloatingPublicId { get; set; }
        public DateTime? LastLogin { get; set; }

        public double? Reviews { get; set; }
        public int? ChatResponseRate { get; set; }
        public DateTime? AccountCreationDate { get; set; }
        public double? QuantityOfProductSold { get; set; }
        public string? UsuallyRespondsToChatIn { get; set; }
        public double? Followers { get; set; }

        public UserSellerProductDTO(Guid? id, string? name, string? imgPerfil, string? imgPerfilPublicId, string? imgFloating, string? imgFloatingPublicId, DateTime? lastLogin, 
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

        public UserSellerProductDTO()
        {
        }

        public void SetImgPerfil(string imgPerfil)
        {
            ImgPerfil = imgPerfil;
        }

        public void SetImgPerfilPublicId(string imgPerfilPublicId)
        {
            ImgPerfilPublicId = imgPerfilPublicId;
        }

        public void SetImgFloatingPublicId(string imgFloating)
        {
            ImgFloating = imgFloating;
        }

        public void SetImgFloatingPublicIdPublicId(string imgFloatingPublicId)
        {
            ImgFloatingPublicId = imgFloatingPublicId;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
