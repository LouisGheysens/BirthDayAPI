using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Birthday.Base
{
    public class BirthdayBaseController : ControllerBase
    {
        public BirthdayBaseController()
        {
        }

        internal static readonly string[] ScopeRequiredByApi = { "access_as_user", "user.read" };

        public IWebHostEnvironment Host { get; internal set; }

        public ServiceResponse<object> ServiceResponse { get; set; }
        public BirthdayBaseController(
            IWebHostEnvironment host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        internal string CurrentUserNameIdentifier => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public CancellationToken Token;

        public int CurrentAccountId
        {
            get
            {
                return int.Parse(User.Claims.First(i => i.Type == ClaimTypes.GroupSid).Value);
            }
        }


        public int CurrentUserId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(User?.Identity?.Name))
                    return 0;
                return int.Parse(User.Identity.Name);
            }
        }

        internal int CurrentRoleId
        {
            get
            {
                var roleId = 0;

                var userRoleClaim = User.Claims.FirstOrDefault(m => m.Type == "roleId");

                if (userRoleClaim != null)
                {
                    int.TryParse(userRoleClaim.Value, out roleId);
                }

                return roleId;
            }

        }
    }
}
