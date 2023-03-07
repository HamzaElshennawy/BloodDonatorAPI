using BloodDonatorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Text.Json.Nodes;

namespace BloodDonatorAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class NewRequest : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(JsonObject request)
        {
            bool done = MakeNewRequest(request).Result;
            return Ok(done);
        }

        private async Task<bool> MakeNewRequest(JsonObject request)
        {
            Requests? newRequest;
            string database = "azure_sys";
            string password = "Hamzamohammed159";
            string ConnectionString = $"Server=blooddonatorserver.postgres.database.azure.com;Database={database};Port=5432;User Id=Elshennawy@blooddonatorserver;Password={password};";
            if (request == null) { return false; }
            else
            {
                try
                {
                    newRequest = JsonConvert.DeserializeObject<Requests>(request.ToJsonString());
                    using (var conn = new NpgsqlConnection(ConnectionString))
                    {
                        conn.Open();
                        using (var command = new NpgsqlCommand($"INSERT INTO \"Requests\" VALUES (\'{newRequest.RequestID}\','{newRequest.UserID}','{newRequest.RequestType}','{newRequest.RequestBody}','{newRequest.RequestDate}','{newRequest.Status}')", conn))
                        {
                            var result =await command.ExecuteNonQueryAsync();
                            
                        }
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
