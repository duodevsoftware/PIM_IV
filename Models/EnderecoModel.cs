using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_IV.Models
{
    [Table("endereco")]
    public class EnderecoModel
    {
        [Key]
        public int id_endereco {  get; set; }
        public string cpf_endereco { get; set; }
        public string tipo_endereco { get; set; }
        public string logradouro { get; set; }
        public string nome_residencial { get; set; }
        public int numero_residencial { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade {  get; set; }
        public string estado {  get; set; }

        public EnderecoModel()
        {

        }

        public EnderecoModel(int id_endereco, string cpf_endereco, string tipo_endereco, string logradouro, string nome_residencial, int numero_residencial, 
            string complemento, string bairro, string cep, string cidade, string estado)
        {
            this.id_endereco = id_endereco;
            this.cpf_endereco = cpf_endereco;
            this.tipo_endereco = tipo_endereco;
            this.logradouro = logradouro;
            this.nome_residencial = nome_residencial;
            this.numero_residencial = numero_residencial;
            this.complemento = complemento;
            this.bairro = bairro;
            this.cep = cep;
            this.cidade = cidade;
            this.estado = estado;
        }
    }
}
