using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using tec_site.Data;
using tec_site.Models;

namespace tec_site.Models
{
    public class ApplicationUser : IdentityUser
    {
    }

    public class Club
    {
        public int Id { get; set; }
    }
    // public class ApplicationRoleClaim : IdentityRoleClaim<int> { }
    public class ApplicationUserClaim : Microsoft.AspNetCore.Identity.IdentityUserClaim<int> { }
    public class ApplicationUserLogin : Microsoft.AspNetCore.Identity.IdentityUserLogin<int> { }
    public class ApplicationUserToken : IdentityUserToken<int> { }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger, IHttpContextAccessor contextAccessor) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }

    public class ApplicationRoleStore<ApplicationRole, tecContext, ApplicationInt, ApplicationUserRole, ApplicationRoleClaim> : RoleStore<ApplicationRole, tec_siteContext, int, Models.ApplicationUserRole, Models.ApplicationRoleClaim>
        where ApplicationRole : IdentityRole<int>
        where tecContext : tec_siteContext
        where ApplicationUserRole : Models.ApplicationUserRole
        where ApplicationRoleClaim : Models.ApplicationRoleClaim
    {
        public ApplicationRoleStore(tec_siteContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        protected override ApplicationRoleClaim CreateRoleClaim(ApplicationRole role, Claim claim)
        {
            throw new NotImplementedException();
        }
    }
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
    public class ApplicationUserRole : Microsoft.AspNetCore.Identity.IdentityUserRole<int>
    {
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
    }
    public class ApplicationRole
    {
    }
    public class ApplicationRoleClaim : IdentityRoleClaim<int> { }
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(DbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}