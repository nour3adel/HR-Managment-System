using HR.Domain.Classes;
using HR.Domain.DTOs.Employee;
using HR.Domain.Helpers;
using HR.Services.Bases;
using HR.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Services.Implementations
{
    public class AuthenticationService : ResponseHandler, IAuthenticationService
    {
        #region Fields

        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signin;
        private readonly JwtSettings _jwtSettings;



        #endregion

        #region Constructor
        public AuthenticationService(UserManager<Employee> userManager, SignInManager<Employee> signin, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signin = signin;
            _jwtSettings = jwtSettings;

        }

        #endregion

        #region Login Employee

        public async Task<Response<JwtAuthResult>> Login(LoginDTO logindata)
        {


            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(logindata.username);
            //Return The UserName Not Found
            if (user == null) return BadRequest<JwtAuthResult>("UserNameIsNotExist");

            //try To Sign in 
            var signInResult = await _signin.CheckPasswordSignInAsync(user, logindata.Password, false);
            if (!signInResult.Succeeded)
            {
                return BadRequest<JwtAuthResult>("PasswordNotCorrect");
            }

            //Generate Token
            var (jwtToken, accessToken) = await GenerateJwtToken(user);
            var response = new JwtAuthResult();

            response.AccessToken = accessToken;
            //return Token 
            return Success(response);
        }

        #region Generate JwtToken
        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(Employee user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        #endregion

        #region Get All Claims
        public async Task<List<Claim>> GetClaims(Employee user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        #endregion

        #endregion


        #region Validate Token
        public async Task<Response<string>> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = false // Don't validate lifetime here; we handle expiration separately.
            };

            try
            {
                // Validate the token
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    var expiration = jwtToken.ValidTo; // Get the token's expiration time (UTC)
                    var currentUtcTime = DateTime.UtcNow;

                    if (currentUtcTime >= expiration)
                    {
                        return BadRequest<string>("TokenExpired");
                    }

                    return Success($"Token is valid and will expire on: {expiration:O} (UTC).");
                }

                return BadRequest<string>("InvalidToken");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Validation failed: {ex.Message}");
            }
        }


        #endregion
    }
}
