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
        public int IDFactura;


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
            consola.Escribir(50, 2, ConsoleColor.Yellow, "LISTA DE FACTURAS");
            consola.Escribir(5, 5, ConsoleColor.Blue, "N°"); consola.Escribir(10, 5, ConsoleColor.Blue, "Código");
            consola.Escribir(40, 5, ConsoleColor.Blue, "Cliente"); consola.Escribir(60, 5, ConsoleColor.Blue, "Fecha");
            consola.Escribir(75, 5, ConsoleColor.Blue, "SubTotal"); consola.Escribir(85, 5, ConsoleColor.Blue, "IVA");
            consola.Escribir(95, 5, ConsoleColor.Blue, "TotalPagar");
            consola.Marco(3, 4, 110, 15);
        }

        public override void mostrarInfoComoFila(int Num, int fila)
        {
            consola.Escribir(5, fila, ConsoleColor.White, Num.ToString()); 
            consola.Escribir(10, fila, ConsoleColor.White, Codigo);
            consola.Escribir(30, fila, ConsoleColor.White, Cliente.Substring(0,25)); 
            consola.Escribir(60, fila, ConsoleColor.White, Fecha.ToString("dd/MM/yyyy"));
            consola.Escribir(73, fila, ConsoleColor.White, SubTotal.ToString("0.00"));
            consola.Escribir(83, fila, ConsoleColor.White, IVA.ToString("0.00"));
            consola.Escribir(93, fila, ConsoleColor.White, TotalPagar.ToString("0.00"));
        }
      

        public override void mostrarInfo()
        {
            
            consola.Escribir(25, 2, ConsoleColor.Red, "INFORMACIÓN DE LA FACTURA");
            consola.Marco(10, 3, 65, 12);
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: "); consola.Escribir(35, 5, ConsoleColor.White, Codigo);
            consola.Escribir(20, 6, ConsoleColor.Yellow, "Cliente: "); consola.Escribir(35, 6, ConsoleColor.White, Cliente);
            consola.Escribir(20, 7, ConsoleColor.Yellow, "Fecha: "); consola.Escribir(35, 7, ConsoleColor.White, Fecha.ToString("dd/MM/yyyy"));
            consola.Escribir(20, 8, ConsoleColor.Yellow, "SubTotal: "); consola.Escribir(35, 8, ConsoleColor.White, SubTotal.ToString("0.00"));
            consola.Escribir(20, 9, ConsoleColor.Yellow, "IVA: "); consola.Escribir(35, 9, ConsoleColor.White, IVA.ToString("0.00"));
            consola.Escribir(20, 10, ConsoleColor.Yellow, "TotalPagar: "); consola.Escribir(35, 10, ConsoleColor.White, TotalPagar.ToString("0.00"));
        }

        public void leerDetallefactura()
        {
            do { 
                Console.Clear();
                mostrarInfo();
                
                consola.Escribir(25, 13, ConsoleColor.Red, "PRODUCTOS DE LA FACTURA");
                consola.Marco(10, 14, 65, 25);
                consola.Escribir(11, 15, ConsoleColor.Blue, "Ingrese Código del producto: ");
                string codItem = consola.leerCadena(40, 15);

                DataTable tb =  objBD.getDatosTB("Select * from TbProductos where codigo='" + codItem +"'");
                if (tb.Rows.Count > 0)
                {
                    consola.Escribir(20, 17, ConsoleColor.Yellow, "Descripción: "); 
                    consola.Escribir(35, 17, ConsoleColor.White, tb.Rows[0]["Descripcion"].ToString());
                    consola.Escribir(20, 18, ConsoleColor.Yellow, "Marca: "); 
                    consola.Escribir(35, 18, ConsoleColor.White, tb.Rows[0]["Marca"].ToString());
                    consola.Escribir(20, 19, ConsoleColor.Yellow, "Precio: "); 
                    consola.Escribir(35, 19, ConsoleColor.White, tb.Rows[0]["Precio"].ToString());
                    consola.Escribir(20, 20, ConsoleColor.Yellow, "IVA: ");
                    consola.Escribir(35, 20, ConsoleColor.White, tb.Rows[0]["IVA"].ToString() + " %");
                    consola.Escribir(11, 22, ConsoleColor.Blue, "Ingrese Cantidad A Comprar: ");
                    double Cantidad= consola.leerNumeroDecimal(40, 22);

                    if (objBD.ejecutarComando("Insert into TbDetallefactura (IDEncabezadofact, IDProducto, Cantidad, PVP, IVA) values(" +
                           this.IDFactura +","+ tb.Rows[0]["ID"].ToString() +"," + Cantidad +"," + tb.Rows[0]["Precio"].ToString() 
                           + "," + tb.Rows[0]["IVA"].ToString() +");") > 0)
                    {
                        consola.Escribir(20, 24, ConsoleColor.Blue, "Se agregó el producto a la Factura");
                    }

                }
                else
                    consola.Escribir(20, 20, ConsoleColor.Red, "Producto NO Encontrado");

                consola.Escribir(11, 26, ConsoleColor.Blue, "¿Desea agregar otro producto? Si/No");
                string resp= consola.leerCadena(50, 26);
                if (resp.ToLower() == "no")
                    break;

            } while(true);

        }

        public Boolean leerInfoEncabezado(int IDFactura)
        {
            Cliente cliente = new Cliente(consola, objBD);

            consola.Escribir(30, 2, ConsoleColor.Red, "NUEVA FACTURA");
            consola.Marco(10, 3, 65, 11);
            consola.Escribir(30, 4, ConsoleColor.Blue, "SELECCIONE EL CLIENTE");
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: ");
            Codigo = consola.leerCadena(30, 5);
            DataTable tb = cliente.busquedaItemBD(Codigo);
            if (tb.Rows.Count > 0)
            {
                Console.Clear();
                consola.Escribir(30, 2, ConsoleColor.Red, "NUEVA FACTURA");
                consola.Marco(10, 3, 65, 12);
                consola.Escribir(30, 5, ConsoleColor.Blue, "DATOS DE LA FACTURA");
                consola.Escribir(20, 6, ConsoleColor.Yellow, "Fecha: ");
                consola.Escribir(20, 7, ConsoleColor.Yellow, "Cliente: ");
                consola.Escribir(20, 8, ConsoleColor.Yellow, "Establecimiento: ");
                consola.Escribir(20, 9, ConsoleColor.Yellow, "Sucursal: ");
                consola.Escribir(20, 10, ConsoleColor.Yellow, "Número: ");
                consola.Escribir(40, 6, ConsoleColor.White, DateTime.Now.ToString("dd/MM/yyyy"));
                consola.Escribir(40, 7, ConsoleColor.White, tb.Rows[0]["Nombres"].ToString());
                String Est = consola.leerCadena(40, 8,3);
                String Sur = consola.leerCadena(40, 9,3);
                String Num = consola.leerCadena(40, 10,9);

                this.Codigo = Est + Sur + Num;
                this.IDCliente = int.Parse(tb.Rows[0]["IdCliente"].ToString());
                this.Fecha = DateTime.Now;
                this.IDFactura = IDFactura;
                this.Cliente = tb.Rows[0]["Nombres"].ToString();
                this.SubTotal = 0;
                this.IVA = 0;
                this.TotalPagar = 0;

                if (guardarItemBD() > 0)
                {
                    consola.Escribir(20, 13, ConsoleColor.Blue, "Los datos de la factura se registraron correctamente!!");
                    return true;
                }
                else
                {
                    consola.Escribir(20, 13, ConsoleColor.Red, "Error al registrar  datos de la factura");
                    return false;
                }

            }
            else
            {
                consola.Escribir(32, 8, ConsoleColor.Red, "NO EXISTE EL CLIENTE");
                return false;
            }
            

        }


        public int getMaxID()
        {
            DataTable tb= objBD.getDatosTB("Select max(ID) as MaxID from TbEncabezadoFactura");
            if (tb.Rows.Count > 0)
            {
                DataRow registro = tb.Rows[0];
                if (registro["MaxID"] == null || registro["MaxID"].ToString()=="")
                {
                    return 1;
                }
                else
                {
                    int MaxID = int.Parse(tb.Rows[0]["MaxID"].ToString());
                    return MaxID + 1;
                }

            }
            else
                return 1;
        
        }
        public override String getSQL(String TipoSQL, String CodigoABuscar = "") 
        {
            String SQL="";
            switch (TipoSQL){
                case "Insert": 
                    SQL= "Insert into tbEncabezadoFactura (ID,Establecimiento, Sucursal, Numero, Fecha, IDCliente) values(" +
                          IDFactura+ ",'" + Codigo.Substring(0, 3) + "','" + Codigo.Substring(3, 3) + "','" + Codigo.Substring(6, 9) 
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
                    SQL=SQL + "GROUP BY c.Nombres, e.Fecha, e.Establecimiento, e.Sucursal, e.Numero, c.ID, e.ID;";
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
