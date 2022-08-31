using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp6
{
    internal class Factura:Item
    {
        public String Cliente;
        public int IDCliente;
        public DateTime Fecha;
        public double SubTotal;
        public double IVA;
        public double TotalPagar;

        public Factura(Consola _consola, BaseDatos _objBD) : base(_consola, _objBD, "000","Factura", "Número")     {        }

        public Factura(Consola _consola, BaseDatos _objBD, String _Codigo, int IDCliente,
            String Cliente, DateTime Fecha, double SubTotal, double IVA, double TotalPagar)
            :base(_consola, _objBD,_Codigo, "Factura", "Número")
        {
            this.Cliente = Cliente;
            this.IDCliente = IDCliente;
            this.Fecha = Fecha;
            this.SubTotal = SubTotal;
            this.IVA = IVA;
            this.TotalPagar = TotalPagar;
        }

        public override Item creatItem(Consola _consola, BaseDatos _objBD, DataRow _registro)
        {
            return new Factura(_consola, _objBD, _registro["Codigo"].ToString(),
                 int.Parse(_registro["IDCliente"].ToString()),
                _registro["Cliente"].ToString(), DateTime.Parse(_registro["Fecha"].ToString()),
                double.Parse(_registro["SubTotal"].ToString()), double.Parse(_registro["IVA"].ToString()),
                double.Parse(_registro["TotalPagar"].ToString()));
        }

        public override Item creatItem(Consola _consola, BaseDatos _objBD)
        {
            return new Factura(_consola, _objBD);
        }

        public override void mostrarMembreteTabla()
        {
            consola.Escribir(40, 2, ConsoleColor.Yellow, "LISTA DE FACTURAS");
            consola.Escribir(5, 5, ConsoleColor.Blue, "N°"); consola.Escribir(10, 5, ConsoleColor.Blue, "Código");
            consola.Escribir(40, 5, ConsoleColor.Blue, "Cliente"); consola.Escribir(60, 5, ConsoleColor.Blue, "Fecha");
            consola.Escribir(70, 5, ConsoleColor.Blue, "SubTotal"); consola.Escribir(80, 5, ConsoleColor.Blue, "IVA");
            consola.Escribir(90, 5, ConsoleColor.Blue, "TotalPagar");
            consola.Marco(3, 4, 105, 15);
        }

        public override void mostrarInfoComoFila(int Num, int fila)
        {
            consola.Escribir(5, fila, ConsoleColor.White, Num.ToString()); 
            consola.Escribir(10, fila, ConsoleColor.White, Codigo);
            consola.Escribir(30, fila, ConsoleColor.White, Cliente); 
            consola.Escribir(60, fila, ConsoleColor.White, Fecha.ToString("dd/MM/yyyy"));
            consola.Escribir(70, fila, ConsoleColor.White, SubTotal.ToString("0.00"));
            consola.Escribir(80, fila, ConsoleColor.White, IVA.ToString("0.00"));
            consola.Escribir(90, fila, ConsoleColor.White, TotalPagar.ToString("0.00"));
        }
      

        public override void mostrarInfo()
        {
            
            consola.Escribir(30, 2, ConsoleColor.Red, "INFORMACIÓN DE LA FACTURA");
            consola.Marco(10, 3, 65, 13);
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: "); consola.Escribir(35, 5, ConsoleColor.White, Codigo);
            consola.Escribir(20, 6, ConsoleColor.Yellow, "Cliente: "); consola.Escribir(35, 6, ConsoleColor.White, Cliente);
            consola.Escribir(20, 7, ConsoleColor.Yellow, "Fecha: "); consola.Escribir(35, 7, ConsoleColor.White, Fecha.ToString("dd/MM/yyyy"));
            consola.Escribir(20, 8, ConsoleColor.Yellow, "SubTotal: "); consola.Escribir(35, 8, ConsoleColor.White, SubTotal.ToString("0.00"));
            consola.Escribir(20, 9, ConsoleColor.Yellow, "IVA: "); consola.Escribir(35, 9, ConsoleColor.White, IVA.ToString("0.00"));
            consola.Escribir(20, 10, ConsoleColor.Yellow, "TotalPagar: "); consola.Escribir(35, 10, ConsoleColor.White, TotalPagar.ToString("0.00"));
        }

        public void leerInfoEncabezado()
        {

            Cliente cliente = new Cliente(consola, objBD);

            consola.Escribir(30, 2, ConsoleColor.Red, "NUEVA FACTURA");
            consola.Marco(10, 3, 65, 11);
            consola.Escribir(30, 4, ConsoleColor.Red, "SELECCIONE EL CLIENTE");
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: ");
            Codigo = consola.leerCadena(30, 5);
            DataTable tb = cliente.busquedaItemBD(Codigo);
            if (tb.Rows.Count > 0)
            {
                Console.Clear();
                consola.Escribir(30, 2, ConsoleColor.Red, "NUEVA FACTURA");
                consola.Marco(10, 3, 65, 11);
                consola.Escribir(30, 4, ConsoleColor.Red, "DATOS DE LA FACTURA");
                consola.Escribir(20, 5, ConsoleColor.Yellow, "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy"));
                consola.Escribir(20, 6, ConsoleColor.Yellow, "Cliente: " + cliente.Nombres);
                consola.Escribir(20, 7, ConsoleColor.Yellow, "Establecimiento: ");
                consola.Escribir(20, 8, ConsoleColor.Yellow, "Sucursal: ");
                consola.Escribir(20, 9, ConsoleColor.Yellow, "Número: ");
                

                String Est = consola.leerCadena(30, 7);
                String Sur = consola.leerCadena(30, 8);
                String Num = consola.leerCadena(30, 9);

                this.Codigo = Est + Sur + Num;
                this.IDCliente = cliente.IDCliente;
                this.Fecha = DateTime.Now;


            }
            else
            {
                consola.Escribir(32, 8, ConsoleColor.Red, "NO EXISTE EL CLIENTE");
            }
            

        }

        public override String getSQL(String TipoSQL, String CodigoABuscar = "") 
        {
            String SQL="";
            switch (TipoSQL){
                case "Insert": 
                    SQL= "Insert into Factura (Establecimiento, Sucursal, Numero, Fecha, IDCliente) values('"
                          + Codigo.Substring(0, 3) + "','" + Codigo.Substring(3, 3) + "','" + Codigo.Substring(6, 9) 
                          + "','" + Fecha.ToString("dd/MM/yyyy") + "'," + IDCliente + ");";
                    break;
                case "Delete":
                    SQL = "Delete from tbProductos where Codigo='" + CodigoABuscar + "'";
                    break;
                case "Select":
                    SQL = "SELECT (e.Establecimiento+e.Sucursal+e.Numero) AS Codigo, c.Nombres AS Cliente, C.ID as IDCliente," +
                    " e.Fecha, Sum(d.Cantidad*PVP) AS SubTotal, Sum(d.Cantidad*PVP*d.IVA) AS IVA," +
                    "Sum(d.Cantidad*PVP)+Sum(d.Cantidad*PVP*d.IVA) AS TotalPagar " +
                    "FROM tbEncabezadoFactura AS e, TbDetalleFactura AS d, TbClientes AS c " +
                    "WHERE e.ID =d.IDEncabezadoFact AND e.IDCliente=c.ID ";
                    if (CodigoABuscar != "")
                    {
                        SQL =SQL + " and F.Establecimiento='" + CodigoABuscar.Substring(0, 3) +
                             "' and F.Sucursal='" + CodigoABuscar.Substring(3, 3) + "'" +
                            " and F.Numero='" + CodigoABuscar.Substring(6, 9) + "'";
                    }
                    SQL=SQL + "GROUP BY c.Nombres, e.Fecha, e.Establecimiento, e.Sucursal, e.Numero;";
                    break;
              }

            return SQL;
        }



        /*   public int actualizarPrecioProductoBD(double nuevoPrecio)
           {
               String SQL = "update tbProductos set precio=" + nuevoPrecio.ToString() + " where Codigo='" + Codigo + "'";
               return objBD.ejecutarComando(SQL);
           }
        */

        /*public void actualizarPrecio()
      {
          Console.Clear();
          mostrarInfoProducto();
          consola.Escribir(20, 10, ConsoleColor.Red, "Nuevo Precio: ");
          double NuevoPrecio = consola.leerNumeroDecimal(35, 10);

          if (actualizarPrecioProductoBD(NuevoPrecio) > 0)
          {
              Precio = NuevoPrecio;
              consola.Escribir(20, 13, ConsoleColor.Blue, "Precio Actualizado! ");
          }
          else
          {
              consola.Escribir(20, 13, ConsoleColor.Blue, "Error al Actualizar precio! ");
          }

           Console.ReadLine();
      }*/

    }
}
