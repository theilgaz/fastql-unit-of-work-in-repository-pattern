using FastqlSample.Infrastructure.Models;
using System.Collections.Generic;

namespace FastqlSample.Infrastructure.Core.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Response<TEntity> Get(int id);
        Response<TEntity> Get(string where);
        Response<IEnumerable<TEntity>> GetAll();
        Response<IEnumerable<TEntity>> GetAll(string where);
        Response<long> Insert(TEntity entity); 
        Response<bool> Update(TEntity entity, string where);
        Response<bool> Delete(string where);
        Response<int> Count(string where = null);
    }
}