using Castle.Core.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infraestructura.Core.DataBasesInfo
{
    public static class DataBaseConnectionStrings
    {
        private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();

        //public static void Initialize(IConfiguration configuration)
        //{
        //    var connectionStringsSection = configuration.Children; //configuration.GetValue("ConnectionStrings");
        //    foreach (var connection in connectionStringsSection)
        //    {
        //        var x = connection.Name;
        //    }
        //}

        public static void Initialize(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            foreach (var connection in connectionStringsSection.GetChildren()) 
            {
                _connections[connection.Key] = connection.Value;
            }
        }

        public static string ObtenerConexion(string connectionName)
        {
            if (_connections.TryGetValue(connectionName, out var connectionString))
            {
                return connectionString;
            }
            throw new ArgumentException($"La cadena de conexión '{connectionName}' no se encontró.");
        }
    }
}
