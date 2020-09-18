namespace UISS.Services.Contracts
{
    using System;

    public interface IJwtService
    {
        string GenerateJwt(Guid id, string userName, string userRole);
    }
}
