using System.Collections.Generic;

namespace ProjetoModeloDDD.Application.Interfaces
{
    public interface IAppServiceBase<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void Dispose();
    }
}
