
using ETicketing.CA.Domain.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketing.CA.Infrastructure.Database
{
    public class DbFactory : IDbFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DbFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUowDb(string userId, bool useTransaction)
        {
            var scope = _serviceProvider.CreateScope();
            var unit= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            unit.Configure(scope,userId, useTransaction);
            return unit;
        }
    }
}
