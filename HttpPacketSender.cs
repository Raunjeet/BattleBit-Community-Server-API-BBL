using BattleBitAPI;
using BattleBitAPI.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace CommunityServerAPI
{
    internal class HttpPacketSender
    {

        //TODO: Need to create a list of player stats where, PlayerStats contains steam64
        public static async Task sendRoundStatsToWebserverOnRoundEnd(string auth0Domain, string clientId, string clientSecret, List<PlayerStats> playerStatsList, string endPointDomain)
        {

            string jsonData = JsonSerializer.Serialize(playerStatsList);


            // Step 1: Obtain the Access Token
            string accessToken = await GetAuth0AccessToken(auth0Domain, clientId, clientSecret);

           

            using (var httpClient = new HttpClient())
            {

            // Step 2: Create the Tokenized JSON Data
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            // Step 3: Send the Tokenized JSON Data to the Web Server
                var response = await httpClient.PostAsync(endPointDomain, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("JSON data sent successfully.");
                }
                else
                {
                    Console.WriteLine("Error sending JSON data: " + response.ReasonPhrase);
                }
            }
        }

        public static async Task<string> GetAuth0AccessToken(string auth0Domain, string clientId, string clientSecret)
        {
            using var httpClient = new HttpClient();

            var tokenRequest = new
            {
                client_id = clientId,
                client_secret = clientSecret,
                audience = $"https://{auth0Domain}/api/v2/",
                grant_type = "client_credentials"
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(tokenRequest), Encoding.UTF8, "application/json");  
            var response = await httpClient.PostAsync($"https://{auth0Domain}/oauth/token", content);
            var tokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            return tokenResponse.access_token;
        }

        public static async Task SendDiscordMessage(string webhookUrl, string message, string user)
        {
            using var httpClient = new HttpClient();

            var discordPayload = new
            {
                username = user,
                content = message
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(discordPayload), Encoding.UTF8, "application/json");
            await httpClient.PostAsync(webhookUrl, content);
        }
#if DEBUG
        public static async Task sendPostDataTest(string fakePayloadString, string endpointUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var fakePayload = new
                {
                    data = fakePayloadString,
                    content = fakePayloadString
                };
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(fakePayload), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(endpointUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("JSON data sent successfully.");
                }
                else
                {
                    Console.WriteLine("Error sending JSON data: " + response.ReasonPhrase);
                }
            }
        }

#endif
#if DEBUG
        public static async Task sendPostDataTestWith0Auth(string auth0Domain, string clientId, string clientSecret, string fakePayload, string endpointUrl)
        {

            string jsonData = JsonSerializer.Serialize(fakePayload);


            // Step 1: Obtain the Access Token
            string accessToken = await GetAuth0AccessToken(auth0Domain, clientId, clientSecret);

            // Step 2: Create the Tokenized JSON Data


            // Step 3: Send the Tokenized JSON Data to the Web Server
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(endpointUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("JSON data sent successfully.");
                }
                else
                {
                    Console.WriteLine("Error sending JSON data: " + response.ReasonPhrase);
                }
            }
        }
#endif
    }
}
