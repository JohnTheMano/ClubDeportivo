using System;

namespace ClubDeportivo.Entidades
{
    public class Cuota
    {
        public int IdCuota { get; set; }
        public int IdSocio { get; set; } // clave foránea a Socio
        public double Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string MedioPago { get; set; } // Efectivo, Tarjeta, etc.

        // Constructor vacío
        public Cuota() { }

        // Constructor con parámetros
        public Cuota(int idCuota, int idSocio, double monto, DateTime fechaVencimiento, string medioPago)
        {
            IdCuota = idCuota;
            IdSocio = idSocio;
            Monto = monto;
            FechaVencimiento = fechaVencimiento;
            MedioPago = medioPago;
        }

        // Método que indica si la cuota está vencida
        public bool EstaVencida()
        {
            return DateTime.Now.Date > FechaVencimiento.Date;
        }

        // Método para mostrar info de la cuota
        public override string ToString()
        {
            string estado = EstaVencida() ? "Vencida" : "Al día";
            return $"Cuota ID: {IdCuota} | Monto: {Monto:C} | Vence: {FechaVencimiento:dd/MM/yyyy} | Estado: {estado}";
        }

    }
}