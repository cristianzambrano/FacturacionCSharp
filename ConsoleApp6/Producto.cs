using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp6
{
    internal class Producto:Item
    {
        public String DescripcionProducto;
        public String Marca;
        public String Tipo;
        public double Precio;
        public int IVA;

        public Producto(Consola _consola, BaseDatos _objBD) : base(_consola, _objBD, "000","Producto", "Código")
        {
            Precio = 0;
        }

        public Producto(Consola _consola, BaseDatos _objBD, String _Codigo,
            String _DescripcionProducto,String _Marca, String _Tipo, Double _Precio, int IVA)
            : base(_consola, _objBD, _Codigo, "Producto", "Código")
        {
            this.DescripcionProducto = _DescripcionProducto;
            this.Marca = _Marca;
            this.Tipo = _Tipo;
            this.Precio = _Precio;
            this.IVA = IVA;
        }

        public override Item creatItem(Consola _consola, BaseDatos _objBD, DataRow _registro)
        {
            return new Producto(_consola, _objBD, _registro["Codigo"].ToString(), 
                _registro["Descripcion"].ToString(), _registro["Marca"].ToString(),
                _registro["Tipo"].ToString(), double.Parse(_registro["Precio"].ToString()),
                int.Parse(_registro["IVA"].ToString()));
        }

        public override Item creatItem(Consola _consola, BaseDatos _objBD)
        {
            return new Producto(_consola, _objBD);
        }

        public override void mostrarMembreteTabla()
        {
            consola.Escribir(40, 2, ConsoleColor.Yellow, "LISTA DE PRODUCTOS");
            consola.Escribir(5, 5, ConsoleColor.Blue, "N°"); consola.Escribir(10, 5, ConsoleColor.Blue, "Código");
            consola.Escribir(25, 5, ConsoleColor.Blue, "Descripción"); consola.Escribir(50, 5, ConsoleColor.Blue, "Marca");
            consola.Escribir(65, 5, ConsoleColor.Blue, "Tipo"); consola.Escribir(80, 5, ConsoleColor.Blue, "Valor");
            consola.Escribir(90, 5, ConsoleColor.Blue, "IVA");
            consola.Marco(3, 4, 100, 15);
        }

        public override void mostrarInfoComoFila(int Num, int fila)
        {
            consola.Escribir(5, fila, ConsoleColor.White, Num.ToString()); 
            consola.Escribir(10, fila, ConsoleColor.White, Codigo);
            consola.Escribir(25, fila, ConsoleColor.White, DescripcionProducto); 
            consola.Escribir(50, fila, ConsoleColor.White, Marca);
            consola.Escribir(65, fila, ConsoleColor.White, Tipo);
            consola.Escribir(80, fila, ConsoleColor.White, Precio.ToString("0.00"));
            consola.Escribir(90, fila, ConsoleColor.White, IVA.ToString()+" %");
        }
      

        public override void mostrarInfo()
        {
            
            consola.Escribir(30, 2, ConsoleColor.Red, "INFORMACIÓN DEL PRODUCTO");
            consola.Marco(10, 3, 65, 12);
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: "); consola.Escribir(35, 5, ConsoleColor.White, Codigo);
            consola.Escribir(20, 6, ConsoleColor.Yellow, "Descripción: "); consola.Escribir(35, 6, ConsoleColor.White, DescripcionProducto);
            consola.Escribir(20, 7, ConsoleColor.Yellow, "Marca: "); consola.Escribir(35, 7, ConsoleColor.White, Marca);
            consola.Escribir(20, 8, ConsoleColor.Yellow, "Tipo: "); consola.Escribir(35, 8, ConsoleColor.White, Tipo);
            consola.Escribir(20, 9, ConsoleColor.Yellow, "Precio: "); consola.Escribir(35, 9, ConsoleColor.White, Precio.ToString("0.00"));
            consola.Escribir(20, 10, ConsoleColor.Yellow, "IVA: "); consola.Escribir(35, 10, ConsoleColor.White, IVA.ToString()+" %");
        }

        public override void leerInfo()
        {
            
            consola.Escribir(30, 2, ConsoleColor.Red, "NUEVO PRODUCTO");
            consola.Marco(10, 3, 65, 12);
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: "); 
            consola.Escribir(20, 6, ConsoleColor.Yellow, "Descripción: "); 
            consola.Escribir(20, 7, ConsoleColor.Yellow, "Marca: "); 
            consola.Escribir(20, 8, ConsoleColor.Yellow, "Tipo: "); 
            consola.Escribir(20, 9, ConsoleColor.Yellow, "Precio: ");
            consola.Escribir(20, 10, ConsoleColor.Yellow, "IVA: ");
            Codigo = consola.leerCadena(35, 5);
            Descripcion = consola.leerCadena(35, 6);
            Marca = consola.leerCadena(35, 7);
            Tipo = consola.leerCadena(35, 8);
            Precio = consola.leerNumeroDecimal(35, 9);
            IVA = consola.leerNumeroEntero(35, 10);

        }

        public override String getSQL(String TipoSQL, String CodigoABuscar = "") 
        {
            String SQL="";
            switch (TipoSQL){
                case "Insert": 
                    SQL= "Insert into tbProductos (Codigo, Descripcion, Marca, Tipo, Precio, IVA) values('"
                          + Codigo + "','" + Descripcion + "','" + Marca + "','" + Tipo + "'," 
                          + Precio.ToString() + "," + IVA.ToString() +");";
                    break;
                case "Delete":
                    SQL = "Delete from tbProductos where Codigo='" + CodigoABuscar + "'";
                    break;
                case "Select":
                    if (CodigoABuscar != "")
                    {
                        SQL = "Select * from tbProductos where codigo='" + CodigoABuscar + "'";
                    }
                    else
                    {
                        SQL = "Select * from tbProductos order by codigo";
                    }
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
