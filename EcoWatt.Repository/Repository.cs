using EcoWatt.Database;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace EcoWatt.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FIAPDBContext _context;

        private readonly DbSet<T> _dbSet;

        public Repository(FIAPDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity, int choice)
        {

            if (choice == 0)
            {
                var sqlCommand = "BEGIN inserir_ecowatt_usuario(:param1, :param2, :param3, :param4, :param5, :outputIdParam); END;";

                var dsUsuarioPropriedade = typeof(T).GetProperty("DsUsuario");
                var dsSenhaPropriedade = typeof(T).GetProperty("DsSenha");
                var dsNomeCompletoPropriedade = typeof(T).GetProperty("DsNomeCompleto");
                var dsDtCriacaoPropriedade = typeof(T).GetProperty("DtCriacao");
                var dsDtModificacaoPropriedade = typeof(T).GetProperty("DtModificacao");



                var dsUsuarioValor = dsUsuarioPropriedade != null ? dsUsuarioPropriedade.GetValue(entity) : null;
                var dsSenhaValor = dsSenhaPropriedade != null ? dsSenhaPropriedade.GetValue(entity) : null;
                var dsNomeCompletoValor = dsNomeCompletoPropriedade != null ? dsNomeCompletoPropriedade.GetValue(entity) : null;
                var dsDtCriacaoValor = dsDtCriacaoPropriedade != null ? dsDtCriacaoPropriedade.GetValue(entity) : null;
                var dsDtModificacaoValor = dsDtModificacaoPropriedade != null ? dsDtModificacaoPropriedade.GetValue(entity) : null;


                var param1 = new OracleParameter("param1", OracleDbType.Varchar2) { Value = dsUsuarioValor ?? DBNull.Value };
                var param2 = new OracleParameter("param2", OracleDbType.Varchar2) { Value = dsSenhaValor ?? DBNull.Value };
                var param3 = new OracleParameter("param3", OracleDbType.Varchar2) { Value = dsNomeCompletoValor ?? DBNull.Value };
                var param4 = new OracleParameter("param4", OracleDbType.Date) { Value = dsDtCriacaoValor ?? (object)DBNull.Value };
                var param5 = new OracleParameter("param5", OracleDbType.Date) { Value = dsDtModificacaoValor ?? (object)DBNull.Value };


                //Trecho de código serve para salvar a pk gerada pelo banco para exibir na tela
                var outputIdParam = new OracleParameter("outputIdParam", OracleDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(sqlCommand, param1, param2, param3, param4, param5, outputIdParam);

                var idPropriedade = typeof(T).GetProperty("IdUsuario");
                if (idPropriedade != null && outputIdParam.Value != DBNull.Value)
                {
                
                    OracleDecimal oracleDecimal = (OracleDecimal)outputIdParam.Value;
                    int idUsuario = oracleDecimal.IsNull ? 0 : oracleDecimal.ToInt32();
                    idPropriedade.SetValue(entity, idUsuario);
                }
            }

            else if (choice == 1)
            {
                var sqlCommand = "BEGIN inserir_ecowatt_baterias(:param1, :param2, :param3, :param4, :param5, :param6, :param7, :param8, :param9, :outputIdParam); END;";

                var idUsuarioPropriedade = typeof(T).GetProperty("IdUsuario");
                var dsTipoBateriaPropriedade = typeof(T).GetProperty("DsTipoBateria");
                var dsNomeConjuntoPropriedade = typeof(T).GetProperty("DsNomeConjunto");
                var vlCapacidadePropriedade = typeof(T).GetProperty("VlCapacidade");
                var vlQuantidadePropriedade = typeof(T).GetProperty("VlQuantidade");
                var dsStatusPropriedade = typeof(T).GetProperty("DsStatus");
                var dtDtUltimaLeituraPropriedade = typeof(T).GetProperty("DtUltimaLeitura");
                var dtCriacaoPropriedade = typeof(T).GetProperty("DtCriacao");
                var dsDtModificacaoPropriedade = typeof(T).GetProperty("DtModificacao");


                var idUsuarioValor = idUsuarioPropriedade?.GetValue(entity) ?? DBNull.Value;
                var dsTipoBateriaValor = dsTipoBateriaPropriedade?.GetValue(entity) ?? DBNull.Value;
                var dsNomeConjuntoValor = dsNomeConjuntoPropriedade?.GetValue(entity) ?? DBNull.Value;
                var vlCapacidadeValor = vlCapacidadePropriedade?.GetValue(entity) ?? DBNull.Value;
                var vlQuantidadeValor = vlQuantidadePropriedade?.GetValue(entity) ?? DBNull.Value;
                var dsStatusValor = dsStatusPropriedade?.GetValue(entity) ?? DBNull.Value;
                var dtDtUltimaLeituraValor = dtDtUltimaLeituraPropriedade?.GetValue(entity) ?? (object)DBNull.Value;
                var dtCriacaoValor = dtCriacaoPropriedade?.GetValue(entity) ?? (object)DBNull.Value;
                var dsDtModificacaoValor = dsDtModificacaoPropriedade != null ? dsDtModificacaoPropriedade.GetValue(entity) : null;


                var param1 = new OracleParameter("param1", OracleDbType.Int32) { Value = idUsuarioValor };
                var param2 = new OracleParameter("param2", OracleDbType.Varchar2) { Value = dsTipoBateriaValor };
                var param3 = new OracleParameter("param3", OracleDbType.Varchar2) { Value = dsNomeConjuntoValor };
                var param4 = new OracleParameter("param4", OracleDbType.Int32) { Value = vlCapacidadeValor };
                var param5 = new OracleParameter("param5", OracleDbType.Int32) { Value = vlQuantidadeValor };
                var param6 = new OracleParameter("param6", OracleDbType.Varchar2) { Value = dsStatusValor };
                var param7 = new OracleParameter("param7", OracleDbType.Date) { Value = dtDtUltimaLeituraValor ?? (object)DBNull.Value };
                var param8 = new OracleParameter("param8", OracleDbType.Date) { Value = dtCriacaoValor ?? (object)DBNull.Value };
                var param9 = new OracleParameter("param9", OracleDbType.Date) { Value = dsDtModificacaoValor ?? (object)DBNull.Value };


                //Trecho de código serve para salvar a pk gerada pelo banco para exibir na tela
                var outputIdParam = new OracleParameter("outputIdParam", OracleDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(sqlCommand, param1, param2, param3, param4, param5, param6, param7, param8, param9, outputIdParam);

                var idPropriedade = typeof(T).GetProperty("IdBateria");
                if (idPropriedade != null && outputIdParam.Value != DBNull.Value)
                {
                    OracleDecimal oracleDecimal = (OracleDecimal)outputIdParam.Value;
                    int idBateria = oracleDecimal.IsNull ? 0 : oracleDecimal.ToInt32();
                    idPropriedade.SetValue(entity, idBateria);
                }
            }
            else {

                await _context.AddAsync(entity);

                await _context.SaveChangesAsync();
            }
            
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(int id, T entity)
        {
            var existing = await _dbSet.FindAsync(id);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

        }
    }
}
