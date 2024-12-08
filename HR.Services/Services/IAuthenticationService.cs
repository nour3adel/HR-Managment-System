using HR.Domain.DTOs.Employee;
using HR.Services.Bases;

namespace HR.Services.Services
{
    public interface IAuthenticationService
    {
        public Task<Response<JwtAuthResult>> Login(LoginDTO logindata);
        public Task<Response<string>> ValidateToken(string accessToken);

    }
}
