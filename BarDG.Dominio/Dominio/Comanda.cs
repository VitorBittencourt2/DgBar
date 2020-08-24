using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDG.Dominio
{
    public class Comanda
    {
        public List<Item> Itens { get; private set; }

        public Comanda()
        {
            Itens = new List<Item>();
        }

        public void RegistrarItem(Item item)
        {
            var itensSuco = Itens.FirstOrDefault(x => x.Id == 1);
            // barrar inserir suco com mais de 3 de quantidade
            if (item.Id == 1 && item.Quantidade > 3)
                return;

            // barrar se eu já tenho suco na minha lista e a quantidade informada + quantidade excede o limite


            if (itensSuco != null)
            {
                var qtde = itensSuco.Quantidade + item.Quantidade;

                if (itensSuco.Quantidade > 3)
                {
                    Console.WriteLine("Quantidade de suco informada excede o limite de 3");
                    return;
                }
                else
                {
                    //  Atualização de Suco na lista
                    itensSuco.Quantidade = qtde;
                    Itens.Add(item);
                    Console.WriteLine($"Item {item.Nome} adicionado");

                }

            }
            else
            {
                Itens.Add(item);
                Console.WriteLine($"Item {item.Nome} adicionado");

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

                if (itemCerveja.Quantidade == 5)
                {
                    descontoCerveja = 5;
                }

            }

            else if (itemAgua != null)
            {
                //  preco += itemAgua.Quantidade * itemAgua.Valor;
                if (itemCerveja.Quantidade == 2 && itemConhaque.Quantidade == 3)
                {
                    descontoAgua = 70;
                }
            }

            foreach (Item it in Itens)
            {
                Console.WriteLine("item: " + it.Nome + "\nQuantidade:" + it.Quantidade);

                preco = it.Valor * it.Quantidade;
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




