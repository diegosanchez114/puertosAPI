using APIPuertos.Entidades;

var builder = WebApplication.CreateBuilder(args);
//Inicio Area de los servicios



//Fin Area de los servicios
var app = builder.Build();

//Inicio de area de los middlewares



app.MapGet("/", () => "Hello World!");

app.MapGet("puertos", () =>
{
    var puertos = new List<Puerto>
    {
        new Puerto
        {
            id= 1,
            puerto= "puerto 1",
            personaEntrevistada= "Diego",
            cargo= "Ingeniero",
            personaContacto= "Elkin",
            cedula= "12345",
            correo= "elkin.escobar@upit.gov.co",
            telefono= "3003358741"
        }

    };
    return puertos;
});
//Fin de area de los middlewares
app.Run();
