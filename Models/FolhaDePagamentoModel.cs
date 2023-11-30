using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PIM_IV.Models
{
    [Table("folha_pagamento")]
    public class FolhaDePagamentoModel
    {
        public FolhaDePagamentoModel() 
        {

        }
        [Key]
        public int id_pagamento { get; set; }
        public string cpf_folha_pagamento { get; set; }
        public DateTime data_referencia {  get; set; }
        public DateTime data_pagamento { get; set; }
        public decimal valor_descontos {  get; set; }
        public decimal salario_bruto { get; set; }
        public decimal salario_liquido { get; set; }
        public decimal inss { get; set; }
        public decimal irpf { get; set; }
        public decimal convenio_medico { get; set; }
        public decimal convenio_odontologico { get; set; }

    }
}
