using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Models;
using EaseTrail.WebApp.Models.Enums;
using EaseTrail.WebApp.Outputs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //Os métodos a baixo devem ser implementados na controller.

        public async Task<IActionResult> InviteUser(Guid userId, string addUserEmail, Guid workSpaceId, int colabType)
        {
            try
            {
                var user = GetUserInfo();

                var workSpace = await _context.WorkSpaces.FirstOrDefaultAsync(x => x.Id == workSpaceId && x.OwnerId == userId);

                if (workSpace == null)
                {
                    throw new Exception("WorkSpace não encontrado");
                }

                if(await _context.Users.FirstOrDefaultAsync(x => x.Email == addUserEmail) == null)
                {
                    //Aplicar fluxo do tem user
                    throw new Exception("Usuário não encontrado");
                }

                UsersWorkSpace userWorker = new UsersWorkSpace(addUserEmail, workSpaceId, (ColaboratorType)colabType);

                await _context.UsersWorkSpaces.AddAsync(userWorker);

                workSpace.UserCount += 1;

                await _context.SaveChangesAsync();

                return new CreatedResult();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IActionResult> RemoveUser(Guid userId, Guid workSpaceId, Guid userWorkspaceItemId)
        {
            try
            {
                var user = GetUserInfo();

                var workSpace = await _context.WorkSpaces.FirstOrDefaultAsync(x => x.Id == workSpaceId && x.OwnerId == userId);

                if (workSpace == null)
                {
                    throw new Exception("WorkSpace não encontrado");
                }

                if (await _context.UsersWorkSpaces.FirstOrDefaultAsync(x => x.Id == userWorkspaceItemId) == null)
                {
                    //Aplicar fluxo do tem user
                    throw new Exception("Conexão não encontrada");
                }

                var userWorker = await _context.UsersWorkSpaces.FirstOrDefaultAsync(x => x.Id == userWorkspaceItemId);

                _context.UsersWorkSpaces.Remove(userWorker);

                workSpace.UserCount -= 1;

                await _context.SaveChangesAsync();

                return new OkObjectResult("Usuário desatrelado com sucesso");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IActionResult> AcceptInvite(string userEmail, Guid workSpaceId)
        {
            try
            {
                var workSpace = await _context.UsersWorkSpaces.FirstOrDefaultAsync(x => x.UserEmail == userEmail && x.WorkSpaceId == workSpaceId);

                workSpace.InviteStatus = InviteStatus.Accept;

                await _context.SaveChangesAsync();

                return new CreatedResult();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IActionResult> DeclineInvite(string userEmail, Guid workSpaceId)
        {
            try
            {
                var workSpace = await _context.UsersWorkSpaces.FirstOrDefaultAsync(x => x.UserEmail == userEmail && x.WorkSpaceId == workSpaceId);

                workSpace.InviteStatus = InviteStatus.Declined;

                await _context.SaveChangesAsync();

                return new CreatedResult();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
