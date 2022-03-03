using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bmwadforth.Middleware;
using Bmwadforth.Repositories;
using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<BlobRepository>().As<IBlobRepository>();
    containerBuilder.RegisterType<ArticleRepository>().As<IArticleRepository>();
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database") ?? throw new Exception("Database connection string must not be null")));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();