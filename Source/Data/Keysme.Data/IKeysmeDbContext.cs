﻿namespace Keysme.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface IKeysmeDbContext
    {
        IDbSet<Host> Hosts { get; set; }

        IDbSet<Currency> Currencies { get; set; }

        IDbSet<Country> Countries { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
