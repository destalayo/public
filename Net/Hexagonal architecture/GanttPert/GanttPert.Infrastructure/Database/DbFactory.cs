using GanttPert.Domain.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Infrastructure.Database
{
    public class DbFactory : IDbFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DbFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUowDb(bool useTransaction)
        {
            var scope = _serviceProvider.CreateScope();
            var unit= scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            unit.Configure(scope, useTransaction);
            return unit;
        }
    }
}
