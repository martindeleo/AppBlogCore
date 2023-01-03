using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;   //Nota: para usar el SelectListItem necesito esta lib. Para versiones

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public   interface ICategoriaRepository : IRepository<Categoria>
    {

        IEnumerable<SelectListItem> GetListaCategorias();    //Nota: SelectListItem lo usamos en dropdown

        void Update(Categoria categoria);
    }
}
