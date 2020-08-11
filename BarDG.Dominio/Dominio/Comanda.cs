using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

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

            if (itensSuco != null)
            {

                var qtde = itensSuco.Quantidade + item.Quantidade;

                if (qtde > 4)
                {
                    return;
                }

                else
                {

                    itensSuco.Quantidade = qtde;
                }
            }
            else
            {
                if (item.Quantidade <= 4)
                {
                    Itens.Add(item);
                }

                else
                {
                    return;
                }
            }

        }

     




        public void FechamentoComanda(Item item) //Item item
        {
            double valorFinal =0;
            double desconto=0;
            double preco = 0;
           

            var itemCerveja = item.Nome.Contains("Cerveja");
            var itemConhaque = item.Nome.Contains("Conhaque");
            var itemAgua = item.Nome.Contains("Agua");
            var itemSuco = item.Nome.Contains("Suco");


            if (itemCerveja)
            {
                preco += 5;
                preco = preco * item.Quantidade;

            }

            else if (itemConhaque)
            {

                preco += 20;
                preco = preco * item.Quantidade;
            }

            else if (itemSuco)
            {
                preco =+ 50;
                preco = preco * item.Quantidade;
            }

            else if (itemAgua)
            {
                preco += 70;
                preco = preco * item.Quantidade;

                if (itemCerveja && item.Quantidade == 2 ||
                    itemConhaque && item.Quantidade == 3)
                {
                    preco += 0;

                    desconto = 70;
                }
            }


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




