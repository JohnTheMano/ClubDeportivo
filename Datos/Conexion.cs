using MySql.Data.MySqlClient;

namespace ClubDeportivo.Datos
{
    public class Conexion
    {
        // Cadena de conexión (ajustada a tu configuración actual)
        private string cadenaConexion =
            "server=localhost; user=root; password=root; database=clubdeportivo; port=3306;";

        // Método para obtener la conexión
        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}