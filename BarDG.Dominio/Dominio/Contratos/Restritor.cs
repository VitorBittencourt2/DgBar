namespace BarDG.Dominio.Contratos
{
    public interface IRestritor 
    {
        bool EhValido(Comanda comanda, Item item);
    }    
}