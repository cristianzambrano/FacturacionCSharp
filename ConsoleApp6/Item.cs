using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp6
{
    internal class Item
    {

        public String Codigo;
        public String Descripcion;
        public String TipoCodigo;
        public Consola consola;
        public BaseDatos objBD;

        public Item(Consola _consola, BaseDatos _objBD)
        {
            this.consola = _consola;
            this.objBD = _objBD;
        }
        
        public Item(Consola _consola, BaseDatos _objBD, String _Codigo,String _Descripcion, String _TipoCodigo)
        {
            this.consola = _consola;
            this.objBD = _objBD;

            this.Codigo = _Codigo;
            this.Descripcion = _Descripcion;
            this.TipoCodigo = _TipoCodigo;
            
        }


        public virtual Item creatItem (Consola _consola, BaseDatos _objBD, DataRow _registro)    {
            return new Item(_consola, _objBD);
        }
        public virtual Item creatItem(Consola _consola, BaseDatos _objBD)
        {
            return new Item(_consola, _objBD);
        }
        public virtual void mostrarMembreteTabla() { }
        public virtual void mostrarInfoComoFila(int Num, int fila) { }
        public virtual void mostrarInfo() { }
        public virtual void leerInfo() { }
        public virtual String getSQL(String TipoSQL, String CodigoABuscar = "") {            return "";        }

        public int guardarItemBD()
        {
            return objBD.ejecutarComando(getSQL("Insert"));
        }

        public int eliminarItemBD()
        {
            return objBD.ejecutarComando(getSQL("Delete", Codigo));
        }

        public DataTable busquedaItemBD(String Codigo)
        {
            return objBD.getDatosTB(getSQL("Select", Codigo));
        }


        public List<Item> getBDListaItems()
        {
            List<Item> lista = new List<Item>();
            DataTable tb = objBD.getDatosTB(getSQL("Select"));
            foreach (DataRow registro in tb.Rows)
            {
                lista.Add(creatItem(consola, objBD, registro));
            }

            return lista;
        }


    }
}
