
using GameSeat.Backend.Business.Services;
using GameSeat.Backend.Infrastructure.Data;
using GameSeat.Backend.Infrastructure.Interfaces;
using GameSeat.Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>

    {

        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    });

//SCOPES
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChairService, ChairService>();
builder.Services.AddScoped<IChairRepository, ChairRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IEstablishmentHourRepository, EstablishmentHourRepository>();
builder.Services.AddScoped<IEstablishmentHourService, EstablishmentHourService>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<StripeService>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var apiKey = configuration["Stripe:SecretKey"];
    return new StripeService(apiKey);
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));


builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowSpecificOrigin",
    //    builder => builder.WithOrigins("http://localhost:4200")
    //        .AllowAnyMethod()
    //        .AllowCredentials()
    //        .AllowAnyHeader());
    options.AddPolicy("AllowSpecificDeployOrigin",
        builder => builder.WithOrigins("https://gameseat-front-aef0d4a36136.herokuapp.com")
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader());
});


builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "GameSeat API",
        Description = "An API for GameSeat"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//app.UseCors("AllowSpecificDeployOrigin");
app.UseCors("AllowSpecificOrigin");

app.UseRouting();

app.MapControllers();

app.Run();
