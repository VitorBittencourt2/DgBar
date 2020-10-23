using BarDG.Dominio.Dominio.Restritores;
using System;
using System.Collections.Generic;
using BarDG.Dominio.Dominio.Contratos;
using BarDG.Dominio.Dominio;
using BarDG.Dominio.Dominio.Enum;

namespace BarDG.Dominio
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Bar DG \n ");

            var itensRestritos = new Dictionary<int, int>
            {
                {3, 3},
            };

            var restricoes = new List<IRestritor> { 
                new RestritorDeQuantidade(itensRestritos) 
            };

            IDefinidorDePromocoes executor = new ExecutorDePromocaoSimples();

            var itensQuantidade= new Dictionary<int, int> //Falta arrumar 
            {
                {1,5},
                {4,2}

            };

            var tipoDeDesconto = TipoDeDesconto.Valor;
            var idItemDesconto = 5;
            var valorDesconto = 5;

            var promocoes = new List<Promocao>(){
                 new ComboItemDescontoPromocao(
                     itensQuantidade,
                     tipoDeDesconto,
                     idItemDesconto,
                     valorDesconto,
                     id: 1,
                     nome: "Combo")
             };


            var c = new Comanda(restricoes, executor, promocoes);

            var itemCerveja = new Item(id: 1, nome: "cerveja", valor:5, quantidade: 2);
            var itemCerveja1 = new Item(id: 5, nome: "cerveja", valor: 5, quantidade: 3);
            var itemSuco = new Item(id: 3, nome: "suco", valor:50, quantidade: 1);
            var itemAgua = new Item(id:2,nome:"agua",valor:70, quantidade:1);
            var itemConhaque = new Item(id:4,nome:"conhaque",valor:20, quantidade:3);

            System.Console.WriteLine($"Incluindo novo item");
            c.RegistrarItem(itemCerveja);
            System.Console.WriteLine($"Incluindo novo item");
            c.RegistrarItem(itemCerveja1);
           

            System.Console.WriteLine($"Incluindo novo item");
            c.RegistrarItem(itemConhaque);

            System.Console.WriteLine($"Incluindo novo item");
            c.RegistrarItem(itemSuco);
            System.Console.WriteLine($"Incluindo novo item");
            c.RegistrarItem(itemSuco);
            System.Console.WriteLine($"Incluindo novo item");
            c.RegistrarItem(itemSuco);


            System.Console.WriteLine("Valor sem desconto: "+c.CalcularValorSemDescontos());
            System.Console.WriteLine("\nFechamento da comanda: ");
            c.FechamentoComanda();

            System.Console.WriteLine($"Total de itens: {c.Itens.Count}");

            System.Console.ReadKey();

            c.ResetaComanda();

            Console.WriteLine($"\nTotal, depois de limpar: {c.Itens.Count}");

            /*A cada 5 cervejas compradas, o cliente pagará 4 cervejas.
             -- Se o cliente comprar 3 conhaques mais 2 cervejas, poderá pedir uma água de graça.
             -- Só é permitido 3 sucos por comanda.*/

        }
    }
}
