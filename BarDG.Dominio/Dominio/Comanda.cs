using BarDG.Dominio.Dominio;
using BarDG.Dominio.Dominio.Contratos;
using System;
using System.Collections.Generic;

namespace BarDG.Dominio
{
    public class Comanda
    {
        public List<Item> Itens { get; private set; }
        public bool ComandaFechada { get; set; }
        public double ValorComDesconto { get; set; }
        public double ValorSemDescontos { get; set; }
        public IEnumerable<IRestritor> Restritores { get; private set; }
        private readonly IDefinidorDePromocoes _executor;
        private IEnumerable<Promocao> Promocoes { get; set; }

        public Comanda(IEnumerable<IRestritor> restritores, IDefinidorDePromocoes executor, IEnumerable<Promocao> promocoes)
        {
            Restritores = restritores;
            _executor = executor;
            Promocoes = promocoes;
            Itens = new List<Item>();
        }

        public void RegistrarItem(Item item)
        {
            Console.WriteLine($"Ao adicionar item {item.Id}");

            foreach (var restritor in Restritores)
            {
                if (!restritor.EhValido(this, item))
                    return;
            Itens.Add(item);
            ValorSemDescontos = CalcularValorSemDescontos();
            }
           
        }

       public double CalcularValorSemDescontos()
        {
            double valor = 0;
            foreach (var item in Itens)
            {
                valor += item.Valor * item.Quantidade;
            }
            return valor;
        }

        public void FechamentoComanda()
        {
            if (ComandaFechada) {
                return;
            }

            ComandaFechada = true;

            _executor.AplicarPromocoes(Promocoes, this);
            ValorComDesconto = _executor.ObterValorTotal();

        }
        public void ResetaComanda()
        {
            Itens.Clear();
        }
    }

}




