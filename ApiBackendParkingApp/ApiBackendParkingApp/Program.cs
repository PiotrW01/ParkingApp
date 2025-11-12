using ApiBackendParkingApp.Interfaces;
using ApiBackendParkingApp.Models.Mapping;
using ApiBackendParkingApp.Repositories;
using ApiBackendParkingApp.Repositories.Interfaces;
using ApiBackendParkingApp.Services;
using ApiBackendParkingApp.Services.Interfaces;
using ApiBackendParkingApp.Utilis;
using AutoMapper;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ParkinLotMappingProfile());
    mc.AddProfile(new SectorMappingProfile());
    mc.AddProfile(new PlaceMappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddScoped<IParkingLotRepository, ParkingLotRepository>();
builder.Services.AddScoped<IParkingLotService, ParkingLotService>();
builder.Services.AddScoped<IDbInteractions, DbInteraction>();
builder.Services.AddScoped<IDataBaseConfig, DataBaseConfig>();
builder.Services.AddSingleton<DataBaseConfig>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowLocalhost");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
