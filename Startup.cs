using Discord.WebSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MinSon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MinSonDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                
               
            });

            services.AddScoped<ScotchService>();
            services.AddScoped<ScotchRepository>();

            services.AddScoped<ZeldaService>();
            services.AddScoped<ZeldaRepository>();

            services.AddScoped<DiscordService>();
            services.AddScoped<DiscordRepository>();

            services.AddScoped(typeof(IScotchService), typeof( ScotchService));
            services.AddScoped(typeof(IScotchRepository), typeof(ScotchRepository));

            services.AddScoped(typeof(IZeldaService), typeof(ZeldaService));
            services.AddScoped(typeof(IZeldaRepository), typeof(ZeldaRepository));

            services.AddScoped(typeof(IDiscordService), typeof(DiscordService));
            services.AddScoped(typeof(IDiscordRepository), typeof(DiscordRepository));

            buildServiceProvider(services);
            
        }

        private DiscordSocketClient _client;
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
        void buildServiceProvider(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var bot = new Bot(serviceProvider);
            services.AddSingleton(bot);
        }


    }
}
