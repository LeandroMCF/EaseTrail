using EaseTrail.WebApp.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Interfaces
{
    public interface IUtilsContext
    {
        public TonkenCredInfoDto GetUserInfo();
        public Task<IActionResult> InviteUser(Guid userId, string addUserEmail, Guid workSpaceId, int colabType);
        public Task<IActionResult> RemoveUser(Guid userId, Guid workSpaceId, Guid userWorkspaceItemId);
        public Task<IActionResult> AcceptInvite(string userEmail, Guid workSpaceId);
        public Task<IActionResult> DeclineInvite(string userEmail, Guid workSpaceId);
    }
}
