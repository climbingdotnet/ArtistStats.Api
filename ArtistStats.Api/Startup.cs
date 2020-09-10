using System;
using ArtistStats.Domain.Concrete;
using ArtistStats.Domain.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ArtistStats.Domain.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ArtistStats.Api
{
    public class Startup
    {
        private readonly OpenApiInfo openApiInfo;
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.openApiInfo = new OpenApiInfo { Title = "Artist Stats Api", Description = "", Version = "v1" };
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        openApiInfo);
                });

            services.AddHttpClient("musicbrainz", client =>
            {
                client.BaseAddress = new Uri(this.configuration.GetValue<string>("api:musicbrainz"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd(this.configuration.GetValue<string>("userAgent"));
            });

            services.AddHttpClient("lyricsovh", client =>
            {
                client.BaseAddress = new Uri(this.configuration.GetValue<string>("api:lyricsovh"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd(this.configuration.GetValue<string>("userAgent"));
            });

            services.AddScoped<IRepository<Artist, string>, ArtistRepository>();
            services.AddScoped<IRepository<Lyric, LyricIdentifier>, LyricRepository>();

            services.AddScoped<IArtistStatBuilder, ArtistStatBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger().UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint($"/swagger/{openApiInfo.Version}/swagger.json", openApiInfo.Title);
                    c.DocExpansion(DocExpansion.None);
                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
