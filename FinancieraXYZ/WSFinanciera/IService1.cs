using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WSFinanciera
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        double getCuota(double monto, int plazo, double interes);
        [OperationContract]
        List<Amortizacion> getTabla_Amortizacion(double monto, int plazo, double interes);
 

    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.

 /********************************clase amortizacion ************************/
    //[DataContract]
    public class Amortizacion
    {

        [DataMember]
        public int periodo { get; set; }
        [DataMember]
        public double cuota { get; set; }
        [DataMember]
        public double interes { get; set; }
        [DataMember]
        public double abonoKs { get; set; }
        [DataMember]
        public double saldo { get; set; }
    }
/********************************************************************************************/


    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
