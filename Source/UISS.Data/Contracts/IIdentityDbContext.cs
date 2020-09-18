namespace UISS.Data.Contracts
{
    using MongoDB.Driver;
    using UISS.Data.Models.Identity;

    public interface IIdentityDbContext
    {
        IMongoCollection<User> UserCollection { get; }
    }
}
