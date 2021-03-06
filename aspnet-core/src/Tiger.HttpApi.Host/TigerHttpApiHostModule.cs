using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Tiger.EntityFrameworkCore;
using Tiger.MultiTenancy;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Tiger
{
    [DependsOn(
        typeof(TigerHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(TigerApplicationModule),
        typeof(TigerEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreSerilogModule)
        )]
    public class TigerHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureUrls(configuration);
            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureVirtualFileSystem(context);
            ConfigureAuditing(context);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
        }

        private void ConfigureUrls(IConfiguration configuration)
        {   
            
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(TigerApplicationModule).Assembly);
            });
        }

        /// <summary>
        /// ??????identityServer??????????????????
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {

            //var builder = context.Services.AddIdentityServer()
            //   .AddInMemoryIdentityResources(Config.IdentityResources)
            //   //.AddInMemoryApiScopes(Config.ApiScopes)
            //   .AddInMemoryClients(Config.Clients)
            //   .AddTestUsers(TestUsers.Users);



            context.Services.AddAuthentication(options =>
                {
                    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = "Tiger";

                    options.BackchannelHttpHandler = new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                })

            // ?????????????????????
                .AddOpenIdConnect("oidc", "Tiger IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    // ????????????https ????????????????????????false ??????????????????
                    options.RequireHttpsMetadata = false;

                    options.Authority = configuration["AuthServer:Authority"];
                    options.ClientId = "interactive.confidential";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
            //.AddGitHub(options =>
            //{
            //    options.ClientId = configuration["Github:ClientID"];
            //    options.ClientSecret = configuration["Github:ClientSecret"];
            //});
        }

        /// <summary>
        /// ??????SwaggerUI
        /// </summary>
        /// <param name="context"></param>
        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("api", new OpenApiInfo
                    {
                        Title = "Tiger API??????",
                        Version = "v1",
                        Description = "Tiger API ??????????????????",
                        //TermsOfService = "None",
                        TermsOfService = new Uri("http://www.baidu.com"),
                        Contact = new OpenApiContact
                        {
                            Name = "hongjy",
                            Email = "hongjy1991@gmail.com",
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                        }
                    }); ;

                    options.SwaggerDoc("admin", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Tiger admin??????",
                        Description = "Tiger admin??????",

                        //API ??????????????? URL??? ???????????? URL ?????????
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "hongjy",
                            Email = "hongjy1991@gmail.com",
                            Url = new Uri("https://www.hongjy.cn/"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                        }
                    });

                    options.SwaggerDoc("admin-basic", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Tiger Basic??????",
                        Description = "Tiger ?????? Basic??????",
                    });

                    options.SwaggerDoc("admin-erp", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "?????????",
                        Description = "????????????????????????",
                    });



                    //???????????????????????????????????????
                    options.DocInclusionPredicate((docName, apiDes) =>
                    {
                        if (!apiDes.TryGetMethodInfo(out MethodInfo method))
                            return false;
                        /*??????ApiExplorerSettingsAttribute?????????GroupName??????????????????
                         * DeclaringType????????????controller????????????
                         * ?????????????????????action???????????????
                         * */
                        var version = method.DeclaringType.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                        if (docName == "admin" && !version.Any())
                            return true;
                        //????????????action?????????
                        var actionVersion = method.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                        if (actionVersion.Any())
                            return actionVersion.Any(v => v == docName);
                        return version.Any(v => v == docName);
                    });
                    //options.DocInclusionPredicate((docName, description) => true);


                    //???????????? ??????????????????
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "???????????????Bearer?????????Token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                    ////???????????????????????????????????????
                    //options.AddSecurityRequirement(new OpenApiSecurityRequirement<string, IEnumerable<string>>
                    //{
                    //    { "Bearer", Enumerable.Empty<string>() }
                    // });


                    // ??? Swagger JSON and UI??????xml??????????????????
                    //?????????????????????????????????????????????????????????????????????????????????????????????????????????
                    var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                    
                    var xmlPathHost = Path.Combine(basePath, "Tiger.HttpApi.Host.xml");
                    var xmlPathHttpApi = Path.Combine(basePath, "Tiger.HttpApi.xml");
                    var xmlPathApplication = Path.Combine(basePath, "Tiger.Application.xml");
                    var xmlPathContracts = Path.Combine(basePath, "Tiger.Application.Contracts.xml");


                    options.IncludeXmlComments(xmlPathHost);
                    options.IncludeXmlComments(xmlPathHttpApi);
                    options.IncludeXmlComments(xmlPathApplication);
                    options.IncludeXmlComments(xmlPathContracts);
                    options.EnableAnnotations();//??????
                    

                    
                });
        }

        /// <summary>
        /// ???????????????
        /// </summary>
        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                //options.Languages.Add(new LanguageInfo("ar", "ar", "??????????????"));
                //options.Languages.Add(new LanguageInfo("cs", "cs", "??e??tina"));
                //options.Languages.Add(new LanguageInfo("en", "en", "English"));
                //options.Languages.Add(new LanguageInfo("fr", "fr", "Fran??ais"));
                //options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Portugu??s"));
                //options.Languages.Add(new LanguageInfo("ru", "ru", "??????????????"));
                //options.Languages.Add(new LanguageInfo("tr", "tr", "T??rk??e"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "????????????"));
                //options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "????????????"));
            });
        }


        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        /// <summary>
        /// ??????????????????
        /// </summary>
        private void ConfigureAuditing(ServiceConfigurationContext context)
        {
            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabled = true; //Disables the auditing system
                options.HideErrors = true;
                options.IsEnabledForAnonymousUsers = true;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseIdentityServer();
            app.UseAuthorization();

            #region swaggerui ??????
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(options =>
            {
                // ??????openAPI 2.0?????? ?????????3.0??????
                //options.SerializeAsV2 = true;
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                // ????????????????????????
                //options.InjectStylesheet("/swagger-ui/custom.css");

                options.SwaggerEndpoint("/swagger/admin-basic/swagger.json", "Admin-??????????????????");
                options.SwaggerEndpoint("/swagger/admin-erp/swagger.json", "Admin-????????????");
                options.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin-????????????");
                options.SwaggerEndpoint("/swagger/api/swagger.json", "API-");
                
                //options.SwaggerEndpoint("/swagger/auth/swagger.json", "Auth");

                //options.SwaggerEndpoint("/swagger/gp/swagger.json", "????????????");
                //options.SwaggerEndpoint("/swagger/mom/swagger.json", "????????????");
                //options.SwaggerEndpoint("/swagger/dm/swagger.json", "????????????");

                // ?????????????????????????????????
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelExpandDepth(1);

                // API??????????????????
                options.RoutePrefix = string.Empty;
                // API??????Title
                options.DocumentTitle = "Tiger???????????? - ??????????????????";


                options.OAuthClientId("testOauthClientId");
            }); 
            #endregion

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
