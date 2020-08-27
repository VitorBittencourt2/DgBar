using System;
using System.Collections.Generic;
using System.Linq;
using BarDG.Dominio.Contratos;

namespace BarDG.Dominio
{
    public class Comanda
    {
        public List<Item> Itens { get; private set; }

        public IEnumerable<IRestritor> Restritores { get; private set; }
        
        public Comanda(IEnumerable<IRestritor> restritores)
        {
            Restritores = restritores;            
            Itens = new List<Item>();
        }

        public void RegistrarItem(Item item)
        {
            foreach(var restritor in Restritores)
            {
                var ehValido = restritor.EhValido(this, item);

                if(!ehValido)
                    return ;
                else
                    Itens.Add(item);                
            }
        }

        public void FechamentoComanda() //Item item
        {
            double valorFinal = 0;
            double desconto = 0;
            double preco = 0;
            double descontoCerveja = 0;
            double descontoAgua = 0;


            var itemCerveja = Itens.FirstOrDefault(x => x.Nome == "Cerveja");
            var itemConhaque = Itens.FirstOrDefault(x => x.Nome == "Conhaque");
            var itemAgua = Itens.FirstOrDefault(x => x.Nome == "Agua");
            var itemSuco = Itens.FirstOrDefault(x => x.Nome == "Suco");

            
            if (itemCerveja != null)
            {
                // E se o cliente comprar 10 cervejas? Nesta lógica, ele teria apenas um desconto de cerveja e deveria ter 2;
                if (itemCerveja.Quantidade == 7)
                {
                    descontoCerveja = 5;
                }
            }

            else if (itemAgua != null)
            {
                // E se o cliente pedir 4 cervejas e 6 conhaques ?
                if (itemCerveja.Quantidade == 2 && itemConhaque.Quantidade == 3)
                {
                    descontoAgua = 70;
                }
            }

            foreach (Item it in Itens)
            {
                Console.WriteLine("item: " + it.Nome + "\nQuantidade:" + it.Quantidade);

                preco = it.Valor * it.Quantidade;
                // E se amanhã eu quiser incluir um produto novo e incluir ele nesta conta ?
                desconto = descontoCerveja + descontoAgua;
                valorFinal += preco - desconto;
            }
            Console.WriteLine("Desconto: " + desconto + "\nValorfinal: " + valorFinal);
        }

        public void ResetaComanda()
        {
            Itens.Clear();
        }
    }
}






