// This sample code demonstrates how to call GetAuthenticatedUrl method
// with a user from external ID provider.

using System;
using System.ServiceModel;

namespace GetAuthenticatedUrl
{
    class Program
    {
        private static readonly string serverName = "scratch.hosted.panopto.com";
        private static readonly string instanceName = "canvas";
        private static readonly string applicationKey = "7bxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxa8";
        private static readonly string userName = "user";

        static void Main(string[] args)
        {
            string apiEndPoint = "https://" + serverName + "/Panopto/PublicAPI/4.6/Auth.svc";

            var api = new AuthService.AuthClient(
                new BasicHttpBinding(BasicHttpSecurityMode.Transport)
                {
                    MaxReceivedMessageSize = 10000000,
                    SendTimeout = TimeSpan.FromMinutes(1),
                    ReceiveTimeout = TimeSpan.FromMinutes(1)
                },
                new EndpointAddress(apiEndPoint));

            var authInfo = new AuthService.AuthenticationInfo()
            {
                UserKey = instanceName + "\\" + userName,
                AuthCode = AuthCode.CreateAuthCode(instanceName, userName, serverName, applicationKey)
            };

            // This is an example URL. URL format may be changed in the future release of Panopto.
            string targetURL = "https://" + serverName + "/Panopto/Pages/Sessions/List.aspx?folderID=5804a465-7a05-4ae6-bc2d-c19abec5ee35";

            string result = api.GetAuthenticatedUrl(authInfo, targetURL);
            Console.WriteLine("Result URL:");
            Console.WriteLine(result);

            System.Threading.Thread.Sleep(30000);
        }
    }
}
