using EaseTrail.WebApp.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace EaseTrail.WebApp.Interfaces
{
    public interface IUtilsContext
    {
        public TonkenCredInfoDto GetUserInfo();
        public bool CanInvite(Guid workSpaceId);
    }
}
