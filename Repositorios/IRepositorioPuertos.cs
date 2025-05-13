using APIPuertos.Entidades;

namespace APIPuertos.Repositorios
{
    public interface IRepositorioPuertos
    {
        Task<int> CrearPuerto(Puerto puerto);
        Task<Puerto?> ObtenerPorId(int id);
        Task<List<Puerto>> ObtenerTodos();
    }
}