namespace APIPuertos.Entidades
{
    public class Muelle
    {
        
        //public int puertoId { get; set; }
        public string tipoCargaPrimerSubsistema  { get; set; } = null!;
        public decimal muelleLongitud { get; set; }
        public string muelleTipo { get; set; } = null!;
        public decimal muelleCalado { get; set; }
        public int muellePosiciones { get; set; }
        public decimal muellePorcentajes { get; set; }
        public decimal muelleRendimiento { get; set; }
       

    }
}
