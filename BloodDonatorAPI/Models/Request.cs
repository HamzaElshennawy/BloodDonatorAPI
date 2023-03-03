using Newtonsoft.Json;

namespace BloodDonatorAPI.Models {
    [JsonObject("Request")]
    public class Requests {
        [JsonProperty("RequestID")]
        public string RequestID { get; set; }

        [JsonProperty("RequestType")]
        public int RequestType { get; set; }

        [JsonProperty("RequestBody")]
        public string RequestBody { get; set; }

        [JsonProperty("RequestDate")]
        public string RequestDate { get; set; }

        [JsonProperty("Name")]
        public string UserID { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}
