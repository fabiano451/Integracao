using LarAutoMapper;
using LarContracts.IRepository;
using LarContracts.IService;
using LarRepository.Context;
using LarRepository.Repositories;
using LarService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =  ReferenceHandler.Preserve;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.RegisterMappings();

builder.Services.AddDbContext<LarEFContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPesssoaService, PessoaService>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ITelefoneService, TelefoneService>();
builder.Services.AddScoped<ITelefoneRepository, TelefoneRepository>();

builder.Services.AddScoped<BaseValidadtionService, BaseValidadtionService>();


var loggerFactory = LoggerFactory.Create(
           builder => builder
                       // add console as logging target
                       .AddConsole()
                       // add debug output as logging target
                       .AddDebug()
                       // set minimum level to log
                       .SetMinimumLevel(LogLevel.Debug)
       );

//var logger = loggerFactory.CreateLogger<Program>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


