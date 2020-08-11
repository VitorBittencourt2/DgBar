using System;
using System.Collections.Generic;
using System.Text;

namespace BarDG.Dominio
{
   public class Item
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }

        public Item(int id, string nome, double valor, int quantidade)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Quantidade = quantidade;
        }
    }
}
