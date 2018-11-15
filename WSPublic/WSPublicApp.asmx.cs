using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WSPublic
{
    /// <summary>
    /// Summary description for WSPublicApp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSPublicApp : System.Web.Services.WebService
    {

        

        [WebMethod]
        public string getTasaMoneda(string pCodigo)
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "";
                builder.Database = "wspublicos";

                MySqlConnection conn = new MySqlConnection(builder.ToString());
                MySqlCommand cmdDescripcion = conn.CreateCommand();
                MySqlCommand cmdValor = conn.CreateCommand();
                cmdDescripcion.CommandText = "select descripcion from tasacambio where cod_moneda = '" + pCodigo + "'";
                cmdValor.CommandText = "select valor_venta from tasacambio where cod_moneda = '" + pCodigo + "'";
                conn.Open();
                string pDescipcion = cmdDescripcion.ExecuteScalar().ToString();
                string pValorMoneda = cmdValor.ExecuteScalar().ToString();
                return pValorMoneda;
                //return "La descripcion de este codigo es '" + pDescipcion + "' Y el valor de la misma: '" + pValorMoneda + "'";

                ////SQL SERVER
                //SqlConnection con = new SqlConnection("Data Source=ALEXIS-PC;Initial Catalog=RegMat;Integrated Security=True");
                //con.Open();
                //string sql = "select sueldo_neto from Empleado where numero_documento = '" + pDocumento + "'";
                ////string sql = "select * from Empleado" ;
                //SqlCommand cmd = new SqlCommand(sql, con);
                //string pSalario = cmd.ExecuteScalar().ToString();
                //return pSalario;
            }
            catch (Exception e)
            {

                return "Este codigo no existe"; ;
            }
        }

        [WebMethod]
        public string getInflacionPeriodo(string pPeriodo)
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "";
                builder.Database = "wspublicos";

                MySqlConnection conn = new MySqlConnection(builder.ToString());
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select promedio_inflacion from indiceinflacion where fecha_perido = '" + pPeriodo + "'";
                conn.Open();
                string pInflacion = cmd.ExecuteScalar().ToString();
                return pInflacion;
            }
            catch (Exception e)
            {

                return "Intenta con el formato DD/MM/YYYY"; ;
            }
        }

        [WebMethod]
        public string getHistorialCrediticio(string pHistorial)
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "";
                builder.Database = "wspublicos";

                MySqlConnection conn = new MySqlConnection(builder.ToString());
                MySqlCommand cmdRNC = conn.CreateCommand();
                MySqlCommand cmdConceptoDeuda = conn.CreateCommand();
                MySqlCommand cmdMontoDeuda = conn.CreateCommand();
                //MySqlCommand cmdFecha = conn.CreateCommand();
                MySqlCommand cmdNombre = conn.CreateCommand();
                cmdRNC.CommandText = "select  rnc_empresa_adeudo from HistorialCrediticio history left join Cliente c on history.id_cliente = '" + pHistorial + "'";
                cmdConceptoDeuda.CommandText = "select concepto_deuda from HistorialCrediticio history left join Cliente c on history.id_cliente = '" + pHistorial + "'";
                cmdMontoDeuda.CommandText = "select  monto_deuda from HistorialCrediticio history left join Cliente c on history.id_cliente = '" + pHistorial + "'";
                cmdNombre.CommandText = "select  c.nombre from HistorialCrediticio history left join Cliente c on history.id_cliente = '" + pHistorial + "'";
                //cmdFecha.CommandText = "select fecha from HistorialCrediticio history left join Cliente c on history.id_cliente = '" + pHistorial + "'";
                conn.Open();
                string pRNC = cmdRNC.ExecuteScalar().ToString();
                string pConceptoDeuda = cmdConceptoDeuda.ExecuteScalar().ToString();
                string pMontoDeuda = cmdMontoDeuda.ExecuteScalar().ToString();
                //string pFecha = cmdFecha.ExecuteScalar().ToString();
                string pNombre = cmdNombre.ExecuteScalar().ToString();
                //return pRNC;
                return "Empresa deudante: " + pRNC + "  Concepto de deuda: " + pConceptoDeuda + " Monto Adeudado: " + pMontoDeuda + " Deudor: " + pNombre;

            }
            catch (Exception e)
            {

                return "Este ID de cliente no cuenta con prestamos en curso"; ;
            }
        }

        [WebMethod]
        public string getSaludFinanciera(string pSaludFinanciera)
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "";
                builder.Database = "wspublicos";

                MySqlConnection conn = new MySqlConnection(builder.ToString());
                MySqlCommand cmd = conn.CreateCommand();
                MySqlCommand cmdComentario = conn.CreateCommand();
                cmd.CommandText = "select  indicador from SaludFinanciera history left join Cliente c on history.id_cliente = '" + pSaludFinanciera + "'";
                cmdComentario.CommandText = "select  comentario from SaludFinanciera history left join Cliente c on history.id_cliente = '" + pSaludFinanciera + "'";
                conn.Open();
                string indicador = cmd.ExecuteScalar().ToString();
                string comentario = cmdComentario.ExecuteScalar().ToString();
                return "El score crediticio es: " + indicador + " Comentarios sobre el cliente: " + comentario;

            }
            catch (Exception e)
            {

                return "Este ID de cliente no cuenta con prestamos en curso"; 
            }
        }
    }
}
