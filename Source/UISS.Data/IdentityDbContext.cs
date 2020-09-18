namespace UISS.Data
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    using Contracts;
    using Models.Identity;
    using GlobalConstants.ApplicationSettings;

    public class IdentityDbContext : IIdentityDbContext
    {
        private readonly IMongoDatabase database;

        private readonly IdentityDbSettings identityDbSettings;

        public IdentityDbContext(IOptions<ApplicationSettings> options)
        {
            this.identityDbSettings = options.Value.Database.IdentityDb;

            MongoClient client = new MongoClient(this.identityDbSettings.ConnectionString);

            this.database = client.GetDatabase(this.identityDbSettings.DatabaseName);

            this.InitializeCollections();
        }

        public IMongoCollection<User> UserCollection { get; private set; }

        private void InitializeCollections()
        {
            this.UserCollection = database
                .GetCollection<User>(this.identityDbSettings.UserCollectionName);
        }
    }
}
