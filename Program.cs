using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogWebsite.Common.AuthenticationSchemes;
using BlogWebsite.Common.Configuration;
using BlogWebsite.Common.Exceptions;
using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Middleware;
using BlogWebsite.Common.Models;
using BlogWebsite.Common.Repositories;
using BlogWebsite.Common.Service;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAntiforgery();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));
// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<BlobRepository>().As<IBlobRepository>();
    containerBuilder.RegisterType<ArticleRepository>().As<IArticleRepository>();
    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
    containerBuilder.RegisterType<JwtAuthenticationService>().As<IJwtAuthenticationService>();
});
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database") ??
                      throw new Exception("Database connection string must not be null")));
builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;

        var authConfig = new AuthenticationConfiguration();
        builder.Configuration.Bind("Authentication", authConfig);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = authConfig.Audience,
            ValidIssuer = authConfig.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.Key))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = ctx =>
                throw new AuthenticationException("Invalid authentication challenge", ctx.AuthenticateFailure)
        };
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = ".bmwadforth.cookie";
        options.Cookie.Domain = builder.Environment.IsDevelopment() ? ".localhost" : ".bmwadforth.com";
        options.Cookie.Path = "/";
        options.Cookie.HttpOnly = false;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = false;
        options.LoginPath = "/api/v1/user/login";
        options.LogoutPath = "/api/v1/user/logout";

        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = ctx =>
                throw new AuthenticationException("Invalid authentication challenge")
        };
    })
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
        ApiKeyAuthenticationDefaults.AuthenticationScheme, null);

builder.Configuration.AddEnvironmentVariables(prefix: "BMWADFORTH_");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// app.UseCookiePolicy();

app.UseRouting();
// app.UseRequestLocalization();
app.UseCors(configurePolicy => { configurePolicy.WithOrigins("https://localhost:44401"); });

app.UseAuthentication();
app.UseAuthorization();
// app.UseSession();
// app.UseResponseCompression();
// app.UseResponseCaching();

//app.MapRazorPages();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();