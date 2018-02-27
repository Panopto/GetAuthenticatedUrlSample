using System.Text;

/// <summary>
/// Sample code to generate authentication code for a user from external ID provider.
/// Thanks to Graham Robinson. 
/// reference: https://www.mediaguy.co.uk/panopto-api/panopto-api-004-authenticating-with-an-identity-provider/
/// </summary>
namespace GetAuthenticatedUrl
{
    static class AuthCode
    {
        /// <summary>
        /// Creates an auth code. Used when we want to authenticate a user, but don't know their password.
        /// </summary>
        /// <param name="identityProviderInstanceName">The instance name as set in Panopto > System > Identity Providors</param>
        /// <param name="username">Username as defined by Panopto</param>
        /// <param name="serverFqdn">The full server name as defined by Panopto > System > Settings > General site settings > Web server FQDN</param>
        /// <param name="applicationKey">The key produced through Panopto > System > Identity Providors</param>
        /// <returns></returns>
        public static string CreateAuthCode(string identityProviderInstanceName, string username, string serverFqdn, string applicationKey)
        {
            string payload = identityProviderInstanceName + "\\" + username + "@" + serverFqdn.ToLower() + "|" + applicationKey.ToLower();

            var data = Encoding.ASCII.GetBytes(payload);
            var hashData = new System.Security.Cryptography.SHA1Managed().ComputeHash(data);

            var hash = string.Empty;

            foreach (var b in hashData)
                hash += b.ToString("X2");

            return hash;
        }
    }
}
