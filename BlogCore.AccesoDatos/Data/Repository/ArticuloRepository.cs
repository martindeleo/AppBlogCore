using AppBlogCore.Data;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Articulo articulo)
        {
           var objDesdeDB = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDB.Nombre = articulo.Nombre;
            objDesdeDB.Descripcion = articulo.Descripcion;
            objDesdeDB.FechaCreacion = articulo.FechaCreacion;
            objDesdeDB.UrlImagen = articulo.UrlImagen;
            objDesdeDB.CategoriaId = articulo.CategoriaId;

            _db.Articulo.Update(objDesdeDB);

            _db.SaveChanges();  
        }
    }
}
