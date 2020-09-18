namespace UISS.Data.Models.Identity
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public class User
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public string Email { get; set; }

        [BsonRequired]
        public int UserRole { get; set; }

        [BsonRequired]
        public string RegistrationCode { get; set; }

        [BsonRequired]
        public bool IsActive { get; set; }

        [BsonRequired]
        public DateTime GeneratedCodeOn { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
