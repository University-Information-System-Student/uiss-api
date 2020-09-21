namespace UISS.Services.Contracts
{
    public interface IJwtService
    {
        string GenerateJwt(string id, string userName, string userRole);
    }
}
