using System;

namespace MarvelHeroes.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; set; }
        public int Id { get; set; }

    }
}