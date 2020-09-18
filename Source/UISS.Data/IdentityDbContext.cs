namespace UISS.Data
{
    using MongoDB.Driver;

    using Contracts;
    using Models.Identity;

    public class IdentityDbContext : IIdentityDbContext
    {
        private IMongoDatabase database;

        public IdentityDbContext()
        {
            this.InitializeDatabase();
        }

        public IMongoCollection<User> UserCollection { get; private set; }

        public IMongoCollection<RegistrationCode> RegistrationCodeCollection { get; private set; }

        private void InitializeDatabase()
        {
            MongoClient client = new MongoClient("Add ConnectionString");

            this.database = client
                .GetDatabase("Add DatabaseName");

            this.UserCollection = database
                .GetCollection<User>("Add UserCollectionName");

            this.RegistrationCodeCollection = database
                .GetCollection<RegistrationCode>("Add RegistrationCodeCollectionName");
        }
    }
}
