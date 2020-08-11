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
            // barrar inserir suco com mais de 3 de quantidade
            if (item.Id == 1 && item.Quantidade > 3)
                return;

            // barrar se eu já tenho suco na minha lista e a quantidade informada + quantidade excede o limite
            var itensSuco = Itens.FirstOrDefault(x => x.Id == 1);

            if (itensSuco != null)
            {
                var qtde = itensSuco.Quantidade + item.Quantidade;

                if (qtde > 3)
                {
                    Console.WriteLine("Quantidade de suco informada excete o limite de 3");
                    return;
                }
                else
                {
                    // estou atualizando a quantidade de sucos na minha lista
                    itensSuco.Quantidade = qtde;
                }
            }
            else
            {
                // Corrigir a validação da quantidade para que considere apenas
                // item suco
                //if (item.Quantidade <= 4)
                //{
                Itens.Add(item);
                Console.WriteLine($"Item {item.Nome} adicionado");
                //}

                //else
                //{
                //    return;
                //}
            }

        }

        public void FechamentoComanda() //Item item
        {
            double valorFinal = 0;
            double desconto = 0;
            double preco = 0;

            var itemCerveja = Itens.FirstOrDefault(x => x.Nome == "Cerveja");
            var itemConhaque = Itens.FirstOrDefault(x => x.Nome == "Conhaque");
            var itemAgua = Itens.FirstOrDefault(x => x.Nome == "Agua");
            var itemSuco = Itens.FirstOrDefault(x => x.Nome == "Suco");

            if (itemCerveja != null)
            {
                preco = itemCerveja.Quantidade * itemCerveja.Valor;
            }
            else if (itemCerveja != null)
            {                
                preco += itemConhaque.Quantidade * itemConhaque.Valor;
            }
            else if (itemSuco != null)
            {
                preco += itemSuco.Quantidade * itemSuco.Valor;
            }
            // calcular o valor total de água
            else if (itemAgua)
            {
                preco += 70;
                preco = preco * item.Quantidade;
                // e se eu tiver 4 cervejas e 6 conhaques ?
                if (itemCerveja && item.Quantidade == 2 ||
                    itemConhaque && item.Quantidade == 3)
                {
                    preco += 0;

                    desconto = 70;
                }
            }

            // daqui pra frente, calcular os descontos:

            if (item.Quantidade == 5 && item.Nome == "Cerveja")
            {
                desconto = 5;
            }

            foreach (Item it in Itens)
            {
                Console.WriteLine("item: " + it.Nome + "\nQuantidade:" + it.Quantidade);
                valorFinal = preco - desconto;
            }

            Console.WriteLine("Desconto: " + desconto + "\nValorfinal: " + valorFinal);

        }

        public void ResetaComanda()
        {
            Itens.Clear();
        }
    }

}




