using APIPuertos.Entidades;

namespace APIPuertos.Repositorios
{
    public interface IRepositorioPuertos
    {
        Task<int> CrearPuerto(PuertoDto dto);
        Task<Puerto?> ObtenerPorId(int id);
        Task<List<Puerto>> ObtenerTodos();
    }
}