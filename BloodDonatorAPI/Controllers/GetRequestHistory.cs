using Azure.Core;
using BloodDonatorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;

namespace BloodDonatorAPI.Controllers {
    [Route("API/[controller]")]
    [ApiController]
    public class GetRequestHistory : ControllerBase {
        [HttpGet] 
        public ActionResult GetHistory(string id) {
            var Requests = RequestHistory(id).Result;
            return Ok(Requests);
        }
        public async Task<string> RequestHistory(string id) {
            string database = "azure_sys";
            string password = "Hamzamohammed159";
            string ConnectionString = $"Server=blooddonatorserver.postgres.database.azure.com;Database={database};Port=5432;User Id=Elshennawy@blooddonatorserver;Password={password};Ssl Mode=Allow;";
            List<Requests> TempRequests = new List<Requests>();
            using (var conn = new NpgsqlConnection(ConnectionString)) {
                conn.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM \"Requests\" WHERE \"UserID\" = '{id}'", conn)) {
                    var result = await command.ExecuteReaderAsync();
                    while (result.Read()) {
                        TempRequests.Add(new Requests() {
                            RequestID = result.GetString(0),
                            RequestType = result.GetInt32(2),
                            RequestBody = result.GetString(3),
                            RequestDate = result.GetString(4),
                            Status = result.GetString(5)
                        });
                    }
                }
            }
            var RequestsList = JsonConvert.SerializeObject(TempRequests);

            return RequestsList;
        }
    }
}
