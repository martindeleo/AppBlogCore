using AppBlogCore.Models;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppBlogCore.Areas.Cliente.Controllers
{
    [Area("Cliente")]   //Nota: Al usar areas y haber movido este controler, le tengo que aclarar esto
    public class HomeController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            HomeVM homevm = new HomeVM()
            {
                Slider = _contenedorTrabajo.Slider.GetAll(),
                ListaArticulos = _contenedorTrabajo.Articulo.GetAll()
            };

            //Nota: La linea siguiente es para saber si estoy o no en el home :
            ViewBag.EstoyEnElHome = true;   //Nota: EstoyEnElHome lo invento como se me canta!
            return View(homevm);
        }


        public IActionResult Details(int id)
        {
            var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(id);

         
            return View(articuloDesdeDb);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}