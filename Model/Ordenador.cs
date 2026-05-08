using System.ComponentModel.DataAnnotations;

namespace Inventario.Models;
    public class Ordenador
    {
        [Key]
        public int ID { get; set; }

        public int Admin { get; set; }

        public string? Usuario { get; set; }

        public string? Procesador { get; set; }

        public string? Sistema_Operativo { get; set; }

        public int Ram { get; set; }

        public int Capacidad_Disco { get; set; }

        public string? Tipo_Disco { get; set; }

        public decimal Precio { get; set; }

        public string? url_imagen { get; set; }

        public string? Estado { get; set; }

        public string? mac_address { get; set; }

        public string? etiqueta_inventario { get; set; }

        public string? quien_modifico { get; set; }

        public DateTime? Hora_de_agregacion { get; set; }
    }
