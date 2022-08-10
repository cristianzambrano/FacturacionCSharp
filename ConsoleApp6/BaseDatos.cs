using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;


namespace ConsoleApp6
{
    internal class BaseDatos
    {
        public String NombreBD;
        public String PathBD;
        OleDbConnection dbcon;
        String cadenaconexion;
        OleDbCommand cmd;
        OleDbDataAdapter da;

        public BaseDatos(String _NombreBD, String _PathBD="")
        {
            NombreBD = _NombreBD;
            PathBD = _PathBD;
            cadenaconexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + PathBD  + NombreBD + ".mdb";
            dbcon = new OleDbConnection(cadenaconexion);
        }

        public void AbrirConexion()
        {
            try
            {
                dbcon.Open();
            }
            catch (OleDbException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(0);

            }
            
        }
        public void CerrarConexion()
        {
            dbcon.Close();
        }

        //Insertar, Eliminar y Actualizar Datos
        public int ejecutarComando(String SQL )
        {
            try
            {
                cmd = dbcon.CreateCommand();
                cmd.CommandText = SQL;
                return cmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                return -1;
            }
        }

        //Consultar Datos
        public DataTable getDatosTB(String SQL)
        {
            da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = new OleDbCommand(SQL, dbcon);
            da.Fill(ds);
            return ds.Tables[0];

        }


    }
}
