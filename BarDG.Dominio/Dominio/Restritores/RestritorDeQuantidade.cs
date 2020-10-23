using BarDG.Dominio.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDG.Dominio.Dominio.Restritores
{
    public class MenorDeIdadeBebidaAlcoolicaRestritor : IRestritor
    {
        public bool EhValido(Comanda comanda, Item item)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RestritorDeQuantidade : IRestritor

    {
        public RestritorDeQuantidade()
        {
            ItemIdQuantidade = new Dictionary<int, int>();
        }
        public RestritorDeQuantidade(Dictionary<int, int> itemIdQuantidade)
        {
            ItemIdQuantidade = itemIdQuantidade;
        }
        public Dictionary<int, int> ItemIdQuantidade { get; set; }

        public bool EhValido(Comanda comanda, Item item)
        {
            var possuiRestricao = ItemIdQuantidade.Any(x => x.Key == item.Id);

            if (!possuiRestricao)
            {
                return true;
            }

            var qtdItens = comanda.Itens.Count() + item.Quantidade;

            var quantidadeLimite = ItemIdQuantidade.FirstOrDefault(x => x.Key == item.Id).Value;

            var valido = (qtdItens > quantidadeLimite);

            if (!valido)
                Console.WriteLine($"Limite excedido de {quantidadeLimite} para o item {item.ToString()}");
            else
                Console.WriteLine($"Item válido para ser adicionado na comanda!");

            return valido;
        }
    }
}
