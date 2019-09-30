﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    using Domain.RepositoryInterfaces;
    public class UnitOfWork : IUnitOfWork
    {
        private BankRegistryDbContext _context;
        public UnitOfWork(BankRegistryDbContext context)
        {
            _context = context;

            RepositoryBank = new Lazy<IBankRepository>(() => new BankRepository(_context));
            RepositoryContactPerson = new Lazy<IContactPersonRepository>(() => new ContactPersonRepository(_context));
            RepositoryPosition = new Lazy<IPositionRepository>(() => new PositionRepository(_context));
        }

        public Lazy<IBankRepository> RepositoryBank { get; private set; }
        public Lazy<IContactPersonRepository> RepositoryContactPerson { get; private set; }
        public Lazy<IPositionRepository> RepositoryPosition { get; private set; }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
