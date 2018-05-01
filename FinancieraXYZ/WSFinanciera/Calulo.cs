using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// clase que nos servira para obtener el monto que pagara
namespace WSFinanciera
{
    public class Calulo
    {
        public double getCuota(double monto, int plazo, double interes)
        {
            // formula para calcular el pago que realizara en cada plazo
            return monto * ((Math.Pow(1 + interes, plazo) * interes) / (Math.Pow(1 + interes, plazo) - 1));
        }
    }
}