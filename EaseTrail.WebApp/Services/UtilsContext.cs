using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Outputs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EaseTrail.WebApp.Services
{
    /// <summary>
    /// Methods that are used in more than one context
    /// </summary>
    public class UtilsContext : IUtilsContext
    {
        private readonly Context _context;
        private readonly SymmetricSecurityKey _signingKey;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilsContext(Context context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            var secretKey = configuration["JwtSettings:SecretKey"];
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public TonkenCredInfoDto GetUserInfo()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            token = token.Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

                TonkenCredInfoDto tonkenCredInfo = new TonkenCredInfoDto(claimsPrincipal.FindFirst("id")?.Value, Convert.ToInt32(claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value));

                return tonkenCredInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
