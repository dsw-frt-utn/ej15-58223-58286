using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class EntityBase
    {
        public Guid? Id { get; private set; }
        protected EntityBase(Guid? id = null) 
        {
            Id = id ?? Guid.NewGuid();
        }
    }
}
