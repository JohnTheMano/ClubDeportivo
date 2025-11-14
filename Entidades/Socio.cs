using System;
using System.Collections.Generic;

namespace ClubDeportivo.Entidades
{
    public class Socio : Persona
    {
        // 🔹 Atributos específicos del socio
        public int IdSocio { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool TieneCarnet { get; set; }
        public bool Estado { get; set; } // activo o dado de baja

        // 🔗 Asociación: un socio puede tener varias cuotas
        public List<Cuota> Cuotas { get; set; } = new List<Cuota>();

        // 🔹 Constructores
        public Socio() : base() { }

        public Socio(int idSocio, int id, string nombre, string apellido, string dni, string direccion, string telefono,
                     DateTime fechaAlta, bool tieneCarnet, bool estado)
            : base(id, nombre, apellido, dni, direccion, telefono)
        {
            IdSocio = idSocio;
            FechaAlta = fechaAlta;
            TieneCarnet = tieneCarnet;
            Estado = estado;
        }

        // 🔹 Método que representa la acción de pagar una cuota
        public void PagarCuota(Cuota cuota)
        {
            if (cuota == null)
                throw new ArgumentNullException(nameof(cuota));

            Cuotas.Add(cuota);
        }

        // 🔹 Sobrescribimos MostrarDatos() para incluir info adicional
        public override string MostrarDatos()
        {
            string estadoTexto = Estado ? "Activo" : "Inactivo";
            return $"{base.MostrarDatos()} | Socio N°: {IdSocio} | Alta: {FechaAlta:dd/MM/yyyy} | Estado: {estadoTexto}";
        }
    }
}
