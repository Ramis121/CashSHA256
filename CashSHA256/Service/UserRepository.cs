using CashSHA256.Data;
using CashSHA256.Model;
using CashSHA256.Resources;
using Microsoft.EntityFrameworkCore;

namespace CashSHA256.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDb;
        private readonly string _peper;
        private readonly int _iteration = 3;
        public UserRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
            _peper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
        }

        public async Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var user = await applicationDb.users
                .FirstOrDefaultAsync(x => x.name == resource.UserName, cancellationToken);

            if (user == null)
                throw new Exception("Username or password did not match.");

            var passwordHash = user.PasswordHash;
            if (user.PasswordHash != passwordHash)
                throw new Exception("Username or password did not match.");

            return new UserResource(user.id, user.name, user.email);
        }

        public async Task<UserResource> Register(RegisterResource register, CancellationToken cancellationToken)
        {
            var user = new User
            {
                email = register.Email,
                name = register.Username,
                PasswordSalt = PasswordHasher.GenerateSalt()
            };
            user.PasswordHash = PasswordHasher.ComputeHash(register.Password, register.Password, _peper, _iteration);
            await applicationDb.users.AddAsync(user, cancellationToken);
            await applicationDb.SaveChangesAsync(cancellationToken);

            return new UserResource(user.id, user.name, user.email);
        }
    }
}
