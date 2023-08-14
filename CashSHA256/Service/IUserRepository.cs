using CashSHA256.Resources;

namespace CashSHA256.Service
{
    public interface IUserRepository
    {
        Task<UserResource> Register(RegisterResource register, CancellationToken cancellationToken);
        Task<UserResource> Login(LoginResource login, CancellationToken cancellationToken);
    }
}
