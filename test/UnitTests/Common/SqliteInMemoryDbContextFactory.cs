using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.Common
{
    //https://www.meziantou.net/2017/09/11/testing-ef-core-in-memory-using-sqlite
    public class SqliteInMemoryDbContextFactory<TDbContext> : SqliteInMemoryConnectionFactory
        where TDbContext : DbContext
    {
        private readonly Action<String> _logger;
        private readonly Func<DbContextOptions<TDbContext>, TDbContext> _factory;
        public SqliteInMemoryDbContextFactory(Func<DbContextOptions<TDbContext>, TDbContext> factory)
        {
            _factory = factory;
        }

        private bool _created = false;
        private DbContextOptions<TDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<TDbContext>()
                .UseSqlite(_connection)
                .EnableSensitiveDataLogging()
                .Options;
        }

        //cant create and seed using the same context
        public async Task<TDbContext> CreateContextAsync(bool create = true, CancellationToken cancellationToken = default)
        {
            await GetConnection(cancellationToken);

            if (!_created && create)
            {
                using (var context = _factory(CreateOptions()))
                {
                    await context.Database.EnsureCreatedAsync(cancellationToken);
                }
                _created = true;
            }

            return _factory(CreateOptions());
        }
    }
}
