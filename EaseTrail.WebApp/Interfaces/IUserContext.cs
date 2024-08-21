using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Interfaces
{
    public interface IUserContext
    {
        public Task<List<User>> GetAll();
        public Task<User> GetById(string id);
        public Task<User> GetByEmail(string email);
        public Task<User> GetByUserName (string userName);
        public Task<IActionResult> CreateUser(CreateUser input);
        public Task<IActionResult> Login(Login input);
        public Task<IActionResult> DeleteUser(string id);
        public Task<IActionResult> UpdateUser(UpdateUser input, string id);
    }
}
