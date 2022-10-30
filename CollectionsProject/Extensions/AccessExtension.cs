using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Extensions
{
    public static class AccessExtension
    {
        public static bool HasAccess(this ControllerBase cb, string name)
        {
            return cb.User.IsInRole("Admin") || cb.User.Identity!.Name == name;
        }
    }
}
