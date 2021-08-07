namespace WebCRM.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.SpaServices;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Microsoft.EntityFrameworkCore;
    using VueCliMiddleware;
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using WebCRM.RoleSecurity.Helpers;

    /// <summary>
    /// Start up point for Web Api and Vue application
    /// </summary>
    /// <author>Daniel Lee Graf</author>
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
            services.AddDbContext<CRMDBContext>(options => options.UseSqlite("Data Source=CRMDB.sqlite3"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebCRM.WebApi", Version = "v1" });
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../vue-app/dist";
            });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICRMRepository<AccountMembership, AccountMembershipViewModel>, AccountMembershipRepository>();
            services.AddScoped<ICRMRepository<AccountNote, AccountNoteViewModel>, AccountNoteRepository>();
            services.AddScoped<ICRMRepository<ContractExpense, ContractExpenseViewModel>, ContractExpenseRepository>();
            services.AddScoped<ICRMRepository<Contract, ContractViewModel>, ContractRepository>();
            services.AddScoped<ICRMRepository<ContractTransaction, ContractTransactionViewModel>, ContractTransactionRepository>();
            services.AddScoped<ICRMRepository<CRMAccount, CRMAccountViewModel>, CRMAccountRepository>();
            services.AddScoped<ICRMRepository<Member, MemberViewModel>, MemberRepository>();
            services.AddScoped<ICRMRepository<MemberTestimonial, MemberTestimonialViewModel>, MemberTestimonialRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebCRM.WebApi v1"));
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../vue-app";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }
            });
        }
    }
}
