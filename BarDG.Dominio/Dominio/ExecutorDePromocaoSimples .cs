using BarDG.Dominio.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDG.Dominio.Dominio
{
   public class ExecutorDePromocaoSimples: IExecutorDePromocaoSimples
    {
        public List<Item> ItemsNaoAnalisados { get; private set; }
        public List<Item> ItemsAnalisados { get; private set; }
        public double ValorAtual { get; set; }
    
        public ExecutorDePromocaoSimples()
        {
            ItemsNaoAnalisados = new List<Item>();
            ItemsAnalisados = new List<Item>();
        }

        public void AplicarPromocoes(IEnumerable<Promocao> promocoes, Comanda comanda)
        {
            ItemsNaoAnalisados = comanda.Itens.ToList();

            foreach (var promocao in promocoes)
            {
                Console.WriteLine($" Valor total antes de aplicar promoção {promocao.Nome}: {promocao.ValorComanda}");
                Console.WriteLine($"A aplicar promoção {promocao.Nome}");
                promocao.AplicarDesconto(ItemsNaoAnalisados);

                var itensAnalisados = promocao.ObterItensAnalisados();

                ValorAtual = promocao.ValorComanda;

                AdicionarItensAplicados(itensAnalisados);
            }
        }
        public double ObterValorTotal() => ValorAtual;
        private void AdicionarItensAplicados(IEnumerable<Item> itensAnalisados)
        {
            foreach (var item in itensAnalisados)
            {
                var itemAnalisado = ItemsNaoAnalisados.FirstOrDefault(x => x.Id == item.Id);
                ItemsAnalisados.Add(item);
                ItemsNaoAnalisados.Remove(itemAnalisado);
            }
        }
    }
}
