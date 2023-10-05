using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_IV.Models
{
    [Route("api/funcionarioCompleto")]
    public class FuncionarioCompletoModel
    {
        //Tabela Funcionario
        public int id_funcionario { get; set; }
        public bool funcionario_ativo { get; set; }
        public string nome_funcionario { get; set; }
        public string sobrenome_funcionario { get; set; }
        public string cpf_funcionario { get; set; }
        public string rg_funcionario { get; set; }
        public string pis { get; set; }
        public string reservista { get; set; }
        public DateTime? data_nascimento_funcionario { get; set; }
        public int idade_funcionario { get; set; }
        public string sexo_funcionario { get; set; }
        public string foto_funcionario { get; set; }
        public string carteira_trabalho_funcionario { get; set; }

        //tabela Contato
        public int id_contato { get; set; }
        public string cpf_contato { get; set; }
        public string tipo_contato { get; set; }
        public string ddd { get; set; }
        public string numero_telefone { get; set; }
        public string email { get; set; }

        //Tabela Endereco
        public int id_endereco { get; set; }
        public string cpf_endereco { get; set; }
        public string tipo_endereco { get; set; }
        public string logradouro { get; set; }
        public string nome_residencial { get; set; }
        public int numero_residencial { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }

        //Tabela Dependentes
        public int id_dependentes { get; set; }
        public string cpf_dependentes { get; set; }
        public string parentesco { get; set; }
        public string nome { get; set; }

        //Tabela Recursos Humanos
        public int id_rh { get; set; }
        public string cpf_rh { get; set; }
        public DateOnly? data_admissao { get; set; }
        public bool convenio_medico { get; set; }
        public bool convenio_odontologico { get; set; }
        public string cargo { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public float salario { get; set; }


    }
}
