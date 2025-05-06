using APIPuertos.Entidades;

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


//Fin Area de los servicios
var app = builder.Build();

//Inicio de area de los middlewares
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseOutputCache();

app.MapGet("/", () => "Hello World!");

app.MapGet("puertos", () =>
{
    var puertos = new List<Puerto>
    {
        new Puerto
        {
            id= 1,
            puerto= "puerto 1",
            ciudad= "Bogota",
            personaEntrevistada= "Diego",
            cargo= "Ingeniero",
            personaContacto= "Elkin",
            cedula= "12345",
            correo= "elkin.escobar@upit.gov.co",
            telefono= "3003358741"
        }

    };
    return puertos;
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15))); //Configura el cache solo para este endpoing t para 15 segundos
//Fin de area de los middlewares
app.Run();
