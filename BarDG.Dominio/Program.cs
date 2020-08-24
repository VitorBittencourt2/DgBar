using System;


namespace BarDG.Dominio
{
    class Program
    {
        static void Main(string[] args)
        {
            var itemSuco1 = new Item(1, "Suco", 50, 1);
            var itemSuco3 = new Item(1, "Suco", 50, 1);
            var itemSuco2 = new Item(1, "Suco", 50, 1);
            var itemSuco4 = new Item(1, "Suco", 50, 1);

            var itemAgua1 = new Item(3, "Agua", 70, 1);
            var itemCerveja = new Item(4, "Cerveja", 5, 5);
            var itemConhaque = new Item(2, "Conhaque", 20, 3);

            var comanda = new Comanda();

         //   comanda.RegistrarItem(itemAgua1);
            comanda.RegistrarItem(itemCerveja);
            comanda.RegistrarItem(itemSuco1);
          //    comanda.RegistrarItem(itemSuco3);
           //   comanda.RegistrarItem(itemConhaque);
            //   comanda.RegistrarItem(itemSuco2);
            //     comanda.RegistrarItem(itemSuco4);

            

            Console.WriteLine($"Total de itens: {comanda.Itens.Count}");
            Console.ReadLine();

            comanda.FechamentoComanda();


            comanda.ResetaComanda();


            Console.WriteLine($"\nTotal, depois de limpar: {comanda.Itens.Count}");
        }
    }
}
