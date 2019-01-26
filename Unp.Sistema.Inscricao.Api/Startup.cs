using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unp.Sistema.Inscricao.Api.Middlewares;
using Unp.Sistema.Inscricao.Command.Aplicacao.Servicos;
using Unp.Sistema.Inscricao.Command.Dominio;
using Unp.Sistema.Inscricao.Infraestrutura.Repositorios;

namespace Unp.Sistema.Inscricao.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IRepositorioInscricao>(
                x => new RepositorioInscricao(Configuration.GetConnectionString("db")));

            services.AddScoped<ServicoDeVerificacaoDeBolsaDeEstudo, ServicoDeVerificacaoDeBolsaDeEstudo>();
            services.AddScoped<InscricaoService, InscricaoService>();

            //services.AddScoped()
            //services.AddSingleton()
            //services.AddTransient()

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMyLog();
            app.UseMvc();
        }
    }
}
