using APIPuertos.Entidades;
using Dapper;
using Microsoft.Data.SqlClient;

namespace APIPuertos.Repositorios
{
    public class RepositorioPuertos : IRepositorioPuertos
    {
        private readonly string? connectionString;

        public RepositorioPuertos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<List<Puerto>> ObtenerTodos()
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var puertos = await conexion.QueryAsync<Puerto>(@"
                                            SELECT * FROM pueProyectos
                                            ");
                return puertos.ToList();
            }
        }

        public async Task<Puerto?> ObtenerPorId(int id)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var puerto = await conexion.QueryFirstOrDefaultAsync<Puerto>(@"
                                            SELECT * FROM pueProyectos WHERE id= @id
                                            ", new {id});
                return puerto;
            }
        }

        public async Task<int> CrearPuerto(PuertoDto dto)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var id = await conexion.QuerySingleAsync<int>(@"
                                        INSERT INTO pueProyectos (nombre, ciudad, personaEntrevistada, cargo, personaContacto, cedula, correo,
                                        telefono, areaUsoPublico, areausoAdyacente, fechaCreacion, fechaActualizacion) VALUES (@nombre, @ciudad, @personaEntrevistada, @cargo, @personaContacto, 
                                        @cedula, @correo, @telefono, @areaUsoPublico, @areausoAdyacente, @fechaCreacion, @fechaActualizacion);

                                        SELECT SCOPE_IDENTITY();
                                        ", dto.primero);

                                        foreach (var muelle in dto.segundo.muelles)
                                        {
                                            await conexion.ExecuteAsync(
                                                @"INSERT INTO pueMuelles (puertoId, tipoCargaPrimerSubsistema, muelleLongitud, muelleTipo, muelleCalado, muellePosiciones, muellePorcentajes, muelleRendimiento, maquinaria, operacion, fechaCreacion, fechaActualizacion)
				                                VALUES (@puertoId, @tipoCargaPrimerSubsistema, @muelleLongitud, @muelleTipo, @muelleCalado, @muellePosiciones, @muellePorcentajes, @muelleRendimiento, @maquinaria, @operacion, @fechaCreacion, @fechaActualizacion);",
                                                new { PuertoId = id, muelle.tipoCargaPrimerSubsistema, muelle.muelleLongitud, muelle.muelleTipo, muelle.muelleCalado, muelle.muellePosiciones, muelle.muellePorcentajes, muelle.muelleRendimiento, maquinaria= dto.segundo.maquinaria, operacion = dto.segundo.operacion, fechaCreacion = dto.segundo.fechaCreacion, fechaActualizacion = dto.segundo.fechaActualizacion });
                                        }
                dto.primero.id= id;
                return id;
            }            
        }
    }
}
