namespace APIPuertos.Entidades
{
    public class SegundoDto
    {
        public int id { get; set; }
        public string maquinaria {  get; set; } = null!;

        public decimal operacion { get; set; }

        public DateTime fechaCreacion { get; set; } = DateTime.Now;

        public DateTime fechaActualizacion { get; set; } = DateTime.Now;

        public List<Muelle> muelles { get; set; } = null!;


    }
}
