using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelHeroes.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

    }
}