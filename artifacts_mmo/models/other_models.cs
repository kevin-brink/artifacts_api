using ArtifactsAPI.Schemas;
using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Models
{
    public class StatusResponse
    {
        public StatusCode status_code { get; private set; }
        public HttpResponseMessage response { get; private set; }

        public Status status { get; private set; }

        public StatusResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content);

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            status = new Status(json);
        }
    }
}
