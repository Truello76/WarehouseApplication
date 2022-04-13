using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace WebApplication2.Areas.Identity.Data
{
    public class UserValidator : IIdentityValidator<WebApplication2User>
    {
        public Task<IdentityResult> ValidateAsync(WebApplication2User item)
        {
            throw new System.NotImplementedException();
        }
    }
}
