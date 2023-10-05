using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_IV.Models
{
    [Table("dependentes")]
    public class DependentesModel
    {
        [Key]
        public int id_dependentes {  get; set; }
        public string cpf_dependentes { get; set; }
        public string parentesco {  get; set; }
        public string nome { get; set; }

        public DependentesModel()
        {

        }

        public DependentesModel(int id_dependentes, string cpf_dependentes, string parentesco, string nome)
        {
            this.id_dependentes = id_dependentes;
            this.cpf_dependentes = cpf_dependentes;
            this.parentesco = parentesco;
            this.nome = nome;
        }
    }
}
