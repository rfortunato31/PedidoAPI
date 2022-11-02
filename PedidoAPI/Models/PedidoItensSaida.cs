namespace PedidoAPI.Models
{
    public class PedidoItensSaida
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = null!;
        public string EmailCliente { get; set; } = null!;
        public bool Pago { get; set; }

        public decimal ValorTotal { get; set; }

        public List<ItensPedidoSaida> Itens { get; set; }
    }
}
