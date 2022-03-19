using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MarvelHeroes.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MarvelHeroesDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MarvelHeroesDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetById(Guid guid)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Guid == guid);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task RemoveById(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public virtual async Task RemoveById(Guid guid)
        {
            var entity = await GetById(guid);
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public virtual async Task RemoveAll(int[] ids)
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

        public async Task SaveAll(IEnumerable<TEntity> objs)
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