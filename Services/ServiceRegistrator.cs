using ReceptFromHolodilnik.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReceptFromHolodilnik.Services
{
    internal static class ServiceREgistrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPythonModel, PythonModelDialog>();
            services.AddSingleton<IYoloDialog, YoloDialog>();
            services.AddSingleton<IYouTubeService, YouTubeFindService>();
            return services;
        }
    }
}
