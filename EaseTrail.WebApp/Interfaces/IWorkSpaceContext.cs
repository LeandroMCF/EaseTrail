using EaseTrail.WebApp.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Interfaces
{
    public interface IWorkSpaceContext
    {
        public Task<IActionResult> CreateWorkSpace(CreateWorkSpace input);
    }
}
