namespace TitanGate.Website.Api.Handlers.ServicesContracts
{
    public interface IPasswordHashService
    {
        string HashWithSaltPassword(string password);

        bool VerifyPassword(string password, string passwordHash);
    }
}
