using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoBlue.Helpers;
using ProjetoBlue.Middleware;
using ProjetoBlue.Profiles;
using ProjetoBlue.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"))); //conexão com o banco

builder.Services.AddScoped<IUsuarioService, UsuarioService>(); //controller 
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>(); //controller 

#region [cors]
    builder.Services.AddCors();
#endregion //cors 

var mapperConfig = MapperConfig.GetMapperConfig();
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region [cors]
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin(); 

});
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>(); //middleware

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
