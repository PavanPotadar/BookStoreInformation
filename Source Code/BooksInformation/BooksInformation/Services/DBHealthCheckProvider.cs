using System;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BooksInformation.Services
{
    public static class DBHealthCheckProvider
    { 

        public static HealthCheckResult Check(string CoonnectionString)
        {
            using (var l_oConnection = new SqlConnection(CoonnectionString))
            {
                try
                {
                    l_oConnection.Open();
                    return HealthCheckResult.Healthy();
                }
                catch (SqlException)
                {
                    return HealthCheckResult.Unhealthy();
                }
                finally
                {
                    l_oConnection.Close();
                }
            }
        }
    }
}

