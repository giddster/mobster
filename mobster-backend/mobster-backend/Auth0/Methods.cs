using RestSharp;
using System.Security.Claims;
using System.Text.Json;

namespace mobster_backend.Auth0
{
    public static class Methods
    {
        //public async Task<IActionResult> TestAuthToken([FromHeader(Name = "Token")] string Token)
        //{

        //    var client = new RestClient("https://outlaw-forum.eu.auth0.com/api/v2/users");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("authorization", "Bearer " + Token);
        //    IRestResponse response = client.Execute(request);

        //    return Ok(response);
        //}

        /// <summary>
        /// Our token variable where we put our bearer token on startup. 
        /// </summary>
        private static string Token { get; set; }
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        };
        

        /// <summary>
        /// Gets a bearer token from Auth0 and puts it into our Token variable. 
        /// </summary>
        public static void GetBearerToken()
        {
            var client = new RestClient("https://outlaw-forum.eu.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"jE4u4p4EWv9iKDqz9Duum2OmTVrfvgNd\",\"client_secret\":\"EX0Bcd30bIpT-ozPnKTqD_tH7UDV38IW_ug0mZDj2wIfsyPICrbOGybDdli8ycFw\",\"audience\":\"https://outlaw-forum.eu.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            string token = response.Content.Substring(17);
            token = token.Substring(0, token.IndexOf("\""));
            Token = token;

        }

        //https://outlaw-forum.eu.auth0.com/api/v2/

        public static void ToggleUserBlock(string userId, bool UserBanned)
        {
            var urlEncoded = System.Web.HttpUtility.UrlEncode(userId);
            RestClient client = new RestClient($"https://outlaw-forum.eu.auth0.com/api/v2/users/" + urlEncoded);
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("authorization", $"Bearer {Token}");
            var auth0User = new Auth0.Objects.Auth0User() { blocked = UserBanned };
            string jsonString = JsonSerializer.Serialize(auth0User, options: jsonSerializerOptions);
            request.AddJsonBody(jsonString);
            IRestResponse response = client.Execute(request);
        }

        public static IRestResponse ChangeEmail(string userId, string email)
        {
            var urlEncoded = System.Web.HttpUtility.UrlEncode(userId);
            RestClient client = new RestClient($"https://outlaw-forum.eu.auth0.com/api/v2/users/" + urlEncoded);
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("authorization", $"Bearer {Token}");
            var auth0User = new Auth0.Objects.Auth0User() { email = email};
            string jsonString = JsonSerializer.Serialize(auth0User, options: jsonSerializerOptions);
            request.AddJsonBody(jsonString);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse CreateChangePasswordTicket(string userId)
        {
            RestClient client = new RestClient($"https://outlaw-forum.eu.auth0.com/api/v2/tickets/password-change");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", $"Bearer {Token}");
            var passwordTicket = new Auth0.Objects.PasswordTicket() { user_id = userId };
            string jsonString = JsonSerializer.Serialize(passwordTicket, options: jsonSerializerOptions);
            request.AddJsonBody(jsonString);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse AssignGroupAdminRole(string userId)
        {
            Auth0.Objects.Permission permission = new Auth0.Objects.Permission() { permission_name = "groupAdmin:access" };
            Auth0.Objects.PermissionsList permissionList = new Auth0.Objects.PermissionsList();
            permissionList.permissions = new System.Collections.Generic.List<Objects.Permission>();
            permissionList.permissions.Add(permission);
            RestClient client = new RestClient($"https://outlaw-forum.eu.auth0.com/api/v2/users/{userId}/permissions");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", $"Bearer {Token}");
            string jsonString = JsonSerializer.Serialize(permissionList, options: jsonSerializerOptions);
            request.AddJsonBody(jsonString);
            IRestResponse response = client.Execute(request);
            return response;
        }

    }
}
