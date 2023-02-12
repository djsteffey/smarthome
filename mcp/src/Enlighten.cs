using mcp.datamodels;
using Microsoft.VisualBasic.FileIO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml;

namespace mcp
{
    class Enlighten
    {
        // variables
        private HttpClientHandler m_httpClientHandler;
        private HttpClient m_httpClient;
        private string m_username;
        private string m_password;
        private string m_serialNumber;
        private string m_softwareVersion;
        private string m_sessionId;
        private string m_authToken;

        // methods
        public Enlighten(string username, string password)
        {
            // init variables
            this.m_httpClientHandler = null;
            this.m_httpClient = null;
            this.m_username = username;
            this.m_password = password;
            this.m_serialNumber = "";
            this.m_softwareVersion = "";
            this.m_sessionId = "";
            this.m_authToken = "";
        }

        public async Task<SolarData> getSolarDataAsync()
        {
            // try to get solar data from envoy
            SolarData data = await this.querySolarDataFromEnvoy();

            // check
            if (data == null)
            {
                // connect
                if (await this.connect(5) == false)
                {
                    // failed to connect
                    return null;
                }

                // get data again
                data = await this.querySolarDataFromEnvoy();
            }

            // done
            return data;
        }

        private async Task<SolarData> querySolarDataFromEnvoy()
        {
            // check client
            if (this.m_httpClient== null)
            {
                return null;
            }

            // build the request
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "https://envoy/production.json");
            msg.Headers.Add("Cookie", "sessionId=" + this.m_sessionId);

            // send the request and await the response
            HttpResponseMessage response = await this.m_httpClient.SendAsync(msg);

            // check status code
            if (response.StatusCode != HttpStatusCode.OK)
            {
                // failed
                return null;
            }

            // parse response content into json object
            string content = await response.Content.ReadAsStringAsync();
            JsonObject node = JsonNode.Parse(content).AsObject();

            // build the data
            SolarData data = new SolarData(
                // timestamp
                node["consumption"].AsArray()[0].AsObject()["readingTime"].AsValue().GetValue<long>(),

                // production
                node["production"].AsArray()[1].AsObject()["wNow"].AsValue().GetValue<double>(),
                node["production"].AsArray()[1].AsObject()["whToday"].AsValue().GetValue<double>(),
                node["production"].AsArray()[1].AsObject()["whLastSevenDays"].AsValue().GetValue<double>(),
                node["production"].AsArray()[1].AsObject()["whLifetime"].AsValue().GetValue<double>(),

                // total consumption
                node["consumption"].AsArray()[0].AsObject()["wNow"].AsValue().GetValue<double>(),
                node["consumption"].AsArray()[0].AsObject()["whToday"].AsValue().GetValue<double>(),
                node["consumption"].AsArray()[0].AsObject()["whLastSevenDays"].AsValue().GetValue<double>(),
                node["consumption"].AsArray()[0].AsObject()["whLifetime"].AsValue().GetValue<double>(),

                // net consumption
                node["consumption"].AsArray()[1].AsObject()["wNow"].AsValue().GetValue<double>(),
                node["consumption"].AsArray()[1].AsObject()["whToday"].AsValue().GetValue<double>(),
                node["consumption"].AsArray()[1].AsObject()["whLastSevenDays"].AsValue().GetValue<double>(),
                node["consumption"].AsArray()[1].AsObject()["whLifetime"].AsValue().GetValue<double>()
            );

            // done
            return data;
        }

