using System;

namespace ClubDeportivo.Entidades
{
    public class NoSocio : Persona
    {
        public int IdNoSocio { get; set; }

        // Constructor vacío
        public NoSocio() { }

        // Constructor con parámetros (para usarlo más adelante si querés)

        public NoSocio(int idNoSocio, int idPersona, string nombre, string apellido, string dni, string direccion, string telefono)
        : base(idPersona, nombre, apellido, dni, direccion, telefono)
        {
            IdNoSocio = idNoSocio;
        }
        public override string ToString()
        {
            return $"{Nombre} {Apellido} (No Socio)";
        }
    }
}
