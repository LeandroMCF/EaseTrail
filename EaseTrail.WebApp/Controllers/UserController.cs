using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserContext _userContext;

        public UserController(IUserContext userContext, IHttpContextAccessor httpContextAccessor)
        {
            _userContext = userContext;
        }

        #region[HttpGet]

        [HttpGet("get")]
        [Authorize(Policy = "Restrict")]
        public async Task<IActionResult> getAll() 
        {
            try
            {
                return Ok(await _userContext.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/id/{id}")]
        [Authorize]
        public async Task<IActionResult> getId(string id)
        {
            try
            {
                return Ok(await _userContext.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/email/{email}")]
        [Authorize]
        public async Task<IActionResult> getEmail(string email)
        {
            try
            {
                return Ok(await _userContext.GetByEmail(email));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/username/{userName}")]
        [Authorize]
        public async Task<IActionResult> getUserName(string userName)
        {
            try
            {
                return Ok(await _userContext.GetByUserName(userName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region [HttpPost]

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateUser createUser)
        {
            try
            {
                await _userContext.CreateUser(createUser);
                return Created();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login input)
        {
            try
            {
                return await _userContext.Login(input);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion

        #region [HttpDelete]

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return Ok(await _userContext.DeleteUser(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion

        #region [HttpPut] / [HttpPath]

        [HttpPatch("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUser input, string id)
        {
            try
            {
                return await _userContext.UpdateUser(input, id);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