        private async Task<bool> connect(int maxAttempts)
        {
            Log.log("Enlighten", "connecting");

            // loop the max attempts
            for (int attempt = 0; attempt < maxAttempts; ++attempt)
            {
                // clear all current variables except credentials
                if (this.m_httpClientHandler != null)
                {
                    this.m_httpClientHandler.Dispose();
                    this.m_httpClientHandler = null;
                }
                if (this.m_httpClient != null)
                {
                    this.m_httpClient.Dispose();
                    this.m_httpClient = null;
                }
                this.m_serialNumber = "";
                this.m_softwareVersion = "";
                this.m_sessionId = "";
                this.m_authToken = "";

                // handler
                this.m_httpClientHandler = new HttpClientHandler();
                this.m_httpClientHandler.UseCookies = true;
                this.m_httpClientHandler.CookieContainer = new CookieContainer();
                this.m_httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                this.m_httpClientHandler.ServerCertificateCustomValidationCallback = this.serverCertificateValidator;

                // create the client
                this.m_httpClient = new HttpClient(this.m_httpClientHandler);

                // get the device info
                Log.log("Enlighten", "getting device info");
                if (await this.getDeviceInfo())
                {
                    Log.log("Enlighten", "getting device info success");
                }
                else
                {
                    Log.log("Enlighten", "getting device info failed");
                    continue;
                }

                // login
                Log.log("Enlighten", "login");
                if (await this.loginAsync())
                {
                    Log.log("Enlighten", "login success");
                }
                else
                {
                    Log.log("Enlighten", "login failed");
                    continue;
                }

                // get the auth token
                Log.log("Enlighten", "get authorization token");
                if (await this.getAuthToken())
                {
                    Log.log("Enlighten", "get authorization token success");
                }
                else
                {
                    Log.log("Enlighten", "get authorization token failed");
                    continue;
                }

                // done
                return true;
            }

            // failed after number of attempts
            return false;
        }

        private async Task<bool> getDeviceInfo()
        {
            // send the request
            var response = await this.m_httpClient.GetAsync("http://envoy/info.xml");

            // check status code
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            // parse response content into xml document
            string content = await response.Content.ReadAsStringAsync();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            // extract the serial number and software version
            XmlNode node = doc.GetElementsByTagName("device")[0];
            this.m_serialNumber = node.SelectSingleNode("sn").InnerText;
            this.m_softwareVersion = node.SelectSingleNode("software").InnerText;

            // done
            return true;
        }

        private async Task<bool> loginAsync()
        {
            // login parameters
            var parameters = new Dictionary<string, string>();
            parameters.Add("user[email]", this.m_username);
            parameters.Add("user[password]", this.m_password);

            // post
            var response = await this.m_httpClient.PostAsync("https://enlighten.enphaseenergy.com/login/login.json", new FormUrlEncodedContent(parameters));

            // check response
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            // parse response content into json object
            string content = await response.Content.ReadAsStringAsync();
            JsonObject node = JsonNode.Parse(content).AsObject();

            // check for success
            var val = node["message"].AsValue().GetValue<String>();
            if (val.Equals("success") == false)
            {
                return false;
            }

            // get session id
            this.m_sessionId = node["session_id"].AsValue().GetValue<string>();

            // check session id
            if (this.m_sessionId.Equals(""))
            {
                return false;
            }

            // done
            return true;
        }

        private async Task<bool> getAuthToken()
        {
            // parameters
            var parameters = new Dictionary<string, string>();
            parameters.Add("session_id", this.m_sessionId);
            parameters.Add("serial_num", this.m_serialNumber);
            parameters.Add("username", this.m_username);

            // post
            HttpResponseMessage response = await this.m_httpClient.PostAsJsonAsync("https://entrez.enphaseenergy.com/tokens", parameters);

            // check the response
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            // get the token
            this.m_authToken = await response.Content.ReadAsStringAsync();

            // exchange the token for a session id to the envoy
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, "https://envoy/auth/check_jwt");
            msg.Headers.Add("Authorization", "Bearer " + this.m_authToken);
            response = await this.m_httpClient.SendAsync(msg);

            // check the response
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            // get the session id from the cookies
            this.m_sessionId = "";
            foreach (Cookie cookie in this.m_httpClientHandler.CookieContainer.GetCookies(new Uri("https://envoy")))
            {
                if (cookie.Name.Equals("sessionId"))
                {
                    this.m_sessionId = cookie.Value;
                }
            }

            // make sure we got one
            if (this.m_sessionId.Equals(""))
            {
                return false;
            }

            // done
            return true;
        }

        private bool serverCertificateValidator(HttpRequestMessage requestMessage, X509Certificate2 cert, X509Chain certChain, SslPolicyErrors policyErrors)
        {
            // if no errors then it is valid
            if (policyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            // our local envoy has a self-signed cert
            if (requestMessage.RequestUri.Host == "envoy")
            {
                return true;
            }

            // otherwise bail
            return false;
        }
    }
}
