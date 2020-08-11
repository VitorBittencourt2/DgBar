using System;

namespace BarDG.Dominio
{
    class Program
    {
        static void Main(string[] args)
        {
            var itemSuco1 = new Item(1, "Suco", 50, 1);
            var itemSuco2 = new Item(1, "Suco", 50, 3);
         
            var itemAgua1 = new Item(3, "Agua", 70, 1);
            var itemCerveja = new Item(4, "Cerveja", 5, 5); //bug na quantidade
        
            var comanda = new Comanda();

            comanda.RegistrarItem(itemCerveja);
            comanda.RegistrarItem(itemAgua1);
            comanda.RegistrarItem(itemSuco2);
            comanda.RegistrarItem(itemSuco1);
                 
            Console.WriteLine($"Total de itens: {comanda.Itens.Count}");
            Console.ReadLine();

            comanda.FechamentoComanda(itemSuco2);

            comanda.ResetaComanda();

            Console.WriteLine($"\nTotal, depois de limpar: {comanda.Itens.Count}");
        }
    }
}
