using BarDG.Dominio.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDG.Dominio.Dominio
{
    public abstract class Promocao
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }     
        public double ValorComanda { get; protected set; }
        public int IdItemDesconto { get; set; }
        public double ValorDesconto { get; set; }
        public TipoDeDesconto TipoDeDesconto { get; set; }

        protected List<Item> ItemsNaoAnalisados { get; set; }
        protected List<Item> ItensEmAnalise { get; set; }
        protected List<Item> ItensAnalisados { get; set; }
        protected List<Item> ItensDescontoAplicado { get; set; }
        public override string ToString() => $"{Nome}";
        protected abstract bool ÉElegível(IEnumerable<Item> Items);
        public abstract void AplicarDesconto(IEnumerable<Item> Items);

        protected Promocao(int id, string nome,TipoDeDesconto tipoDeDesconto, int idItemDesconto, double valorDesconto)
        {
            Id = id;
            Nome = nome;
            TipoDeDesconto = tipoDeDesconto;
            IdItemDesconto = idItemDesconto;
            ValorDesconto = valorDesconto;
            ItemsNaoAnalisados = new List<Item>();
            ItensEmAnalise = new List<Item>();
            ItensAnalisados = new List<Item>();
            ItensDescontoAplicado = new List<Item>();
        }

        protected virtual double CalcularValorComanda()
        {
            double valor = 0;
            foreach (var item in ItemsNaoAnalisados)
                valor += item.Quantidade * item.Valor;

            foreach (var item in ItensAnalisados)
                valor += item.Quantidade * item.Valor;

            Console.WriteLine($" Valor total após aplicar promoção {Nome}: {valor}");

            return valor;
        }

        public virtual IEnumerable<Item> ObterItensAnalisados()
        {
            var itens = new List<Item>();

            if (!ItensAnalisados.Any())
                return itens;

            var groups = ItensAnalisados.GroupBy(x => x.Id);
            foreach (var group in groups)
            {
                var qtd = group.Sum(x => x.Quantidade);
                var item = group.FirstOrDefault();
                item.Quantidade = qtd;
                itens.Add(item);
            }

            return itens;
        }

        public virtual IEnumerable<Item> ObterItensDescontoAplicado()
        {
            var itens = new List<Item>();

            if (!ItensDescontoAplicado.Any())
                return itens;

            var groups = ItensDescontoAplicado.GroupBy(x => x.Id);
            foreach (var group in groups)
            {
                var qtd = group.Sum(x => x.Quantidade);
                var item = group.FirstOrDefault();
                item.Quantidade = qtd;
                itens.Add(item);
            }

            return itens;
        }

        protected virtual double AplicarDescontoItem(double valorAtual)
        {
            if (TipoDeDesconto == TipoDeDesconto.Valor)
                return valorAtual - ValorDesconto;
            if (TipoDeDesconto == TipoDeDesconto.Porcentagem)
                return valorAtual * ValorDesconto;
            return valorAtual;
        }

        protected virtual void RegistrarItemASerAplicadoDesconto()
        {
            var itemDesconto = ItensEmAnalise.FirstOrDefault(x => x.Id == IdItemDesconto);
            Console.WriteLine($"aplicado desconto sob item {itemDesconto.Nome} - promoção {Nome} - valor antigo: {itemDesconto.Valor}");
            itemDesconto.Valor = AplicarDescontoItem(itemDesconto.Valor);
            Console.WriteLine($"aplicado desconto sob item {itemDesconto.Nome} - promoção {Nome} - novo valor: {itemDesconto.Valor}");

            ItensDescontoAplicado.Add(itemDesconto);
            ItensEmAnalise.Remove(itemDesconto);
            ItensAnalisados.Add(itemDesconto);
            ItensAnalisados.AddRange(ItensEmAnalise);
            ItensEmAnalise = new List<Item>();
        }

    }
}
