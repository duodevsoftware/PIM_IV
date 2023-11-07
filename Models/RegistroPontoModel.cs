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
        public TimeSpan entrada {  get; set; }
        public TimeSpan? saida_almoco {  get; set; }
        public TimeSpan? volta_almoco { get; set; }
        public TimeSpan? saida { get; set; }
        public TimeSpan? entrada_extra {  get; set; }
        public TimeSpan? saida_extra {  get; set; }
        public DateTime data_registro { get; set; }
        
        public RegistroPontoModel()
        {

        }

        public RegistroPontoModel(int id_ponto, string cpf_registro_ponto, TimeSpan entrada, TimeSpan saida_almoco, TimeSpan volta_almoco, TimeSpan saida,
            TimeSpan entrada_extra, TimeSpan saida_extra, DateTime data_registro)
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
