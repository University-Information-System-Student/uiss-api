namespace UISS.Data
{
    using Contracts;
    using MongoDB.Driver;

    public class IdentityDbContext : IIdentityDbContext
    {
        private readonly IMongoDatabase database;
    }
}
