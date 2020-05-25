using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;

namespace WebApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddRazorPages();

            var authBuilder = services.AddAuthentication();

            // Sign-in users with the Microsoft identity platform
            authBuilder.AddSignIn(Configuration, "AzureAd", "AzureAd", "AzureAdCookies");

            // Sign-in users with AzureADB2C
            authBuilder.AddSignIn(Configuration, "AzureAdB2C", "AzureAdB2C", "AzureAdB2CCookies");

            // Sign-in users with a generic OpenIDConnect provider.
            // Need to set up Cookies authentication for this provider manually,
            // indicate that it should delegate to the OpenIdConnect one for auth
            // challenges, and indicate that the Cookies scheme we defined should
            // be used for SignIn after the OpenIdConnect handler does its job.
            authBuilder
                .AddCookie("OidcProviderCookies", cookieOptions =>
                {
                    cookieOptions.ForwardChallenge = "OidcProvider";
                })
                .AddOpenIdConnect("OidcProvider", options =>
                {
                    Configuration.GetSection("OidcProvider").Bind(options);
                    options.SignInScheme = "OidcProviderCookies";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}