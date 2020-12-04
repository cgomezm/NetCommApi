using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NetCommApi.AuthorizationHandlers
{
    public class MinAgeRequirement : IAuthorizationRequirement
    {
        public int MinAge { get; } 

        public MinAgeRequirement(int minAge)
        {
            MinAge = minAge;
        }
    }


    public class MinAgeHandler : AuthorizationHandler<MinAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            var dateOfBirth = DateTime.ParseExact(context.User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.DateOfBirth).Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (requirement.MinAge < DateTime.Now.Year - dateOfBirth.Year)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
