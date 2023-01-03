using Microsoft.AspNetCore.Mvc.Rendering;   //Nota: para usar el SelectListItem necesito esta lib. Para versiones anteriores a la 7 se usa AspNet.Mvc
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class HomeVM     //VM de View Model
    {
       // public Articulo Articulo { get; set; }

        public IEnumerable<Slider> Slider { get; set; }

        public IEnumerable<Articulo> ListaArticulos { get; set; }

    }
}
