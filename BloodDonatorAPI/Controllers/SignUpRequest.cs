using BloodDonatorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace BloodDonatorAPI.Controllers {
    [Route("API/[controller]")]
    [ApiController]
    public class SignUpRequest : ControllerBase {
        [HttpPost]
        public IActionResult Post(JsonObject TempUser) {
            bool done = PostDataToServer(TempUser).Result;
            return Ok(done);
        }
        public async Task<bool> PostDataToServer(JsonObject user) {
            User TempUser = new User();
            string database = "azure_sys";
            string password = "Hamzamohammed159";
            string ConnectionString = $"Server=blooddonatorserver.postgres.database.azure.com;Database={database};Port=5432;User Id=Elshennawy@blooddonatorserver;Password={password};";
            try {
                var user1 = JsonConvert.DeserializeObject<User>(user.ToString());
                if (user1 != null) {
                    TempUser = user1;
                }
                else {
                    return false;
                }
                using (var conn = new NpgsqlConnection(ConnectionString)) {
                    conn.Open();
                    using (var command = new NpgsqlCommand($"SELECT \"UserID\" FROM \"Users\"", conn)) {
                        var result = command.ExecuteReader();
                        while (result.Read()) {
                            var id = result.GetString(0);
                            if (TempUser.id == id) {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception) {
                return false;
            }
            using (var conn = new NpgsqlConnection(ConnectionString)) {
                conn.Open();
                using (var command = new NpgsqlCommand($"INSERT INTO \"Users\" VALUES (\'{TempUser.id}\',\'{TempUser.UserName}\',\'{TempUser.Password}\',\'{TempUser.FirstName}\',\'{TempUser.LastName}\',\'{TempUser.Address}\',\'{TempUser.City}\',\'{TempUser.Region}\',{TempUser.PhoneNumber},{TempUser.Age},\'{TempUser.Birthdate}\','{TempUser.NationalID}','{TempUser.UserType}')", conn)) {
                    var AddUser =await command.ExecuteNonQueryAsync();
                    if (AddUser < 1)
                        return false;
                }
            }
            return true;
        }
    }
}
