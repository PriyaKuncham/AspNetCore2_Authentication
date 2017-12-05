using Microsoft.AspNetCore.Authorization;

namespace WEBAPPSample.Security.Authorization.Auth
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public AgeRequirement(int age)
        {
            this.Age = age;
        }

        public int Age { get; }
    }
}
