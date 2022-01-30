using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PigOut.Business.Intefaces;
using PigOut.Business.Models;
using PigOut.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PigOut.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly PigOutDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(PigOutDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> ObterPorGuid(Guid guid)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Guid == guid);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task RemoverPorId(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public virtual async Task RemoverPorGuid(Guid guid)
        {
            var entity = await ObterPorGuid(guid);
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public virtual async Task RemoveVariosPorId(int[] ids)
        {
            foreach (var id in ids)
            {
                DbSet.Remove(DbSet.Find(id));
            }
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public async Task SalvarVarios(IEnumerable<TEntity> objs)
        {
            Db.AddRange(objs);
            await SaveChanges();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}