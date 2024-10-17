namespace Shoope.Application.DTOs
{
    public class UserCuponDTO
    {
        public Guid? Id { get; set; }

        public Guid? CuponId { get; set; }
        public CuponDTO? CuponDTO { get; set; }

        public Guid? UserId { get; set; }
        public UserDTO? UserDTO { get; set; }

        public UserCuponDTO()
        {
        }

        public UserCuponDTO(Guid? id, Guid? cuponId, CuponDTO? cuponDTO, Guid? userId, UserDTO? userDTO)
        {
            Id = id;
            CuponId = cuponId;
            CuponDTO = cuponDTO;
            UserId = userId;
            UserDTO = userDTO;
        }
    }
}
