using Aplicacion.Core;
using Aplicacion.Services;
using Aplicacion.Services.BConocimiento;
using Aplicacion.Services.Tickets;
using Infraestructura.Context;
using Infraestructura.Core.DataBasesInfo;
using Infraestructura.Core.Identity;
using Infraestructura.Core.Jwtoken;
using Infraestructura.Core.RestClient;
using Microsoft.EntityFrameworkCore;
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

const string AllowAllOriginsPolicy = "AllowAllOriginsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllOriginsPolicy,
        x =>
        {
            x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

string conectionString = builder.Configuration.GetConnectionString("conectionDataBase");

builder.Services.AddDbContext<MyContext>(
        dbContextOption => dbContextOption.UseSqlServer(conectionString), ServiceLifetime.Transient
    );

builder.Services.AddTransient<IDataContext, MyContext>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Register Json Web Token
builder.Services.AddTransient<ITokenService, JwtTokenService>();


RestClientFactory.SetCurrent(new HttpRestClientFactory());
//builder.Services.AddTransient<IRestClient, HttpRestClient>();
//builder.Services.AddTransient<IRestClientFactory, HttpRestClientFactory>();
IdentityFactory.SetCurrent(new ADOIdentityGeneratorFactory());

builder.Services.AddScoped<SecurityAplicationService>();
builder.Services.AddScoped<TicketApplicationService>();
builder.Services.AddScoped<ArticuloAppService>();
builder.Services.AddScoped<CategoriaAppService>();

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
