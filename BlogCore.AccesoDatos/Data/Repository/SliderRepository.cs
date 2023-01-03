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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slider slider)
        {
           var objDesdeDB = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeDB.Nombre = slider.Nombre;
            objDesdeDB.Estado = slider.Estado;
            objDesdeDB.UrlImagen = slider.UrlImagen;
       

            _db.Slider.Update(objDesdeDB);

            _db.SaveChanges();  
        }
    }
}
