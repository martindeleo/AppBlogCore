using AppBlogCore.Data;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;   //Nota: para usar el SelectListItem necesito esta lib. Para versiones

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categoria.Select(i => new SelectListItem()
                {
                    Text = i.Nombre,            //Nota: Esto es para los combos
                    Value = i.Id.ToString()
                }
            );
        }


        public void Update(Categoria categoria)
        {
           var objDesdeDB = _db.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDB.Nombre = categoria.Nombre;
            objDesdeDB.Orden = categoria.Orden;

            _db.Categoria.Update(objDesdeDB);

            _db.SaveChanges();  
        }
    }
}
