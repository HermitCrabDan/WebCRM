namespace WebCRM.RoleSecurity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using WebCRM.RoleSecurity.Helpers;

    public class UserService: IUserService
    {
        public UserService()
        {
        }

        private readonly AuthSettings _authSettings;
        public UserService(IOptions<AuthSettings> appSettings) => _authSettings = appSettings.Value;

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            return null;

            //var token = GenerateJwtToken(user);

            //return new AuthenticateResponse(user, token);
        }

        
        //public User GetById(int id) => _securityContext.Users.FirstOrDefault(u => u.Id == id);

        private string GenerateJwtToken(User user)
        {
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}