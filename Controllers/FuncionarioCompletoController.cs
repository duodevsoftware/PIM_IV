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

        public FuncionarioCompletoController(IFuncionarioCompleto funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
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
    }
}
