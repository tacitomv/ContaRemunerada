using System.ComponentModel.DataAnnotations;

namespace ContaRemunerada.Data.Domain
{
    public enum TransactionStatus
    {
        [Display(Name = "Concluída")]
        Completed = 0,
        [Display(Name = "Em análise")]
        BeingAnalized = 1,
        [Display(Name = "Negada")]
        Unauthorized = 2,
        [Display(Name = "Cancelada")]
        Canceled = 3
    }
}