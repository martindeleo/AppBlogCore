using Microsoft.AspNetCore.Mvc.Rendering;   //Nota: para usar el SelectListItem necesito esta lib. Para versiones anteriores a la 7 se usa AspNet.Mvc
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class ArticuloVM
    {
        public Articulo Articulo { get; set; }

        public IEnumerable<SelectListItem> ListaCategorias { get; set; }

    }
}
