using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_IV.Models
{
    [Table("contato")]
    public class ContatoModel
    {
        [Key]
        public int id_contato { get; set; }
        public string cpf_contato { get; set; }
        public string tipo { get; set; }
        public string ddd { get; set; }
        public string numero_telefone { get; set; }
        public string email { get; set; }

        public ContatoModel()
        {

        }

        public ContatoModel(int id_contato, string cpf_contato, string tipo, string ddd, string numero_telefone, string email)
        {
            this.id_contato = id_contato;
            this.cpf_contato = cpf_contato;
            this.tipo = tipo;
            this.ddd = ddd;
            this.numero_telefone = numero_telefone;
            this.email = email;
        }
    }
}
