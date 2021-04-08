using System;
using System.Collections.Generic;
using System.Text;
using FactoryConnection.ConnectionFactory;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryConnection.DependencyInjection
{
    public static class DependencyConnection
    {
        public static IServiceCollection AddConnectionOrCache(this IServiceCollection services)
        {
            services.AddSingleton<IConnection, Connection>();
            return services;
        }
    }
}
