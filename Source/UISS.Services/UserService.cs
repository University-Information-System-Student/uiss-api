namespace UISS.Services
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;

    using Contracts;
    using Models.User;
    using Data.Contracts;
    using Data.Models.Identity;

    public class UserService : IUserService
    {
        private readonly IIdentityDbContext identityDb;
        private readonly IMapper mapper;

        public UserService(IIdentityDbContext identityDb, IMapper mapper)
        {
            this.identityDb = identityDb;
            this.mapper = mapper;
        }

        public async Task<UserServiceModel> GetUserByUsernameAsync(string usernama)
        {
            var filter =
                Builders<User>.Filter.Eq(x => x.Username, usernama) &
                Builders<User>.Filter.Eq(x => x.IsActive, true);

            var user = await (await identityDb.UserCollection
                .FindAsync(filter))
                .FirstOrDefaultAsync();

            return this.mapper.Map<UserServiceModel>(user);
        }

        public async Task<UserServiceModel> GetUserByRegistrationCodeAsync(string registrationCode)
        {
            var filter =
                Builders<User>.Filter.Eq(x => x.RegistrationCode, registrationCode) &
                Builders<User>.Filter.Eq(x => x.IsActive, true);

            var user = await (await identityDb.UserCollection
                .FindAsync(filter))
                .FirstOrDefaultAsync();

            return this.mapper.Map<UserServiceModel>(user);
        }

        public async Task<string> InsertUnregisteredUserAsync(int userRole)
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

        public async Task UpdateUserToRegistered(Guid id, string username, string hashedPassword, string email)
        {
            var filter =
                Builders<User>.Filter.Eq(x => x.Id, id) &
                Builders<User>.Filter.Eq(x => x.IsActive, true);

            var update = Builders<User>.Update
                .Set(x => x.Username, username)
                .Set(x => x.HashedPassword, hashedPassword)
                .Set(x => x.Email, email)
                .Set(x => x.IsActivatedCode, true)
                .Set(x => x.RegisteredOn, DateTime.UtcNow);

            await identityDb.UserCollection
                .UpdateOneAsync(filter, update);
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
