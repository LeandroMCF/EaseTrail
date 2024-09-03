using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Interfaces
{
    public interface IWorkSpaceContext
    {
        public Task<List<WorkSpace>> GetAll();
        public Task<List<WorkSpace>> GetByUserId(string id = "");
        public Task<WorkSpace> GetById(Guid workSpaceId);
        public Task<IActionResult> CreateWorkSpace(CreateWorkSpace input);
        public Task<IActionResult> UpdateWorkSpace(UpdateWorkSpace input, Guid workSpaceId);
        public Task<IActionResult> DeleteWorkSpace(Guid workSpaceId);
    }
}
