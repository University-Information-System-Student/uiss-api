namespace UISS.Services.Contracts
{
    using System.Threading.Tasks;

    using Models.User;

    public interface IUserService
    {
        Task<UserServiceModel> GetUserByUserNameAsync(string userNama);
    }
}
