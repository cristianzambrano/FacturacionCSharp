using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp6
{
    internal class BaseDatosProductos
    {

        Consola consola;
        BaseDatos objBD;

        public BaseDatosProductos(Consola _consola, BaseDatos _objBD)
        {
            objBD = _objBD;
            this.consola = _consola;
        }

        ~BaseDatosProductos()
        {
            /*foreach (Producto itemProducto in bdProductos)
            {
                bdProductos.Remove(itemProducto);
            }*/

        }
        public void actualizarPrecioProducto()
        {
            Console.Clear();
            consola.Escribir(30, 1, ConsoleColor.Yellow, "ACTUALIZAR PRECIO PRODUCTO");
            consola.Escribir(10, 3, ConsoleColor.Blue, "Ingrese el código del producto a actualizar: ");
            string codProucto = consola.leerCadena(70, 3);

            Producto itemEncontrado = buscarProducto(codProucto);
            if (itemEncontrado != null)
                itemEncontrado.actualizarPrecio();
            else
            {
                consola.Escribir(10, 5, ConsoleColor.Red, "Producto NO encontrado!!");
                Console.Read();
            }
        }

        public void eliminarProducto()
        {
            Console.Clear();
            consola.Escribir(30, 1, ConsoleColor.Yellow, "ELIMINAR PRODUCTO");
            consola.Escribir(10, 3, ConsoleColor.Blue, "Ingrese el código del producto a eliminar: ");
            string codProucto = consola.leerCadena(70, 3);

            Producto itemEncontrado = buscarProducto(codProucto);
            if (itemEncontrado != null)
            {
                if(itemEncontrado.eliminarProductoBD()>0)
                    consola.Escribir(20, 5, ConsoleColor.Blue, "Producto eliminado correctamente!!");
                else
                    consola.Escribir(20, 5, ConsoleColor.Red, "Error al Eliminar Producto");

                Console.Read();
            }
            else
            {
                consola.Escribir(10, 5, ConsoleColor.Red, "Producto NO encontrado!!");
                Console.Read();
            }
        }

        public void mostrarProductoTabla()
        {
            List<Producto> listaProductos = getBDListaProductos();
            for (int i = 0; i < listaProductos.Count; i += 5)
            {
                Console.Clear();
                listaProductos.ElementAt(i).mostrarMembreteTabla();

                for (int j = i; (j < listaProductos.Count && j - i < 5); j++)
                {
                    listaProductos.ElementAt(j).mostrarInfoComoFila(j, j - i + 7);
                }

                Console.ReadLine();
            }


        }

        public List<Producto> getBDListaProductos()
        {
            List<Producto> lista = new List<Producto>();
            DataTable tb = objBD.getDatosTB("Select * from tbProductos order by codigo");
            foreach (DataRow registro in tb.Rows)
            {
                lista.Add(new Producto(consola, objBD, registro));
            }

            return lista;
        }

        public void mostrarProductoxProducto()
        {
            Console.Clear();
            List<Producto> listaProductos = getBDListaProductos();
            foreach (Producto itemProducto in listaProductos)
            {
                Console.Clear();
                itemProducto.mostrarInfoProducto();
                Console.ReadLine();
            }
        }
        
        public void crearProducto()
        {
            Console.Clear();
            Producto producto = new Producto(consola, objBD);
            producto.leerInfoProducto();
            if(producto.guardarProductoBD() >0)
                consola.Escribir(20, 13, ConsoleColor.Blue, "Su producto se registró correctamente!!");
            else
                consola.Escribir(20, 13, ConsoleColor.Red, "Error al registrar producto");

            Console.ReadLine();
        }

        public Producto buscarProducto(String Codigo)
        {
            List<Producto> lista = new List<Producto>();
            DataTable tb = objBD.getDatosTB("Select * from tbProductos where Codigo='" + Codigo + "'");
            if (tb.Rows.Count > 0)
                return new Producto(consola, objBD, tb.Rows[0]);
            else
                return null;


        }
        /*public int getTotalProductos()
        {
            return bdProductos.Count;
        }*/

       
    }
}
