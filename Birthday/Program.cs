using Birthday.Installers;
using Business.Interfaces;
using Business.Services;
using Data.Models;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("BirthDatabaseContext");
builder.Services.AddDbContext<BirthDayDatabaseContext>(x => x.UseSqlServer(connectionstring));
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(Assembly.Load("Dto"));
builder.Services.AddSwaggerGen();
builder.Services.InstallServicesInAssembly(builder.Configuration);
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseRouting();

app.UseSwaggerUI();

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

//app.MapControllers();

app.Run();
