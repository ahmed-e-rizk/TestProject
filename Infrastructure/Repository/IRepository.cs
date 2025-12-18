using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        public T Add(T entity);
        public T Get(Expression<Func<T, bool>> where);
    }
}
