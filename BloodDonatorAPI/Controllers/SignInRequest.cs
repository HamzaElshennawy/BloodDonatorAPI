using BloodDonatorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Npgsql;

namespace BloodDonatorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignInRequest : ControllerBase
    {
        User MainUser { get; set; }

        [HttpPost]
        public IActionResult Post(string username, string password)
        {
            return Ok(GetDataFromServer(username, password));
        }
        public string GetDataFromServer(string user,string pass)
        {
            MainUser = new();
            string database = "azure_sys";
            string password = "Hamzamohammed159";
            string ConnectionString = $"Server=blooddonatorserver.postgres.database.azure.com;Database={database};Port=5432;User Id=Elshennawy@blooddonatorserver;Password={password};";
            try
            {

                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand($"SELECT * FROM \"Users\" WHERE \"UserName\" = '{user}' AND \"UPassword\" = '{pass}'", conn))
                    {
                        var result = command.ExecuteReader();
                        while (result.Read())
                        {
                            MainUser.id = result.GetString(0);
                            MainUser.UserName = result.GetString(1);
                            MainUser.Password = result.GetString(2);
                            MainUser.FirstName = result.GetString(3);
                            MainUser.LastName = result.GetString(4);
                            MainUser.Address = result.GetString(5);
                            MainUser.City = result.GetString(6);
                            MainUser.Region = result.GetString(7);
                            MainUser.PhoneNumber = (int)result.GetInt64(8);
                            MainUser.Age = result.GetInt32(9);
                            MainUser.Birthdate = result.GetString(10);
                            MainUser.NationalID = (int)result.GetInt64(11);
                            MainUser.UserType = result.GetInt16(12);
                        }
                    }
                }
                var UserToJson = JsonConvert.SerializeObject(MainUser);
                return UserToJson;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
