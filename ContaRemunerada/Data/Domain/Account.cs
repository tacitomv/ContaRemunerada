using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ContaRemunerada.Data.Domain
{
    public class Account : Entity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        
        private bool HasFunds() => Value > 0;
        private bool HasEnoughFunds(decimal value) => HasFunds() && Value >= value;
        public void AddFunds(decimal value) => Value += value;
        public void RemoveFunds(decimal value)
        {
            if (HasEnoughFunds(value))
            {
                Value -= value;
            }
            else throw new NoFundsException("Não há fundos suficientes para realizar esta operação.");
        }

        public decimal GetFunds() => Value;
    }

    [Serializable]
    internal class NoFundsException : Exception
    {
        public NoFundsException()
        {
        }

        public NoFundsException(string message) : base(message)
        {
        }

        public NoFundsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoFundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
