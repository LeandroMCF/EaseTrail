using EaseTrail.WebApp.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Interfaces
{
    public interface IUtilsContext
    {
        public TonkenCredInfoDto GetUserInfo();
        public Task<IActionResult> InviteUser(Guid userId, Guid workSpaceId, int colabType);
        public Task<IActionResult> RemoveUser(Guid userId, Guid workSpaceId);
        public Task<IActionResult> AcceptInvite(Guid userId, Guid workSpaceId);
        public Task<IActionResult> DeclineInvite(Guid userId, Guid workSpaceId);
    }
}
