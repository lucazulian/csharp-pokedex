using System.Collections.Generic;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Services;
using CsharpPokedex.Domain.TranslationStrategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CsharpPokedex.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CsharpPokedex.Api", Version = "v1"});
            });

            services.AddHttpClient();

            services.AddScoped<IPokemonClient, PokemonClient>();
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddSingleton<IFunTranslationsClient, FunTranslationsClient>();
            services.AddSingleton(x => new YodaTranslationStrategy(x.GetService<IFunTranslationsClient>()));
            services.AddSingleton(x => new ShakespeareTranslationStrategy(x.GetService<IFunTranslationsClient>()));
            services.AddSingleton<ITranslationService>(x =>
                new TranslationService(new Dictionary<TranslatorType, ITranslationStrategy>
                {
                    {TranslatorType.Yoda, x.GetRequiredService<YodaTranslationStrategy>()},
                    {TranslatorType.Shakespeare, x.GetRequiredService<ShakespeareTranslationStrategy>()}
                })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CsharpPokedex.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}