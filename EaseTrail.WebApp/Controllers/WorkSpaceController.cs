using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkSpaceController : ControllerBase
    {
        private readonly IWorkSpaceContext _workSpaceContext;

        public WorkSpaceController(
            IWorkSpaceContext workSpaceContext
        )
        {
            _workSpaceContext = workSpaceContext;
        }
        #region Get

        [HttpGet("get")]
        [Authorize(Policy = "Restrict")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _workSpaceContext.GetAll());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(await _workSpaceContext.GetById(new Guid(id)));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/user")]
        [Authorize]
        public async Task<IActionResult> GetByUser()
        {
            try
            {
                return Ok(await _workSpaceContext.GetByUserId());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/user/{id}")]
        [Authorize(Policy = "Restrict")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            try
            {
                return Ok(await _workSpaceContext.GetByUserId(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Post

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateWorkSpace createWorkSpace)
        {
            try
            {
                return await _workSpaceContext.CreateWorkSpace(createWorkSpace);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Update

        [HttpPatch("update/{workSpaceId}")]
        public async Task<IActionResult> Update(UpdateWorkSpace input, string workSpaceId)
        {
            try
            {
                return await _workSpaceContext.UpdateWorkSpace(input, new Guid(workSpaceId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Delete

        [HttpDelete("delete/{workSpaceId}")]
        public async Task<IActionResult> Delete(string workSpaceId)
        {
            try
            {
                return await _workSpaceContext.DeleteWorkSpace(new Guid(workSpaceId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
