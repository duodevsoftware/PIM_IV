using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_IV.Models
{
    [Table("recursos_humanos")]
    public class RecursosHumanosModel
    {
        [Key]
        public int id_rh {  get; set; }
        public string cpf_rh {  get; set; }
        public DateOnly? data_admissao { get; set; }
        public bool convenio_medico {  get; set; }
        public bool convenio_odontologico { get; set; }
        public string cargo { get; set; }
        public string login {  get; set; }
        public string senha { get; set; }

        [Column(TypeName = "double precision")]
        public float salario { get; set; }

        public RecursosHumanosModel()
        {

        }

        public RecursosHumanosModel(int id_rh, string cpf_rh, DateOnly? data_admissao, bool convenio_medico, bool convenio_odontologico, string cargo, string login, string senha, float salario)
        {
            this.id_rh = id_rh;
            this.cpf_rh = cpf_rh;
            this.data_admissao = data_admissao;
            this.convenio_medico = convenio_medico;
            this.convenio_odontologico = convenio_odontologico;
            this.cargo = cargo;
            this.login = login;
            this.senha = senha;
            this.salario = salario;
        }
    }
}
