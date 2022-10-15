using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace CollectionsProject.Hash
{
    public static class MyGuid
    {
        public static string GetGuid()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        }
    }
}
