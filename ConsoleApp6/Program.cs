using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Consola consola = new Consola(ConsoleColor.White);

            BaseDatos objBD = new BaseDatos("bdFacturacion");
            objBD.AbrirConexion();

            BaseDatosProductos bdProductos = new BaseDatosProductos(consola, objBD);

            int opcion;
            do
            {
                Console.Clear();
                consola.PintarFondo(ConsoleColor.Black);
                consola.MenuOpciones();
                opcion = consola.leerOpcion();
                switch (opcion)
                {
                    case 1:
                        bdProductos.crearProducto();
                        break;
                    case 2:
                        bdProductos.mostrarProductoxProducto();
                        break;
                    case 3:
                        bdProductos.actualizarPrecioProducto();
                        break;
                    case 4:
                        bdProductos.mostrarProductoTabla();
                        break;
                    case 5:
                        bdProductos.eliminarProducto();
                        break;
                    default:
                        Console.Clear();
                        consola.Escribir(50, 1, ConsoleColor.Yellow, "FIN DEL PROGRAMA");
                        bdProductos = null;
                        GC.Collect();
                        Console.Read();
                        break;

                }
            }
            while (opcion != 6);

            objBD.CerrarConexion();
        }
    }

   

  
   
}