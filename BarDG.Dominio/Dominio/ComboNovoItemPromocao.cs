using BarDG.Dominio.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarDG.Dominio.Dominio
{
    public class ComboNovoItemPromocao:Promocao
    {
        public Dictionary<int, int> ItemIdQuantidadeTemplate { get; set; }

        public ComboNovoItemPromocao(Dictionary<int, int> itemIdQuantidadeTemplate, TipoDeDesconto tipoDesconto, int idItemDesconto, double valorDesconto,int id,string nome)
            : base(id, nome, tipoDesconto, idItemDesconto, valorDesconto)
        {
            ItemIdQuantidadeTemplate = itemIdQuantidadeTemplate;
        }

        protected override bool ÉElegível(IEnumerable<Item> itens)
        {
            ItemsNaoAnalisados = itens.ToList();

            if (!ItemsNaoAnalisados.Where(x => x.Id == IdItemDesconto).Any())
                return false;

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

            var itemDesconto = ItemsNaoAnalisados.FirstOrDefault(x => x.Id == IdItemDesconto);
            ItemsNaoAnalisados.Remove(itemDesconto);
            if (itemDesconto.Quantidade > 1)
            {
                itemDesconto.Quantidade--;
                ItemsNaoAnalisados.Add(itemDesconto);
            }
            else
                ItemsNaoAnalisados.Remove(itemDesconto);

            var _itemDesconto = new Item(itemDesconto.Id, itemDesconto.Nome, itemDesconto.Valor, 1);
            ItensEmAnalise.Add(_itemDesconto);
           

            RegistrarItemASerAplicadoDesconto();

            if (ItemsNaoAnalisados.Any())
                AplicarDesconto(ItemsNaoAnalisados);

            ValorComanda = CalcularValorComanda();
        }
    }
}
