using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wipro.Api.Configuration
{
    public static class CorsConfig
    {
        public static void AddCorsService(this IServiceCollection services)
        {
            services.AddCors(c =>
                c.AddPolicy("DefaultPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }));
        }

        public static void UseCorsSetup(this IApplicationBuilder app)
        {
            app.UseCors("DefaultPolicy");
        }
    }
}
