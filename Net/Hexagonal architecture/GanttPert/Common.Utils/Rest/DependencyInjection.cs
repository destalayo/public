using Common.Utils.Rest.Interfaces;
using Common.Utils.Rest.Models;
using Common.Utils.Tools.Interfaces;
using Common.Utils.Tools.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Rest
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRest(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRestService, RestService>();
            return services;
        }
    }
}
