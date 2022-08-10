using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp6
{
    internal class Producto
    {
        public String Codigo;
        public String Descripcion;
        public String Marca;
        public String Tipo;
        public double Precio;
        Consola consola;
        BaseDatos objBD;

        public Producto(Consola _consola, BaseDatos _objBD)
        {
            Precio = 0;
            this.consola = _consola;
            this.objBD = _objBD;
        }


        public Producto(Consola _consola, BaseDatos _objBD, String _Codigo,String _Descripcion,String _Marca, String _Tipo, Double _Precio)
        {
            this.consola = _consola;
            this.objBD = _objBD;

            this.Codigo = _Codigo;
            this.Descripcion = _Descripcion;
            this.Marca = _Marca;
            this.Tipo = _Tipo;
            this.Precio = _Precio;
        }

        public Producto(Consola _consola, BaseDatos _objBD, DataRow registro)
        {
            this.consola = _consola;
            this.objBD = _objBD;

            this.Codigo = registro["Codigo"].ToString();
            this.Descripcion = registro["Descripcion"].ToString();
            this.Marca = registro["Marca"].ToString();
            this.Tipo = registro["Tipo"].ToString();
            this.Precio = double.Parse(registro["Precio"].ToString());
        }

        public void mostrarMembreteTabla()
        {
            consola.Escribir(40, 2, ConsoleColor.Yellow, "LISTA DE PRODUCTOS");
            consola.Escribir(5, 5, ConsoleColor.Blue, "N°"); consola.Escribir(10, 5, ConsoleColor.Blue, "Código");
            consola.Escribir(25, 5, ConsoleColor.Blue, "Descripción"); consola.Escribir(50, 5, ConsoleColor.Blue, "Marca");
            consola.Escribir(65, 5, ConsoleColor.Blue, "Tipo"); consola.Escribir(80, 5, ConsoleColor.Blue, "Valor");
            consola.Marco(3, 4, 90, 15);
        }

        public void mostrarInfoComoFila(int Num, int fila)
        {
            consola.Escribir(5, fila, ConsoleColor.White, Num.ToString()); 
            consola.Escribir(10, fila, ConsoleColor.White, Codigo);
            consola.Escribir(25, fila, ConsoleColor.White, Descripcion); 
            consola.Escribir(50, fila, ConsoleColor.White, Marca);
            consola.Escribir(65, fila, ConsoleColor.White, Tipo);
            consola.Escribir(80, fila, ConsoleColor.White, Precio.ToString("0.00"));
        }
        public void actualizarPrecio()
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
        }
        public void mostrarInfoProducto()
        {
            
            consola.Escribir(30, 2, ConsoleColor.Red, "INFORMACIÓN DEL PRODUCTO");
            consola.Marco(10, 3, 65, 11);
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: "); consola.Escribir(35, 5, ConsoleColor.White, Codigo);
            consola.Escribir(20, 6, ConsoleColor.Yellow, "Descripción: "); consola.Escribir(35, 6, ConsoleColor.White, Descripcion);
            consola.Escribir(20, 7, ConsoleColor.Yellow, "Marca: "); consola.Escribir(35, 7, ConsoleColor.White, Marca);
            consola.Escribir(20, 8, ConsoleColor.Yellow, "Tipo: "); consola.Escribir(35, 8, ConsoleColor.White, Tipo);
            consola.Escribir(20, 9, ConsoleColor.Yellow, "Precio: "); consola.Escribir(35, 9, ConsoleColor.White, Precio.ToString("0.00"));
        }

        public void leerInfoProducto()
        {
            
            consola.Escribir(30, 2, ConsoleColor.Red, "NUEVO PRODUCTO");
            consola.Marco(10, 3, 65, 11);
            consola.Escribir(20, 5, ConsoleColor.Yellow, "Código: "); 
            consola.Escribir(20, 6, ConsoleColor.Yellow, "Descripción: "); 
            consola.Escribir(20, 7, ConsoleColor.Yellow, "Marca: "); 
            consola.Escribir(20, 8, ConsoleColor.Yellow, "Tipo: "); 
            consola.Escribir(20, 9, ConsoleColor.Yellow, "Precio: "); 
            Codigo = consola.leerCadena(35, 5);
            Descripcion = consola.leerCadena(35, 6);
            Marca = consola.leerCadena(35, 7);
            Tipo = consola.leerCadena(35, 8);
            Precio = consola.leerNumeroDecimal(35, 9);

        }

        public int guardarProductoBD()
        {
            String SQL = "Insert into tbProductos (Codigo, Descripcion, Marca, Tipo, Precio) values('"
                + Codigo + "','" + Descripcion + "','" + Marca + "','" + Tipo + "'," + Precio.ToString() + ");";
            return objBD.ejecutarComando(SQL);
        }

        public int eliminarProductoBD()
        {
            String SQL = "Delete from tbProductos where Codigo='" + Codigo + "'";
            return objBD.ejecutarComando(SQL);
        }


        public int actualizarPrecioProductoBD(double nuevoPrecio)
        {
            String SQL = "update tbProductos set precio=" + nuevoPrecio.ToString() + " where Codigo='" + Codigo + "'";
            return objBD.ejecutarComando(SQL);
        }


    }
}
