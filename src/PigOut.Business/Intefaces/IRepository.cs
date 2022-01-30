using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PigOut.Business.Models;

namespace PigOut.Business.Intefaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(int id);
        Task<TEntity> ObterPorGuid(Guid guid);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task RemoverPorId(int id);
        Task RemoverPorGuid(Guid guid);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
        Task SalvarVarios(IEnumerable<TEntity> objs);
        Task RemoveVariosPorId(int[] ids);
    }
}