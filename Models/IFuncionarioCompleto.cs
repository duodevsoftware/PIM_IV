namespace PIM_IV.Models
{
    public interface IFuncionarioCompleto
    {
        void FuncionarioCompleto(FuncionarioModel funcionario, EnderecoModel endereco, ContatoModel contato, DependentesModel dependentes, RecursosHumanosModel recursosHumanos);

        List<FuncionarioModel> GetFuncionarioModel();
        List<EnderecoModel> GetEnderecoModel();
        List<ContatoModel> GetContatoModel();
        List<DependentesModel> GetDependentesModel();
        List<RecursosHumanosModel> GetRecursosHumanosModel();
    }
}
