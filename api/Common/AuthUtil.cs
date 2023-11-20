using Microsoft.AspNetCore.Identity;

namespace api.Common
{
    public class AuthUtil
    {
        public static string GetFirstErrorFromIdentityResult(IdentityResult result, string methodName)
        {
            var firstError = result.Errors.FirstOrDefault();
            if (firstError != null)
                return firstError.Description;
            else
                return methodName + " method failed";
        }
    }
}
