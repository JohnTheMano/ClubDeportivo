using System;

namespace ClubDeportivo.Entidades
{
    public class Actividad
    {
        public int IdActividad { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Horario { get; set; }
        public int CupoMaximo { get; set; }

        // Constructor vacío
        public Actividad() { }

        // Constructor con parámetros
        public Actividad(int idActividad, string nombre, double precio, string horario, int cupoMaximo)
        {
            IdActividad = idActividad;
            Nombre = nombre;
            Precio = precio;
            Horario = horario;
            CupoMaximo = cupoMaximo;
        }

        // Método que devuelve info resumida de la actividad
        public override string ToString()
        {
            return $"{Nombre} | Precio: {Precio:C} | Horario: {Horario} | Cupo máximo: {CupoMaximo}";
        }
    }
}
