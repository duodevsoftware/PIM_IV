using Microsoft.AspNetCore.Connections;
using PIM_IV.Models;

namespace PIM_IV.Infra
{
    public class FuncionarioCompletoRepository : IFuncionarioCompleto
    {
        private readonly ConnectionContext _conectionContext = new ConnectionContext();

        public FuncionarioCompletoRepository(ConnectionContext connectionContext)
        {
            _conectionContext = connectionContext;
        }

        public void AddFuncionarioCompleto(FuncionarioModel funcionario, ContatoModel contatoFuncionario, EnderecoModel enderecoFuncionario, DependentesModel dependentes, RecursosHumanosModel recursosHumanos)
        {
            using (var transaction = _conectionContext.Database.BeginTransaction())
            {
                try
                {
                    _conectionContext.FuncionarioModel.Add(funcionario);
                    _conectionContext.SaveChanges();

                    _conectionContext.ContatoModel.Add(contatoFuncionario);
                    _conectionContext.SaveChanges();

                    _conectionContext.EnderecoModel.Add(enderecoFuncionario);
                    _conectionContext.SaveChanges();

                    _conectionContext.DependentesModel.Add(dependentes);
                    _conectionContext.SaveChanges();

                    _conectionContext.RecursosHumanosModel.Add(recursosHumanos);
                    _conectionContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var innerException = ex.InnerException;

                    while (innerException != null)
                    {
                        // Registre os detalhes da exceção interna
                        Console.WriteLine($"Inner Exception Message: {innerException.Message}");
                        Console.WriteLine($"Inner Exception Stack Trace: {innerException.StackTrace}");

                        innerException = innerException.InnerException;
                    }
                    
                    Console.WriteLine($"Erro ao adicionar funcionário: {ex.Message}");
                }
            }
        }

        public List<ContatoModel> GetContatoModel()
        {
            return _conectionContext.ContatoModel.ToList();
        }

        public List<DependentesModel> GetDependentesModel()
        {
            return _conectionContext.DependentesModel.ToList();
        }

        public List<EnderecoModel> GetEnderecoModel()
        {
            return _conectionContext.EnderecoModel.ToList();
        }

        public List<FuncionarioModel> GetFuncionarioModel()
        {
            return _conectionContext.FuncionarioModel.ToList();
        }

        public List<RecursosHumanosModel> GetRecursosHumanosModel()
        {
            return _conectionContext.RecursosHumanosModel.ToList();
        }
    }
}
