using System.Net.Http.Headers;
using AltruCode.Graph;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options);

        options.Events.OnTokenValidated = async context =>
        {
            var tokenAcquisition = context.HttpContext.RequestServices
                .GetRequiredService<ITokenAcquisition>();

            var graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(async (request) => {
                    var token = await tokenAcquisition
                        .GetAccessTokenForUserAsync(new[] { "User.Read", "User.ReadBasic.All" }, user: context.Principal);
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                })
            );
            // Get user from Graph
            var user = await graphClient.Me.Request()
                .Select(u => new
                {
                    u.DisplayName,
                    u.GivenName,
                    u.Surname,
                    u.Mail
                }).GetAsync();
            context.Principal?.AddUserGraphInfo(user);
        };
    })
    .EnableTokenAcquisitionToCallDownstreamApi(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
    }, GraphConstants.Scopes)
    .AddMicrosoftGraph(options =>
    {
        options.Scopes = string.Join(' ', GraphConstants.Scopes);
    })
    .AddInMemoryTokenCaches();

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddMicrosoftIdentityUI();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();