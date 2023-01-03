using BlogCore.AccesoDatos.Data.Repository.IRepository;     //Nota: Se agrega
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class   //Nota: Me sigue dando error porque todavia no impremente los metodos de la interfaz. Usar la bombita q los crea jajaja

    {
        protected readonly DbContext Context;       //Nota lo agrego
        internal DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.DbSet = context.Set<T>();
        }


        void IRepository<T>.Add(T entity)
        {
            DbSet.Add(entity);
        }

        T IRepository<T>.Get(int id)
        {
            return DbSet.Find(id);
        }

        //Nota: Este es el mas complejo de implemetar :
        IEnumerable<T> IRepository<T>.GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties)
        {
            IQueryable<T> query = DbSet; // Nota: IQueryable es parecido a IEnumerable, pero se pueden hacer consultas al resultado

            if(filter !=null)
            {
                query = query.Where(filter);
            }

            //Nota: Cuando tengo propiedades, me trae todas las relacionadas 1 a 1 o 1 a *
            //Include Properties separados por comas
            if(includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();

        }

        T IRepository<T>.GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Nota: Cuando tengo propiedades, me trae todas las relacionadas 1 a 1 o 1 a *
            //Include Properties separados por comas
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }

            return query.FirstOrDefault();
        }

        void IRepository<T>.Remove(int id)
        {
            T entityToremove = DbSet.Find(id);
            DbSet.Remove(entityToremove);
        }

        void IRepository<T>.Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
