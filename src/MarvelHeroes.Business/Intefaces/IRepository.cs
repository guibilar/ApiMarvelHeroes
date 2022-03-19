using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MarvelHeroes.Business.Models;

namespace MarvelHeroes.Business.Intefaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<TEntity> GetById(Guid guid);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task RemoveById(int id);
        Task RemoveById(Guid guid);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
        Task SaveAll(IEnumerable<TEntity> objs);
        Task RemoveAll(int[] ids);
    }
}