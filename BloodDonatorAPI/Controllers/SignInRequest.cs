using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BloodDonatorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignInRequest : ControllerBase
    {
        
        [HttpPost]
        public IActionResult Post(string username, string password)
        {
            return Ok(GetDataFromServer(username, password));
        }
        public string GetDataFromServer(string user,string pass)
        {
            string database = "azure_sys";
            string password = "Hamzamohammed159";
            string ConnectionString = $"Server=blooddonatorserver.postgres.database.azure.com;Database={database};Port=5432;User Id=Elshennawy@blooddonatorserver;Password={password};";
            try
            {
                // request data from azure database using postgre

                //SqlConnection connection = new SqlConnection(ConnectionString);
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand($"SELECT \"UserName\",\"UPassword\" FROM \"Users\" WHERE \"UserName\" = '{user}' AND \"UPassword\" = '{pass}'", conn))
                    {
                        var result = command.ExecuteReader();
                        string stringToShow = "";
                        while (result.Read())
                        {
                            stringToShow += "Username: ";
                            stringToShow += result.GetString(0);
                            stringToShow += "\nPassword: ";
                            stringToShow += result.GetString(1);
                        }
                        return stringToShow;
                    }

                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
