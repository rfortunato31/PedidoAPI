namespace PedidoAPI.Models
{
    public class ItensPedidoSaida
    {
        public int Id { get; set; }

        public int IdProduto { get; set; }

        public string NomeProduto { get; set; } = null!;

        public decimal Valor { get; set; }

        public int Quantidade { get; set; }
    }
}
