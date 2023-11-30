using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_IV.Models
{
    [Table("registro_ponto")]
    public class RegistroPontoModel
    {
        [Key]
        public int id_ponto {  get; set; }
        public string cpf_registro_ponto { get; set; }
        public DateTime? entrada {  get; set; }
        public DateTime? saida_almoco {  get; set; }
        public DateTime? volta_almoco { get; set; }
        public DateTime? saida { get; set; }
        public DateTime? entrada_extra {  get; set; }
        public DateTime? saida_extra {  get; set; }
        public DateTime data_registro { get; set; }
        
        public RegistroPontoModel()
        {

        }

        public RegistroPontoModel(int id_ponto, string cpf_registro_ponto, DateTime? entrada, DateTime? saida_almoco, DateTime? volta_almoco, DateTime? saida,
            DateTime? entrada_extra, DateTime? saida_extra, DateTime data_registro)
        {
            this.id_ponto = id_ponto;
            this.cpf_registro_ponto = cpf_registro_ponto;
            this.entrada = entrada;
            this.saida_almoco = saida_almoco;
            this.volta_almoco = volta_almoco;
            this.saida = saida;
            this.entrada_extra = entrada_extra;
            this.saida_extra = saida_extra;
            this.data_registro = data_registro;
        }
    }
}
