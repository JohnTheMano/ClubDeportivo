namespace ClubDeportivo.Entidades
{
    // Clase abstracta: no se puede instanciar directamente.
    public abstract class Persona
    {
        // 🧩 Propiedades comunes
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        // 🔹 Constructor base
        protected Persona(int id, string nombre, string apellido, string dni, string direccion, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Direccion = direccion;
            Telefono = telefono;
        }

        // Constructor vacío (necesario para serialización o uso con DataGrid)
        protected Persona() { }

        // 📄 Método común que se puede sobreescribir
        public virtual string MostrarDatos()
        {
            return $"{Nombre} {Apellido} - DNI: {Dni}";
        }
    }
}
