using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class EntityBase()
    {
        public Guid? _id = Guid.NewGuid();
        public EntityBase(Guid? id) :this()
        {
            _id = id;
        }
    }
}
