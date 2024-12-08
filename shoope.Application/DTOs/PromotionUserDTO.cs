namespace Shoope.Application.DTOs
{
    public class PromotionUserDTO
    {
        public Guid? Id { get; set; }

        public Guid? PromotionId { get; set; }
        public PromotionDTO? PromotionDTO { get; set; }

        public Guid? UserId { get; set; }
        public UserDTO? UserDTO { get; set; }

        public PromotionUserDTO()
        {
        }

        public PromotionUserDTO(Guid? id, Guid? promotionId, PromotionDTO? promotionDTO, Guid? userId, UserDTO? userDTO)
        {
            Id = id;
            PromotionId = promotionId;
            PromotionDTO = promotionDTO;
            UserId = userId;
            UserDTO = userDTO;
        }

        public PromotionUserDTO(Guid? id, PromotionDTO? promotionDTO, UserDTO? userDTO)
        {
            Id = id;
            PromotionDTO = promotionDTO;
            UserDTO = userDTO;
        }
    }
}
