using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;



namespace Financiera
{
    public partial class Hoja1
    {
        //public int fila;
        public int filaF;
        //int columna;
        private void Hoja1_Startup(object sender, System.EventArgs e)
        {
          //  this.fila = 17;
          this.filaF = 0;
        }

        public Excel.Worksheet GetActiveWorkSheet()
        {
            return (Excel.Worksheet)Application.ActiveSheet;
        }


        private void Hoja1_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Código generado por el Diseñador de VSTO

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InternalStartup()
        {
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.Startup += new System.EventHandler(this.Hoja1_Startup);
            this.Shutdown += new System.EventHandler(this.Hoja1_Shutdown);

        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //Worksheet currentSheet = Globals.Hoja1.GetActiveWorkSheet();

            if (val_vacios())
            {
                if (!val_monto())
                {
                    MessageBox.Show("El monto debe ser un valor numerico mayor a 0");
                }
                else
                {
                    if (!val_plazo())
                    {
                        MessageBox.Show("El plazo debe ser un valor numerico y entero mayor a 0");
                    }
                    else
                    {
                        if (!val_interes())
                        {
                            MessageBox.Show("El interes debe ser un valor numerico mayor a 0");
                        }
                        else
                        {
                            double monto = Double.Parse(this.monto.Text);
                            int plazo = Int32.Parse(this.plazo.Text);
                            double interes = Double.Parse(this.interes.Text);
                            double couta;

                            //vamos a calcular el monto
                            using (WSFinanciera.Service1Client cliente = new WSFinanciera.Service1Client())
                            {
                                //calculamos la cuta que nos quedara
                                couta = Math.Round(cliente.getCuota(monto, plazo, interes), 4);

                                Worksheet currentSheet = Globals.Hoja1.GetActiveWorkSheet();
                                currentSheet.Range["C16"].Value = couta;
                                cliente.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();

            if (!val_monto())
            {
                MessageBox.Show("El monto debe ser un valor numerico mayor a 0");

            }
            else
            {
                if (!val_plazo())
                {
                    MessageBox.Show("El inters debe ser un valor numerico mayor a 0");
                }
                else
                {
                    if (!val_interes())
                    {
                        MessageBox.Show("El interes debe ser un valor numerico mayor a 0");
                    }
                    else
                    {
                        limpiar();
                        double monto = Double.Parse(this.monto.Text);
                        int plazo = Int32.Parse(this.plazo.Text);
                        double interes = Double.Parse(this.interes.Text);
                        double couta;
                        using (WSFinanciera.Service1Client cliente = new WSFinanciera.Service1Client())
                        {

                            //calculamos la cuta que nos quedara
                            couta = Math.Round(cliente.getCuota(monto, plazo, interes), 4);

                            Worksheet currentSheet = Globals.Hoja1.GetActiveWorkSheet();
                            currentSheet.Range["C16"].Value = couta;
                            //tamaño de la fila

                            //llenar la lista
                            var Datos = cliente.getTabla_Amortizacion(monto, plazo, interes);

                            filaF = Datos.Length;
                            int i = 17;

                            foreach (var dato in Datos)
                            {
                                currentSheet.Range["F" + i].Value = dato.periodo;
                                currentSheet.Range["F" + i].Borders.LineStyle = XlLineStyle.xlContinuous;

                                currentSheet.Range["G" + i].Value = dato.cuota;
                                currentSheet.Range["G" + i].Borders.LineStyle = XlLineStyle.xlContinuous;

                                currentSheet.Range["H" + i].Value = dato.interes;
                                currentSheet.Range["H" + i].Borders.LineStyle = XlLineStyle.xlContinuous;

                                currentSheet.Range["I" + i].Value = dato.abonoKs;
                                currentSheet.Range["I" + i].Borders.LineStyle = XlLineStyle.xlContinuous;

                                currentSheet.Range["J" + i].Value = dato.saldo;
                                currentSheet.Range["J" + i].Borders.LineStyle = XlLineStyle.xlContinuous;
                                i++;
                            }
                            //currentSheet.Columns.AutoFit();
                            cliente.Close();
                        }
                    }
                }
            }
        }

        public void limpiar()
        {
            Worksheet currentSheet = Globals.Hoja1.GetActiveWorkSheet();
            if(filaF != 0)
            {
               for (int j = 17; j <= 17+filaF; j++)
               {
                   currentSheet.Range["F" + j].Value = "";
                   currentSheet.Range["F" + j].Borders.LineStyle= XlLineStyle.xlLineStyleNone;

                   currentSheet.Range["G" + j].Value = "";
                   currentSheet.Range["G" + j].Borders.LineStyle = XlLineStyle.xlLineStyleNone;

                    currentSheet.Range["H" + j].Value ="";
                    currentSheet.Range["H" + j].Borders.LineStyle = XlLineStyle.xlLineStyleNone;

                    currentSheet.Range["I" + j].Value = "";
                    currentSheet.Range["I" + j].Borders.LineStyle = XlLineStyle.xlLineStyleNone;

                    currentSheet.Range["J" + j].Value = "";
                    currentSheet.Range["J" + j].Borders.LineStyle = XlLineStyle.xlLineStyleNone;
                }
            }
            //MessageBox.Show(filaF.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.monto.Text = "";
            this.interes.Text = "";
            this.plazo.Text = "";
            limpiar();
            Worksheet currentSheet = Globals.Hoja1.GetActiveWorkSheet();
            currentSheet.Range["C16"].Value = "";
            this.filaF = 0;
        }

        /*Para realizar validadiones*/

        /*Validando que las cajas este llenas*/
        public Boolean val_vacios()
        {
            string monto = this.monto.Text;
            string plazo = this.plazo.Text;
            string interes = this.interes.Text;
            if (!string.IsNullOrEmpty(monto) && !string.IsNullOrEmpty(plazo) && !string.IsNullOrEmpty(interes))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       public bool val_monto()
        {
            try
            {
                double monto = Double.Parse(this.monto.Text);
                if(monto > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool val_plazo()
        {
            try
            {
                int plazo = Int32.Parse(this.plazo.Text);
                if (plazo > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool val_interes()
        {
            try
            {
                double interes = Double.Parse(this.interes.Text);
                if (interes > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
