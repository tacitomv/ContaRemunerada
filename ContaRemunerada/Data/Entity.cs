using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContaRemunerada.Data
{
    public abstract class Entity
    {
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Display(Name = "Data de Criação")]
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "Última Atualização")]
        public DateTime? UpdatedAt { get; set; }
    }
}
