using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace Financiera
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            double monto = Double.Parse(this.txtMonto.Text);
            int plazo = Int32.Parse(this.txtPlazo.Text);
            double interes = Double.Parse(this.txtInteres.Text);
            double couta;
            //vamos a calcular el monto
            using (WSFinanciera.Service1Client cliente = new WSFinanciera.Service1Client())
            {
                //calculamos la cuta que nos quedara
                couta = Math.Round(cliente.getCuota(monto, plazo ,interes),4);

                Worksheet currentSheet = Globals.Hoja1.GetActiveWorkSheet();
                currentSheet.Range["B4"].Value = couta;
                cliente.Close();
            }
        }
    }
}
