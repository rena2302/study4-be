using study4_be.Interface;

namespace study4_be.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConfiguration configuration;

        public ConnectionService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private readonly string connectionString = "Data Source=LAPTOP-62MKG1UJ;Initial Catalog=STUDY4;Integrated Security=True;Trust Server Certificate=True";

        public string GetConnectionString()
        {
            return connectionString;
        }
        ////public string? Datebase => configuration.GetConnectionString("Database");
        public string? Datebase => connectionString;
     
    }
}
