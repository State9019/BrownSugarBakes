using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrownSugarBakes.UI.MVC
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;


        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "AX-QOReonFey-DMx8AjRAQD_9F0I6lZcxVRfd3lndYDPDUiP7KmBT3KuNk0LRrJ7PGXS2lDLLm6XHTvE";
            clientSecret = "EF-0uJRq4Y-GKFFlKcUdMYes9Cp9u_KUO1owuHDPeajAQ1jdyVVK5BZljQh5BTHg9a_zseOEAiCeMXdM";
        }

        private static Dictionary<string, string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }
    }
}