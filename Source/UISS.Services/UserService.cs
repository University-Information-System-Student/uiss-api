namespace UISS.Services
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Models.User;
    using Data.Contracts;
    using Data.Models.Identity;

    public class UserService : IUserService
    {
        private readonly IIdentityDbContext identityDb;

        public UserService(IIdentityDbContext identityDb)
        {
            this.identityDb = identityDb;
        }

        public async Task<UserServiceModel> GetUserByUserNameAsync(string usernama)
        {
            var registrationCode = this.GegenerateCode();

            this.identityDb.UserCollection.InsertOne(new User()
            {
                Id = Guid.NewGuid(),
                UserRole = 1,
                RegistrationCode = "12-12-32-544445-65",
                IsActive = true,
                IsActivatedCode = false,
                GeneratedCodeOn = DateTime.UtcNow,
            });

            // TODO: Add implementation For GetUserByUserNameAsync

            throw new System.NotImplementedException();
        }

        public async Task<string> InsertEmptyUserAsync(int userRole)
        {
            var registrationCode = this.GegenerateCode();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserRole = userRole,
                RegistrationCode = registrationCode,
                IsActive = true,
                IsActivatedCode = false,
                GeneratedCodeOn = DateTime.UtcNow,
            };

            await this.identityDb.UserCollection
                .InsertOneAsync(user);

            return registrationCode;
        }

        private string GegenerateCode()
        {
            return Guid.NewGuid()
                .ToString()
                .ToUpper()
                .Trim();
        }
    }
}
