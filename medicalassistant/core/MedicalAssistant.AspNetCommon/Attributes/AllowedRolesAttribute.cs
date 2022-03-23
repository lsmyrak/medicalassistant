using Microsoft.AspNetCore.Authorization;

namespace MedicalAssistant.AspNetCommon.Attributes
{
    public class AllowedRolesAttribute : AuthorizeAttribute
    {
        public AllowedRolesAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
