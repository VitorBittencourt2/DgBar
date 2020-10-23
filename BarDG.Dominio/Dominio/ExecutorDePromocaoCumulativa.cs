using BarDG.Dominio.Dominio.Contratos;
using System;
using System.Collections.Generic;

namespace BarDG.Dominio.Dominio
{
    public class ExecutorDePromocaoCumulativa: IExecutorDePromocaoCumulativa
    {
        private double ValorAtual { get; set; }
        
        public void AplicarPromocoes(IEnumerable<Promocao> promocoes, Comanda comanda)
        {
            foreach (var promocao in promocoes)
            {
                Console.WriteLine($" Valor total antes de aplicar promoção {promocao.Nome}: {promocao.ValorComanda}");
                Console.WriteLine($"A aplicar promoção {promocao.Nome}");
                promocao.AplicarDesconto(comanda.Itens);
                
                ValorAtual = promocao.ValorComanda;
            }
        }
        public double ObterValorTotal() => ValorAtual;
    }
}
