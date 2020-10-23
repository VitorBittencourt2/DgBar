using System.Collections.Generic;

namespace BarDG.Dominio.Dominio.Contratos
{
   public interface IDefinidorDePromocoes
    {
        void AplicarPromocoes(IEnumerable<Promocao> promocoes, Comanda comanda);
        double ObterValorTotal();
    }

    public interface IExecutorDePromocaoSimples : IDefinidorDePromocoes { }
    public interface IExecutorDePromocaoCumulativa : IDefinidorDePromocoes { }
}
