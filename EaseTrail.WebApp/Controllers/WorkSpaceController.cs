using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Interfaces;
using EaseTrail.WebApp.Services;
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

        #endregion

        #region Post

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateWorkSpace createWorkSpace)
        {
            try
            {
                await _workSpaceContext.CreateWorkSpace(createWorkSpace);
                return Created();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
