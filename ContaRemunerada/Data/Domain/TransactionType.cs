using System.ComponentModel.DataAnnotations;

namespace ContaRemunerada.Data.Domain
{
    public enum TransactionType
    {
        [Display(Name = "Depósito")]
        Deposit = 0,
        [Display(Name = "Resgate")]
        Whitdraw = 1,
        [Display(Name = "Pagamento")]
        Payment = 2,
        [Display(Name = "Remuneração")]
        Yield = 3
    }
}