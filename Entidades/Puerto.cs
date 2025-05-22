using Microsoft.VisualBasic;

namespace APIPuertos.Entidades
{
    public class Puerto
    {
        public int id {  get; set; }
        public string nombre { get; set; } = null!;
        public string ciudad {  get; set; } = null!;
        public string personaEntrevistada { get; set; } = null!;
        public string cargo {  get; set; } = null!;
        public string personaContacto {  get; set; } = null!;
        public string cedula {  get; set; } = null!;
        public string correo { get; set; } = null!;
        public string telefono { get; set; } = null!;
        public decimal? areaUsoPublico { get; set; }= 0;
        public decimal? areaUsoAdyacente { get; set; } = 0!;

        public DateTime fechaCreacion { get; set; } = DateTime.Now;

        public DateTime fechaActualizacion { get; set; } = DateTime.Now;
    }
}
