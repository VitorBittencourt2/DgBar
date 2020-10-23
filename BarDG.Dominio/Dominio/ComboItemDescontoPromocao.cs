using BarDG.Dominio.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarDG.Dominio.Dominio
{
   public class ComboItemDescontoPromocao:Promocao
    {
        public Dictionary<int, int> ItemIdQuantidadeTemplate { get; set; }

        public ComboItemDescontoPromocao(Dictionary<int, int> itemIdQuantidadeTemplate,TipoDeDesconto tipoDeDesconto,int idItemDesconto,double valorDesconto,int id,string nome) : base(id, nome, tipoDeDesconto, idItemDesconto, valorDesconto)
        {
            ItemIdQuantidadeTemplate = itemIdQuantidadeTemplate;
        }
        protected override bool ÉElegível(IEnumerable<Item> itens)
        {
            ItemsNaoAnalisados = itens.ToList();

            foreach (var itemTemplate in ItemIdQuantidadeTemplate)
            {
                var contemItem = itens.FirstOrDefault(x => x.Id == itemTemplate.Key);

                if (contemItem == null)
                    return false;

                var quantidadeSuficiente = contemItem.Quantidade >= itemTemplate.Value;

                if (!quantidadeSuficiente)
                    return false;
            }
            return true;
        }

        public override void AplicarDesconto(IEnumerable<Item> Items)
        {
            if (!ÉElegível(Items))
                return;

            foreach (var itemTemplate in ItemIdQuantidadeTemplate)
            {
                var item = ItemsNaoAnalisados.FirstOrDefault(x => x.Id == itemTemplate.Key);

                var qtdRestante = item.Quantidade - itemTemplate.Value;

                item.Quantidade = itemTemplate.Value;
                ItensEmAnalise.Add(item);
                ItemsNaoAnalisados.Remove(item);

                if (qtdRestante > 0)
                {
                    var itemNaoAnalisado = new Item(item.Id, item.Nome, item.Valor, qtdRestante);
                    ItemsNaoAnalisados.Add(itemNaoAnalisado);
                }
            }

            RegistrarItemASerAplicadoDesconto();

            if (ItemsNaoAnalisados.Any())
                AplicarDesconto(ItemsNaoAnalisados);

            ValorComanda = CalcularValorComanda();
        }
    }
}
