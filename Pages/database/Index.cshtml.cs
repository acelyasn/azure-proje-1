using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace azureproje.Pages.database
{
    public class IndexModel : PageModel
    {
        public List<Country> countries = new List<Country>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=net-speed-server.database.windows.net;Initial Catalog=net-speed-database;User ID=net-speed-server-admin;Password=AQazure94;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM [Internet Speed 2022]";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Country country= new Country();
                                country.countryName = reader.GetStringOrNull(0);
                              
                                country.band = "" + reader.GetDoubleOrNull(1);
                                country.mobile = "" + reader.GetDoubleOrNull(2);

                                countries.Add(country);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                
            }
        }
    }
    public static class DataReaderExtensions
    {
        public static string GetStringOrNull(this IDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        public static string GetStringOrNull(this IDataReader reader, string columnName)
        {
            return reader.GetStringOrNull(reader.GetOrdinal(columnName));
        }

        public static string GetDoubleOrNull(this IDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : "" + reader.GetDouble(ordinal);

        }

        public static string GetDoubleOrNull(this IDataReader reader, string columnName)
        {
            return reader.GetDoubleOrNull(reader.GetOrdinal(columnName));
        }

    }

    public class Country
    {
        public String countryName;
        public String band;
        public String mobile;
    }
}
