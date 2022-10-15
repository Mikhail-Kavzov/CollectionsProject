using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CollectionsProject.Models.UserModels
{
    public class User : IdentityUser
    {
        public Status Status { get; set; }
        public Role Role { get; set; }

        public List<Collection>? Collections { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<UserComment>? UserComments { get; set; }
    }
}
