using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

namespace Book
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
            services.AddSpaStaticFiles(c => c.RootPath = "ClientApp/build");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    };
                    services.AddAuthentication()
                      .AddGoogle(options =>
                       {
                           IConfigurationSection googleAuthNSection =
                               Configuration.GetSection("Authentication:Google");

                           options.ClientId = googleAuthNSection["276896049146-2s6ns615hpnlq7lthasuhpvqkqo3mcs2.apps.googleusercontent.com"];
                           options.ClientSecret = googleAuthNSection["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"];
                       });
                });


            //services.AddIdentity<User, IdentityRole>
            //    (options => options.SignIn.RequireConfirmedAccount = true)
            //   .AddUserStore<ArticleContext>()
            //   .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddControllers();
            // services.AddRazorPages();

            services.AddControllersWithViews();

            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            services.AddDbContext<ArticleContext>(
                opts => opts.UseNpgsql(connectionString)
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book v1"));
            }
            else
            {


                app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(contextFeature.Error.Message);
                        }
                    });
                });
                app.UseHsts();

            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseSwaggerUI(c =>
            // {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book V2");
            // });


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}
