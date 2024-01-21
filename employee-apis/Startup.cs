using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Data.UnitofWork;
using Domain.Mapping;
using Domain.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace employee_apis
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
            services.AddCors();
            services.AddControllers();
            services.AddSingleton(Configuration);
            
            services.AddDbContext<mainDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            //services.AddDbContextPool<mainDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //services.AddDbContext<IdentityContext>();

            //services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddTransient(typeof(EmployeeService<,>), typeof(EmployeeService<,>));
            services.AddTransient(typeof(DepartmentService<,>), typeof(DepartmentService<,>));

            services.AddTransient(typeof(UserManager<>), typeof(UserManager<>));
            services.AddTransient(typeof(SignInManager<>), typeof(SignInManager<>));

            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddDataProtection().SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            services.AddDataProtection();
            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});


            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = "3168317616542352";
            //    facebookOptions.AppSecret = "3484f06f53d601803a9287099ecf66ab";
            //    //facebookOptions.AuthorizationEndpoint = "https://localhost:44391/users/faceback";
            //    //facebookOptions.SaveTokens = true;
            //    facebookOptions.CallbackPath = "/users/faceback";
            //    //facebookOptions.TokenEndpoint
            //    //facebookOptions.CallbackPath = "/faceback";
            //});

            //services.AddIdentityCore<ApplicationUser>(options => { });
            //new IdentityBuilder(typeof(ApplicationUser), typeof(IdentityRole), services)
            //    .AddRoleManager<RoleManager<IdentityRole>>()
            //    .AddSignInManager<SignInManager<ApplicationUser>>()
            //    .AddEntityFrameworkStores<mainDBContext>();

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<mainDBContext>()
            //.AddDefaultTokenProviders();
            
            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<mainDBContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Login";
            });

            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //    facebookOptions.CallbackPath = new PathString("/signin-facebook");

            //    //facebookOptions.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
            //    //{
            //    //    OnCreatingTicket = async context =>
            //    //    {
            //    //        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            //    //        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

            //    //        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
            //    //        response.EnsureSuccessStatusCode();

            //    //        var user = await response.Content.ReadAsStringAsync();
            //    //        JsonDocument document = JsonDocument.Parse(user);
            //    //        context.RunClaimActions(document.RootElement);
            //    //    }
            //    //};
            //});

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(o => o.LoginPath = new PathString("/login"))
            //    // You must first create an app with Facebook and add its ID and Secret to your user-secrets.
            //    // https://developers.facebook.com/apps/
            //    // https://developers.facebook.com/docs/facebook-login/manually-build-a-login-flow#login
            //    .AddFacebook(o =>
            //    {
            //        o.AppId = Configuration["Authentication:Facebook:AppId"];
            //        o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //        o.Scope.Add("email");
            //        o.Fields.Add("name");
            //        o.Fields.Add("email");
            //        o.SaveTokens = true;
            //        o.CallbackPath = new PathString("/signin-facebook");
            //        o.Events = new OAuthEvents()
            //        {
            //            OnRemoteFailure = HandleOnRemoteFailure
            //        };
            //    }
            //    );

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:Secretkey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee APIs", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Specify the authorization token (JWT)",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);
                c.CustomSchemaIds(x => x.FullName);
                var requirement = new OpenApiSecurityRequirement();
                requirement.Add(securityScheme, new string[] { });
                c.AddSecurityRequirement(requirement);

            });

            services.AddAuthorization(config =>
            {
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAuthPolicy = defaultAuthBuilder
                //    .RequireAuthenticatedUser()
                //    .RequireClaim(ClaimTypes.DateOfBirth)
                //    .Build();

                //config.DefaultPolicy = defaultAuthPolicy;

                config.AddPolicy("Student", policyBuilder => policyBuilder.RequireClaim("UserJob", "Student"));

            });


        }

        private async Task HandleOnRemoteFailure(RemoteFailureContext context)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("<html><body>");
            await context.Response.WriteAsync("A remote failure has occurred: <br>" +
                context.Failure.Message.Split(Environment.NewLine).Select(s => HtmlEncoder.Default.Encode(s) + "<br>").Aggregate((s1, s2) => s1 + s2));

            if (context.Properties != null)
            {
                await context.Response.WriteAsync("Properties:<br>");
                foreach (var pair in context.Properties.Items)
                {
                    await context.Response.WriteAsync($"-{ HtmlEncoder.Default.Encode(pair.Key)}={ HtmlEncoder.Default.Encode(pair.Value)}<br>");
                }
            }

            await context.Response.WriteAsync("<a href=\"/\">Home</a>");
            await context.Response.WriteAsync("</body></html>");

            // context.Response.Redirect("/error?FailureMessage=" + UrlEncoder.Default.Encode(context.Failure.Message));

            context.HandleResponse();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var context = new mainDBContext();
            //try
            //{
            //    context.Database.Migrate();
            //}
            //catch (Exception ee)
            //{
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee APIs V1");
            });

            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.Map("/login", signinApp =>
            {
                signinApp.Run(async context =>
                {
                    var authType = context.Request.Query["authscheme"];
                    if (!string.IsNullOrEmpty(authType))
                    {
                        // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
                        // send them to the home page instead (/).
                        await context.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = "/" });
                        return;
                    }

                    var response = context.Response;
                    response.ContentType = "text/html";
                    await response.WriteAsync("<html><body>");
                    await response.WriteAsync("Choose an authentication scheme: <br>");
                    var schemeProvider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
                    foreach (var provider in await schemeProvider.GetAllSchemesAsync())
                    {
                        await response.WriteAsync("<a href=\"?authscheme=" + provider.Name + "\">" + (provider.DisplayName ?? "(suppressed)") + "</a><br>");
                    }
                    await response.WriteAsync("</body></html>");
                });
            });

            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
