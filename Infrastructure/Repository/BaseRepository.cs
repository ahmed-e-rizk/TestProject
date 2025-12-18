using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Entites;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
       private SystemContext _db;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(SystemContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }
       
        public T Add(T entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
            return entity;  
        }
       


        public T Get(Expression<Func<T, bool>> where)
        {
            return  _dbSet.FirstOrDefault(where);
        }


    }
}
