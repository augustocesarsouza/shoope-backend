namespace Shoope.Domain.Authentication
{
    public interface ICurrentUser
    {
        public string? Phone { get; }
        public bool IsValid { get; }
    }
}
