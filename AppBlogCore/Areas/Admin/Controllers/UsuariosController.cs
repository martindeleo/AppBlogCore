using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]   //Nota: ponemos esto
    public class UsuariosController : Controller
    {


        private readonly IContenedorTrabajo _contenedorTrabajo;


        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
            
        }

       
        public IActionResult Index()
        {
            //Nota: Opcion 1 : Obtener tooodos los usuarios
            //  return View(_contenedorTrabajo.Usuario.GetAll());

            //Nota: Opcion 2: Obtiene todos los usuarios menos el q esta logedo...para no auto bloquease:

            var claimsIdenty = (ClaimsIdentity)this.User.Identity;    //nota: para obtener todo lo que tiene q ver con la autnticacion del usuario logeado

            var usuarioctual = claimsIdenty.FindFirst(ClaimTypes.NameIdentifier);

            return View(_contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioctual.Value));



        }


        public IActionResult Bloquear(string Id)
        {
            if(Id == null)
            {
                return NotFound();  
            }
            _contenedorTrabajo.Usuario.BloquearUsuario(Id); 
            return RedirectToAction("Index");   

        }

        public IActionResult DesBloquear(string Id,string prueba)       //Nota: El parametro prueba lo uso de ejemplo para ver como mando algo con el asp-route-??? 
        {
            if (Id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Usuario.DesBloquearUsuario(Id);
            return RedirectToAction(nameof(Index));

        }

    }
}
