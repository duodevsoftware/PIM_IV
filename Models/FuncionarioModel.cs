using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_IV.Models
{
    [Table("funcionario")]
    public class FuncionarioModel
    {
        [Key]
        public int id_funcionario { get; set; }
        public bool funcionario_ativo { get; set; }
        public string nome_funcionario { get; set; }
        public string sobrenome_funcionario { get; set; }
        public string cpf_funcionario { get; set; }
        public string rg_funcionario { get; set; }
        public string pis { get; set; }
        public string reservista { get; set; }
        public DateTime? data_nascimento_funcionario { get; set; }
        public int idade_funcionario {  get; set; }
        public string sexo_funcionario { get; set; }
        public string foto_funcionario { get; set; }
        public string carteira_trabalho_funcionario { get; set; }

        public FuncionarioModel()
        {

        }

        public FuncionarioModel(int id_funcionario, bool funcionario_ativo, string nome_funcionario, string sobrenome_funcionario, 
            string cpf_funcionario, string rg_funcionario, string pis, string reservista, DateTime data_nascimento_funcionario, int idade_funcionario, 
            string sexo_funcionario, string foto_funcionario, string carteira_trabalho_funcionario)
        {
            this.id_funcionario = id_funcionario;
            this.funcionario_ativo = funcionario_ativo;
            this.nome_funcionario = nome_funcionario;
            this.sobrenome_funcionario = sobrenome_funcionario;
            this.cpf_funcionario = cpf_funcionario;
            this.rg_funcionario = rg_funcionario;
            this.pis = pis;
            this.reservista = reservista;
            this.data_nascimento_funcionario = data_nascimento_funcionario;
            this.idade_funcionario = idade_funcionario;
            this.sexo_funcionario = sexo_funcionario;
            this.foto_funcionario = foto_funcionario = null;
            this.carteira_trabalho_funcionario = carteira_trabalho_funcionario;
        }
    }
}
