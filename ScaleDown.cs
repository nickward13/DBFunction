using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace Hectagon.Function
{
    public static class ScaleDown
    {
        [FunctionName("ScaleDown")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string sqlConnectionString = Environment.GetEnvironmentVariable("SQLCONN");
            string dbName = Environment.GetEnvironmentVariable("DBNAME");

            string sqlQuery = $"ALTER DATABASE {dbName} MODIFY (SERVICE_OBJECTIVE = 'GP_Gen5_2');";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Connection.Open();
                await sqlCommand.ExecuteNonQueryAsync();
            }
                        
            return (ActionResult)new OkResult();
        }
        
    }
}
