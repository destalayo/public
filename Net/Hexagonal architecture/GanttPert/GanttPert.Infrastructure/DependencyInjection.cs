using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using GanttPert.Infrastructure.Database;
using GanttPert.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("MiBaseEnMemoria")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)),           
            ServiceLifetime.Scoped,  
            ServiceLifetime.Scoped);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IFeaturesRepository, FeaturesRepository>();
            services.AddScoped<ITasksRepository, TasksRepository>();
            return services;
        }
    }
}
