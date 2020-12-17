using ContaRemunerada.Data;
using ContaRemunerada.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaRemunerada.Services
{
    public class RatesService
    {
        public RatesService() { }

        /// <summary>
        /// Get CDI value
        /// Taxa fake de 2018 para gerar um resultado minimamente positivo.
        /// </summary>
        /// <returns></returns>
        public double GetCDI()
        {
            return 6.89;
        }
    }
}
