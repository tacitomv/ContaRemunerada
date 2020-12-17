using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaRemunerada.Data.Domain
{
    public class Transaction : Entity
    {
        public decimal Value { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public long AccountId { get; set; }
        public string Description { get; set; }
    }
}
