﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IServiceBase<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> Set();
        void Save(TEntity entity);
        void Remove(TEntity entity);
        void Commit();
    }
}
