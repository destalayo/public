using Common.Utils.CQRS.Interfaces;
using Common.Utils.CQRS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.CQRS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediator, Mediator>();
            return services;
        }
    }
}
