using System;

namespace ClubDeportivo.Entidades
{
    public class PagoEventual
    {
        public int IdPagoEventual { get; set; }
        public int IdNoSocio { get; set; }      // referencia al NoSocio
        public int IdActividad { get; set; }    // referencia a la Actividad
        public double Monto { get; set; }
        public string MedioPago { get; set; }   // Efectivo, Tarjeta, etc.
        public DateTime FechaPago { get; set; }

        // Constructor vacío
        public PagoEventual() { }

        // Constructor con parámetros
        public PagoEventual(int idPagoEventual, int idNoSocio, int idActividad, double monto, string medioPago, DateTime fechaPago)
        {
            IdPagoEventual = idPagoEventual;
            IdNoSocio = idNoSocio;
            IdActividad = idActividad;
            Monto = monto;
            MedioPago = medioPago;
            FechaPago = fechaPago;
        }

        // Método que devuelve info resumida del pago
        public override string ToString()
        {
            return $"PagoEventual ID: {IdPagoEventual} | NoSocio ID: {IdNoSocio} | Actividad ID: {IdActividad} | Monto: {Monto:C} | Medio: {MedioPago} | Fecha: {FechaPago:dd/MM/yyyy}";
        }
    }
}
