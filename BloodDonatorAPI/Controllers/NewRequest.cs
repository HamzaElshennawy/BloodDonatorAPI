using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace BloodDonatorAPI.Controllers {
    [Route("API/[controller]")]
    [ApiController]
    public class NewRequest : ControllerBase {
        [HttpPost]
        public IActionResult Post(JsonObject request) {
            bool done = MakeNewRequest(request);
            return Ok(done);
        }

        private bool MakeNewRequest(JsonObject request) {
            
            
            return true;
        }
    }
}
