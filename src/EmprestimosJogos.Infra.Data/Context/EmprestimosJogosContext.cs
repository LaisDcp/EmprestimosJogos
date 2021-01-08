using EmprestimosJogos.Domain.Core.Models;
using EmprestimosJogos.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.Data.Context
{
    public class EmprestimosJogosContext : DbContext
    {
        public static readonly LoggerFactory _debugLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entities Config (identificação automática)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmprestimosJogosContext).Assembly);

            //Configurações globais
            modelBuilder.ApplyGlobalStandards();
            modelBuilder.SeedData();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(wh => wh.ClrType.BaseType == typeof(Entity)))
            {
                /// <summary>
                /// Cria uma expressão para remover deletados
                /// "entity => EF.Property<bool>(entity, "IsDeleted") == false"
                /// </summary>
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType);
                MethodInfo propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                MethodCallExpression isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                LambdaExpression lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot _configuration = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", optional: true)
                                                    .AddJsonFile($"appsettings.{_environment}.json", optional: true, reloadOnChange: true)
                                                    .AddEnvironmentVariables()
                                                    .Build();

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(EmprestimosJogosContext)));

            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();

            if (Debugger.IsAttached)
                optionsBuilder.UseLoggerFactory(_debugLoggerFactory);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                if (entry.Entity is Entity trackableEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        trackableEntity.CreatedDate = DateTime.Now;
                        trackableEntity.IsDeleted = false;
                    }

                    else if (entry.State == EntityState.Modified)
                        trackableEntity.ModifiedDate = DateTime.Now;
                }
            });
        }

        public void MarkDeleteOnDeletedEntities()
        {
            ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                if (entry.State == EntityState.Deleted && entry.Entity is Entity trackableEntity)
                {
                    trackableEntity.ModifiedDate = DateTime.Now;
                    trackableEntity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            });
        }

        public void ResetContextState() => ChangeTracker.Entries()
                                                        .Where(e => e.Entity != null).ToList()
                                                        .ForEach(e => e.State = EntityState.Detached);
    }
}
