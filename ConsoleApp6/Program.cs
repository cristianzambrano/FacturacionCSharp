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
        public static Consola consola;
        public static BaseDatos objBD;

        static void Main(string[] args)
        {
            consola = new Consola(ConsoleColor.White);
            objBD = new BaseDatos("bdFacturacion");
            objBD.AbrirConexion();

            int opcion;
            do
            {
                Console.Clear();
                consola.PintarFondo(ConsoleColor.Black);
                consola.MenuPrincipal();
                opcion = consola.leerOpcion();
                switch (opcion)
                {
                    case 1:
                        MenuItem(new Producto(consola, objBD));
                        break;
                    case 2:
                        MenuItem(new Cliente(consola, objBD));
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        MenuItem(new Empleado(consola, objBD));
                        break;
                    case 5:
                        
                        break;
                    default:
                        Console.Clear();
                        consola.Escribir(50, 1, ConsoleColor.Yellow, "FIN DEL PROGRAMA");
                        GC.Collect();
                        Console.Read();
                        break;

                }
            }
            while (opcion != 6);

            objBD.CerrarConexion();
        }

        static void MenuItem(Item item)
        {
            BaseDatosItems bdItems = new BaseDatosItems(consola, objBD, item);

            int opcion;
            do
            {
                Console.Clear();
                consola.PintarFondo(ConsoleColor.Black);
                consola.MenuItems(item.Descripcion);
                opcion = consola.leerOpcion();
                switch (opcion)
                {
                    case 1:
                        bdItems.crearItem();
                        break;
                    case 2:
                        bdItems.mostrarItemxItem();
                        break;
                    case 3:
                        // bdItems.actualizarPrecioProducto();
                        break;
                    case 4:
                        bdItems.mostrarItemTabla();
                        break;
                    case 5:
                        bdItems.eliminarItem();
                        break;
                }
            }
            while (opcion != 6);
        }
    }

   

  
   
}