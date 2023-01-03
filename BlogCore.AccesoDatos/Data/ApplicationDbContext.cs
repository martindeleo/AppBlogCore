using BlogCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppBlogCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Los pasos son:
        /// 1. creo el modelo
        /// 2. lo doy de alta aqui en dbContext
        /// 3. corro add-migration NmbreMigracion
        /// 4. corro update-database
        /// 5. opcional...puedo correr migration y me dice todas aplicadas = true
        /// </summary>

        public DbSet<Categoria> Categoria { get; set; }     //Nota: Despues de crear el modelo, pongo esto y despues migro !!

        public DbSet<Articulo> Articulo { get; set; }

        public DbSet<Slider> Slider { get; set; }

       // public DbSet<AplicationUser> AplicationUser { get; set; }

    }
}