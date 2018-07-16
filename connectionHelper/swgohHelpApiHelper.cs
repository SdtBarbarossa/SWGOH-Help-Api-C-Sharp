using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace swgoh_help_api
{
    public class swgohHelpApiHelper
    {
        string user = "";
        string token = "";
        string url = "";
        string signin = "";
        string data = "";
        string player = "";
        string guild = "";
        Dictionary<string, string> userAsDictonary;

        private HttpClient client = new HttpClient();

        public swgohHelpApiHelper(UserSettings settings)
        {

            user = "username=" + settings.username;
            user += "&password=" + settings.password;
            user += "&grant_type=password";
            user += "&client_id=" + settings.client_id;
            user += "&client_secret=" + settings.client_secret;

            //user = "{ \"username\" : " + "\"" + settings.username + "\"" + ",";
            //user += "\"password\" : " + "\"" + settings.password + "\"" + ",";
            //user += "\"grant_type\" : " + "\"" + "\"password\"" + "\"" + ",";
            //user += "\"client_id\" : " + "\"" + settings.client_id + "\"" + ",";
            //user += "\"client_secret\" : " + "\"" + settings.client_secret + "\"" + " }";

            this.token = null;

            string protocol = string.IsNullOrEmpty(settings.protocol) ? "https" : settings.protocol;
            string host = string.IsNullOrEmpty(settings.host) ? "api.swgoh.help" : settings.host;
            string port = string.IsNullOrEmpty(settings.port) ? "" : settings.port;

            this.url = protocol + "://" + host + port;
            this.signin = this.url + "/auth/signin/";
            this.data = this.url + "/swgoh/data/";
            this.player = this.url + "/swgoh/player/";
            this.guild = this.url + "/swgoh/guild/";

        }

        public void login()
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.signin);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] byteArray = Encoding.UTF8.GetBytes(user);
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var loginresponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                var loginResponseObject = new JavaScriptSerializer().Deserialize<LoginResponse>(loginresponse);
                token = loginResponseObject.access_token;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public string fetchApi(string url, string param)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + param);
                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + token + "");
                byte[] byteArray = Encoding.UTF8.GetBytes("");

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var apiResponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return apiResponse;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string fetchData(string parameters)
        {
            var response = this.fetchApi(this.data, parameters);
            return response;
        }

        public Player fetchPlayer(int allycode)
        {
            var response = this.fetchApi(this.player, allycode.ToString());
            var playerObject = Player.FromJson(response);
            return playerObject;
        }

        public List<Player> fetchGuild(int allycode)
        {
            var response = this.fetchApi(this.guild, allycode.ToString());
            var guildObject = Player.GuildFromJson(response);
            return guildObject;
        }

    }

}