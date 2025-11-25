using Aplicacion.Core;
using Infraestructura.Core.DataBasesInfo;
using WebServices.Configuraciones;
using WebServices.Jwtoken;
using WebServices.Middleware;

var builder = WebApplication.CreateBuilder(args);

DataBaseConnectionStrings.Initialize(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();


builder.ConfigureJwt();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddApplicationServices()
                .AddInfrastructure(builder.Configuration)
                .AddCorsPolicy();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
