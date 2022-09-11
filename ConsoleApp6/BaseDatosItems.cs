using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp6
{
    internal class BaseDatosItems
    {

        Consola consola;
        BaseDatos objBD;
        Item item;

        public BaseDatosItems(Consola _consola, BaseDatos _objBD, Item _item)
        {
            objBD = _objBD;
            this.consola = _consola;
            this.item = _item;
        }

        ~BaseDatosItems()
        {
        }
       

        public void eliminarItem()
        {
            Console.Clear();
            consola.Escribir(30, 1, ConsoleColor.Yellow, "ELIMINAR " + item.Descripcion);
            consola.Escribir(10, 3, ConsoleColor.Blue, "Ingrese " + item.TipoCodigo + " de " + item.Descripcion + " a eliminar: ");
            string codItem = consola.leerCadena(70, 3);

            Item itemEncontrado = buscarItem(codItem);
            if (itemEncontrado != null)
            {
                if (itemEncontrado.eliminarItemBD() > 0)
                    consola.Escribir(20, 5, ConsoleColor.Blue, item.Descripcion + " eliminado correctamente!!");
                else
                    consola.Escribir(20, 5, ConsoleColor.Red, "Error al Eliminar " + item.Descripcion);

                Console.Read();
            }
            else
            {
                consola.Escribir(10, 5, ConsoleColor.Red, item.Descripcion + " NO encontrado!!");
                Console.Read();
            }
        }

        public void mostrarItemTabla()
        {
            List<Item> listaItems = item.getBDListaItems();
            for (int i = 0; i < listaItems.Count; i += 5)
            {
                Console.Clear();
                listaItems.ElementAt(i).mostrarMembreteTabla();

                for (int j = i; (j < listaItems.Count && j - i < 7); j++)
                {
                    listaItems.ElementAt(j).mostrarInfoComoFila(j, j - i + 7);
                }

                Console.ReadLine();
            }


        }


        public void mostrarItemxItem()
        {
            Console.Clear();
            List<Item> listaItems = item.getBDListaItems();
            foreach (Item itemdelalista in listaItems)
            {
                Console.Clear();
                itemdelalista.mostrarInfo();
                Console.ReadLine();
            }
        }

        public void crearItem()
        {
            Console.Clear();
            Item itemnuevo = item.creatItem(consola, objBD);
            itemnuevo.leerInfo();
            if (itemnuevo.guardarItemBD() > 0)
                consola.Escribir(20, 13, ConsoleColor.Blue, item.Descripcion + " se registró correctamente!!");
            else
                consola.Escribir(20, 13, ConsoleColor.Red, "Error al registrar " + item.Descripcion);

            Console.ReadLine();
        }

        public Item buscarItem(String Codigo)
        {
            DataTable tb = item.busquedaItemBD(Codigo);
            if (tb.Rows.Count > 0)
                return item.creatItem(consola, objBD, tb.Rows[0]);
            else
                return null;


        }

        public void crearFactura()
        {
            Console.Clear();
            Factura factura = new Factura(consola, objBD);
            int IDFactura = factura.getMaxID();
            if (factura.leerInfoEncabezado(IDFactura))
            {
                factura.leerDetallefactura();
            }
            else
            {

            }

            

            Console.ReadLine();
        }
        /*public int getTotalProductos()
        {
            return bdProductos.Count;
        }*/


        /*public void actualizarPrecioProducto()
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
       }*/
    }
}
