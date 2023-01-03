using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Articulo  //Nota: decia internal lo cambie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para el articulo")]
        [Display(Name = "Nombre del Articulo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "descripcion obligatoria")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de creacion")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        [Required]                                      //Nota: Las 2 propiedades sigueintes son para una relacion 1 a n
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set;}


        //Nota: Despues de crer el modelo SI o SI lo tenemos q poner en al ApplicationDbContext y despues migrar
    }
}