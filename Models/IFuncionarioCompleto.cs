namespace PIM_IV.Models
{
    public interface IFuncionarioCompleto
    {
        List<FuncionarioModel> GetFuncionarioModel();
        List<EnderecoModel> GetEnderecoModel();
        List<ContatoModel> GetContatoModel();
        List<DependentesModel> GetDependentesModel();
        List<RecursosHumanosModel> GetRecursosHumanosModel();
        void AddFuncionarioCompleto(FuncionarioModel funcionario, ContatoModel contatoFuncionario, EnderecoModel enderecoFuncionario, DependentesModel dependentes, RecursosHumanosModel recursosHumanos);
    }
}
