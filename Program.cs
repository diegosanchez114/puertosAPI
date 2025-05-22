using APIPuertos.Entidades;
using APIPuertos.Repositorios;
using Microsoft.AspNetCore.OutputCaching;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;

//Inicio Area de los servicios
builder.Services.AddCors(opciones =>
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); //Permite todos los origenes y tambien los metodos Get, post, put y delete

    }));

builder.Services.AddOutputCache(); //Swervicio para utilizar cache desde el backend

builder.Services.AddEndpointsApiExplorer(); //Swagger explora los Endpoints
builder.Services.AddSwaggerGen(); //Añade el servicio de Swagger

builder.Services.AddScoped<IRepositorioPuertos, RepositorioPuertos>(); //Llamammos el IrepositorioPuertos con el RepositorioPuertos


//Fin Area de los servicios
var app = builder.Build();

//Inicio de area de los middlewares
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseOutputCache();

app.MapGet("/", () => "Hello World!");

app.MapGet("puertos", async(IRepositorioPuertos repositorio) =>
{
    return await repositorio.ObtenerTodos();
    
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("puertos-get")); //Configura el cache solo para este endpoing t para 15 segundos


app.MapGet("puertos/{id:int}", async (int id, IRepositorioPuertos repositorio) =>
{
    var puerto= await repositorio.ObtenerPorId(id);

    if (puerto is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(puerto);

}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)));

app.MapPost("/puertos", async (PuertoDto puerto, IRepositorioPuertos repositorioPuertos, IOutputCacheStore outputCacheStore) => 
{
    var id= await repositorioPuertos.CrearPuerto(puerto);
    await outputCacheStore.EvictByTagAsync("puertos-get", default);
    return TypedResults.Created($"/puertos/{id}", puerto);
});
//Fin de area de los middlewares
app.Run();
