using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LarContracts.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        IList<T> GetAll();

        T FindById(params object[] id);

        T Insert(T entity);

        T Update(T entity);

        void Delete(T entity);

        IList<T> Find(Expression<Func<T, Boolean>> predicate);

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
        //IColumn GetColumnProperties(string field);
    }
}
