namespace Shoope.Domain.Entities
{
    public class UserCupon
    {
        public Guid? Id { get; private set; }

        public Guid? CuponId { get; private set; }
        public Cupon? Cupon { get; private set; }

        public Guid? UserId { get; private set; }
        public User? User { get; private set; }

        public UserCupon() { }

        public UserCupon(Guid? id, Guid? cuponId, Cupon? cupon, Guid? userId, User? user)
        {
            Id = id;
            CuponId = cuponId;
            Cupon = cupon;
            UserId = userId;
            User = user;
        }
    }
}
