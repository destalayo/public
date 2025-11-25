using Common.Utils.CQRS.Interfaces;
using Common.Utils.CQRS.Models;
using Common.Utils.Tools.Interfaces;
using Common.Utils.Tools.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Tools
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTools(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IToolServices, ToolServices>();
            return services;
        }
    }
}
