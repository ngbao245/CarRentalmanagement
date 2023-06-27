using CRMCar;
using Serilog;

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("logs/crmCar/log-.log", 
//        outputTemplate: "{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}",
//        rollingInterval:RollingInterval.Day)
//    .MinimumLevel.Debug()
//    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
_ = builder.Host.UseSerilog((httpHost, config) =>
 _ = config.ReadFrom.Configuration(builder.Configuration));
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectString");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CRMCar.API", Version = "v2" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Configuration.GetValue<bool>("useSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Car car = new Car();
car.carInfo();

app.Run();
