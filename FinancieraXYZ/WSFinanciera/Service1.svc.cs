using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WSFinanciera
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public double getCuota(double monto, int plazo, double interes)
        {
            return new Calculo().getCuota(monto, plazo, interes);
        }
        public List<Amortizacion> getTabla_Amortizacion(double monto, int plazo, double interes)
        {
            int j = 0;
            // extraer la cuota
            double cuota = getCuota(monto, plazo, interes);
            //creamos la lista donde guardaremos cada elemento
            List<Amortizacion> tabla = new List<Amortizacion>();
            //agregamos el primer elemento
            tabla.Add(new Amortizacion
            {
                periodo = 0,
                cuota = 0,
                interes = 0,
                abonoKs = 0,
                saldo = monto
            });
            //variables para el control

            double interesT = 0;
            double abono_kT = 0;
            double saldoAnterior = monto;
            for (int i = 1; i <= plazo; i++)
            {
                interesT = saldoAnterior * interes;
                abono_kT = cuota - interesT;
                saldoAnterior = saldoAnterior - abono_kT;
                tabla.Add(new Amortizacion
                {
                    periodo = i,
                    cuota = cuota,
                    interes = interesT,
                    abonoKs = abono_kT,
                    saldo = saldoAnterior
                });
            }
            return tabla;
        }
    }
}
