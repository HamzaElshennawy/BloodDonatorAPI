using Azure.Core;
using Newtonsoft.Json;
using System.Drawing;

namespace BloodDonatorAPI.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("PhoneNumber")]
        public long PhoneNumber { get; set; }

        [JsonProperty("Age")]
        public int Age { get; set; }

        [JsonProperty("Birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("request")]
        public Request request { get; set; }

        [JsonProperty("NationalID_Image")]
        public Image NationalID_Image { get; set; }

        [JsonProperty("NationalID")]
        public long NationalID { get; set; }

        [JsonProperty("PersonalPhoto")]
        public Image PersonalPhoto { get; set; }

        [JsonProperty("UserType")]
        public int UserType { get; set; }

        [JsonProperty("RequestHistory")]
        public List<Request> RequestHistory { get; set; }
    }
}
