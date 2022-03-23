using MedicalAssistant.AspNetCommon.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;

namespace MedicalAssistant.AspNetCommon.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void MigrateDatabase<TContext>(this IServiceProvider serviceProvider, int attempts)
            where TContext : DbContext
        {
            Exception failException = null;
            for (var attemptNumber = 0; attemptNumber < attempts; attemptNumber++)
            {
                try
                {
                    serviceProvider.MigrateDatabase<TContext>();
                    return;
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception exc)
#pragma warning restore CA1031 // Do not catch general exception types
                {
                    failException = exc;
                    Thread.Sleep(TimeSpan.FromSeconds(1 + attemptNumber));
                }
            }

            throw new MigrationFailedException(failException);
        }

        public static void MigrateDatabase<TContext>(this IServiceProvider serviceProvider)
            where TContext : DbContext
        {
            var context = serviceProvider.GetService<TContext>();
            var pendingMigrations = context.Database.GetPendingMigrations();

            if (!pendingMigrations.Any())
            {
                return;
            }

            context.Database.Migrate();
        }
    }
}
