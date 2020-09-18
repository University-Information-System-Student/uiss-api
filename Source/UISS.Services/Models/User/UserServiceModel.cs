namespace UISS.Services.Models.User
{
    using System;

    public class UserServiceModel
    {
        public Guid Id { get; set; }

        public string HashedPassword { get; set; }

        public int UserRole { get; set; }

        public bool IsActivatedCode { get; set; }
    }
}
