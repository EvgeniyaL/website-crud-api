using System.Threading.Tasks;
using TitanGate.Website.Api.Handlers.Services;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using Xunit;

namespace TitanGate.Website.Api.UnitTests
{
    public class PasswordHashServiceTests : TestsSetup
    {
        private const string Password = "very-strong-password";
        private const char Delimiter = '#';

        private readonly IPasswordHashService _passwordHashService;

        public PasswordHashServiceTests()
        {
            _passwordHashService = new PasswordHashService();
        }

        [Fact]
        public async Task HashWithSaltPassword_ShouldReturnSaltWithHashString()
        {
            var saltAndHash = _passwordHashService.HashWithSaltPassword(Password);
            var splited = saltAndHash.Split(Delimiter);
            Assert.Equal(2, splited.Length);
        }

        [Fact]
        public async Task VerifyPassword_ShouldReturnTrue_WhenPasswordsAreMaching()
        {
            var saltAndHash = _passwordHashService.HashWithSaltPassword(Password);
            var match = _passwordHashService.VerifyPassword(Password, saltAndHash);
            Assert.True(match);
        }

        [Fact]
        public async Task VerifyPassword_ShouldReturnFalse_WhenPasswordsAreNotMaching()
        {
            var saltAndHash = _passwordHashService.HashWithSaltPassword(Password);
            var match = _passwordHashService.VerifyPassword("NotTheSamePassword", saltAndHash);
            Assert.False(match);
        }
    }
}
