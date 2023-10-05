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

        public void FuncionarioCompleto(FuncionarioModel funcionario, EnderecoModel endereco, ContatoModel contato, DependentesModel dependentes, 
            RecursosHumanosModel recursosHumanos)
        {
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
