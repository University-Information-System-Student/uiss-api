namespace UISS.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    using Models.User;

    public interface IUserService
    {
        Task<UserServiceModel> GetUserByUsernameAsync(string userNama);

        Task<UserServiceModel> GetUserByRegistrationCodeAsync(string registrationCode);

        Task<string> InsertUnregisteredUserAsync(int userRole);

        Task UpdateUserToRegistered(Guid id, string username, string hashedPassword, string email);
    }
}
