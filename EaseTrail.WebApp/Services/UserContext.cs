#pragma warning disable CA2200 // Rethrow to preserve stack details
using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Models;
using EaseTrail.WebApp.Models.Enums;
using EaseTrail.WebApp.Outputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EaseTrail.WebApp.Services
{
    public class UserContext : IUserContext
    {
        private readonly Context _context;
        private readonly SymmetricSecurityKey _signingKey;

        private readonly IUtilsContext _utilsContext;

        public UserContext(
            Context context 
            ,IConfiguration configuration
            ,IUtilsContext utilsContext
        )
        {
            var secretKey = configuration["JwtSettings:SecretKey"];
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            _context = context;
            _utilsContext = utilsContext;
        }

        public async Task<IActionResult> CreateUser(CreateUser input)
        {
            try
            {
                if (EmailExist(input.Email))
                {
                    throw new Exception("Email já existente");
                }
                if (DocumentExist(input.DocumentId))
                {
                    throw new Exception("Documento já existente");
                }
                if (UserNameExist(input.UserName))
                {
                    throw new Exception("Nome de usuário já existente");
                }

                User newUser = new User(input.UserName, input.Name, input.SecondName, input.Email, RegisterUser(input.Password), input.DocumentId, (UserType)Enum.ToObject(typeof(UserType), input.UserType));

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return new CreatedResult();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await GetById(id);

                if (!ViewToken(user))
                {
                    throw new Exception("Não autorizado.");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return new OkObjectResult("Usuário deletado com sucesso");
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                /*
                 * Utilizar DTOs:
                public class UserDto
                {
                    public Guid Id { get; set; }
                    public string UserName { get; set; }
                    public List<WorkSpaceDto> WorkSpaces { get; set; }
                }

                public class WorkSpaceDto
                {
                    public string Name { get; set; }
                    public string Description { get; set; }
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    WorkSpaces = user.WorkSpaces.Select(ws => new WorkSpaceDto
                    {
                        Name = ws.Name,
                        Description = ws.Description
                    }).ToList()
                };

                Dessa forma é possivel manipular os dados sem ciclos infinitos

                PS: Criar na pasta output
                */

                var user = await _context.Users.Include(x => x.WorkSpaces).FirstOrDefaultAsync(x => x.Id == new Guid(id));

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                else 
                { 
                    return user; 
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<User> GetByUserName(string userName)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IActionResult> Login(Login input)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == input.Email_UserName || u.UserName == input.Email_UserName);

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                else
                {
                    if (!AuthenticateUser(user.Password, input.Password))
                    {
                        throw new Exception("Usuário ou senha estão errados.");
                    }
                    else if (user.Status == Status.Inactive || user.Status == Status.Baned)
                    {
                        throw new Exception("Acesso negado. Entre em contato com a adminstração.");
                    }
                }

                UserToken token = new UserToken();

                token.Token = GenerateToken(user);

                return new OkObjectResult(token);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IActionResult> UpdateUser(UpdateUser input, string id)
        {
            try
            {
                var user = await GetById(id);

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado");
                }
                if (!ViewToken(user))
                {
                    throw new Exception("Não autorizado");
                }

                UpdateUser(user, input);

                _context.SaveChanges();

                return new OkObjectResult("Usuário atualizado com sucesso!");
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        #region Private Methods
        private bool EmailExist(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool DocumentExist(string document)
        {
            var user = _context.Users.FirstOrDefault(x => x.DocumentId == document);

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool UserNameExist(string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string RegisterUser(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        private bool AuthenticateUser(string hashedPasswordFromDb, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDb);
        }

        private void UpdateUser(User user, UpdateUser input)
        {
            if (!string.IsNullOrEmpty(input.UserName))
                user.UserName = input.UserName;

            if (!string.IsNullOrEmpty(input.Name))
                user.Name = input.Name;

            if (!string.IsNullOrEmpty(input.SecondName))
                user.SecondName = input.SecondName;

            if (!string.IsNullOrEmpty(input.Email))
                user.Email = input.Email;

            if (!string.IsNullOrEmpty(input.Password))
                user.Password = input.Password;

            if (input.DocumentId != default)
                user.DocumentId = input.DocumentId;

            if (input.Status != default)
                user.Status = (Status)input.Status;

            if (input.UserType != default)
                user.UserType = (UserType)input.UserType;
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, ((int)user.UserType).ToString()),
                    new Claim("status", ((int)user.Status).ToString()),
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool ViewToken(User otherUser)
        {
            try
            {
                var tonkenCredInfo = _utilsContext.GetUserInfo();

                if (tonkenCredInfo.Id == otherUser.Id.ToString())
                {
                    return true;
                }
                else if (tonkenCredInfo.UserType == 0 || tonkenCredInfo.UserType == 1)
                {
                    return true;
                }
                else if (tonkenCredInfo.UserType == 2 && (int)otherUser.UserType == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
#pragma warning restore CA2200 // Rethrow to preserve stack details
