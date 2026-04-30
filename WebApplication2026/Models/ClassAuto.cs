using System.ComponentModel.DataAnnotations;

namespace WebApplication2026.Models
{
    public class ClassAuto
    {

        public int Idauto { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Motor { get; set; }
        public decimal Precio { get; set; }
        [Display(Name = "Seleccione Marca")]
        public int Idmarca { get; set; }
        [Display(Name = "Marca")]
        public string Nommarca { get; set; }

    }// fin de la clase
}
