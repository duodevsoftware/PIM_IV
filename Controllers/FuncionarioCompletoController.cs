using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PIM_IV.Models;

namespace PIM_IV.Controllers
{
    [ApiController]
    [Route("api/funcionariosCompletos")]
    public class FuncionarioCompletoController : ControllerBase
    {
        private readonly IFuncionarioCompleto _funcionarioRepository;

        public FuncionarioCompletoController(IFuncionarioCompleto funcionarioCompleto)
        {
            _funcionarioRepository = funcionarioCompleto ?? throw new ArgumentNullException(nameof(funcionarioCompleto));
        }

        [HttpGet]
        public IActionResult BuscarTodos(string cpf = null)
        {
            var funcionarios = _funcionarioRepository.GetFuncionarioModel();
            var enderecos = _funcionarioRepository.GetEnderecoModel();
            var contatos = _funcionarioRepository.GetContatoModel();
            var dependentes = _funcionarioRepository.GetDependentesModel();
            var recursos_humanos = _funcionarioRepository.GetRecursosHumanosModel();

            var buscarFuncionario = from funcionario in funcionarios
                                    join endereco in enderecos on funcionario.cpf_funcionario equals endereco.cpf_endereco into enderecoGroup
                                    join contato in contatos on funcionario.cpf_funcionario equals contato.cpf_contato into contatosGroup
                                    join dependente in dependentes on funcionario.cpf_funcionario equals dependente.cpf_dependentes into dependentesGroup
                                    join recursosHumanos in recursos_humanos on funcionario.cpf_funcionario equals recursosHumanos.cpf_rh into rhGroup
                                    select new
                                    {
                                        funcionario = funcionario,
                                        endereco = enderecoGroup.ToList(),
                                        contato = contatosGroup.ToList(),
                                        dependentes = dependentesGroup.ToList(),
                                        recursosHumanos = rhGroup.ToList(),
                                    };

            if (!string.IsNullOrWhiteSpace(cpf))
            {
                buscarFuncionario = buscarFuncionario.Where(item => item.funcionario.cpf_funcionario == cpf);
            }

            var response = buscarFuncionario.ToList();
            return Ok(response);
        }

        // POST: api/funcionariosCompletos
        [HttpPost]
        public async Task<ActionResult<FuncionarioCompletoModel>> AddFuncionarioCompleto (FuncionarioCompletoModel funcionarioCompleto)
        {
            try
            {
                var funcionario = new FuncionarioModel(funcionarioCompleto.id_funcionario, funcionarioCompleto.funcionario_ativo, funcionarioCompleto.nome_funcionario, funcionarioCompleto.sobrenome_funcionario, funcionarioCompleto.cpf_funcionario,
                   funcionarioCompleto.rg_funcionario, funcionarioCompleto.pis, funcionarioCompleto.reservista, funcionarioCompleto.data_nascimento_funcionario, funcionarioCompleto.idade_funcionario, funcionarioCompleto.sexo_funcionario,
                   funcionarioCompleto.foto_funcionario, funcionarioCompleto.carteira_trabalho_funcionario);

                var contatoFuncionario = new ContatoModel(funcionarioCompleto.id_contato, funcionarioCompleto.cpf_contato, funcionarioCompleto.tipo_contato, funcionarioCompleto.ddd, funcionarioCompleto.numero_telefone, funcionarioCompleto.email);

                var enderecoFuncionario = new EnderecoModel(funcionarioCompleto.id_endereco, funcionarioCompleto.cpf_endereco, funcionarioCompleto.tipo_endereco, funcionarioCompleto.logradouro, funcionarioCompleto.nome_residencial, funcionarioCompleto.numero_residencial,
                   funcionarioCompleto.complemento, funcionarioCompleto.bairro, funcionarioCompleto.cep, funcionarioCompleto.cidade, funcionarioCompleto.estado);

                var dependentes = new DependentesModel(funcionarioCompleto.id_dependentes, funcionarioCompleto.cpf_dependentes, funcionarioCompleto.parentesco, funcionarioCompleto.nome);

                var recursosHumanos = new RecursosHumanosModel(funcionarioCompleto.id_rh, funcionarioCompleto.cpf_rh, funcionarioCompleto.data_admissao, funcionarioCompleto.convenio_medico, funcionarioCompleto.convenio_odontologico, funcionarioCompleto.cargo,
                   funcionarioCompleto.login, funcionarioCompleto.senha, funcionarioCompleto.salario);

                if (funcionario.data_nascimento_funcionario.HasValue)
                {
                    DateTime dataNascimento = funcionario.data_nascimento_funcionario.Value;
                    int idade = DateTime.Now.Year - dataNascimento.Year;

                    if (DateTime.Now < dataNascimento.AddYears(idade))
                    {
                        idade--;
                    }

                    funcionario.idade_funcionario = idade;
                }

                // Salvar os objetos no repositório
                _funcionarioRepository.AddFuncionarioCompleto(funcionario, contatoFuncionario, enderecoFuncionario, dependentes, recursosHumanos);

                // Retornar um status 201 Created com a URI para o novo recurso criado
                return CreatedAtAction("detalhes do funcionario", new { cpf = funcionario.cpf_funcionario }, funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar funcionário: {ex.Message}");
            }
        }
    }
}
