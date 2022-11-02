namespace PedidoAPI.Models
{
    public class PedidoAdd
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = null!;
        public string EmailCliente { get; set; } = null!;
        public bool Pago { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ItensPedidoAdd> ItensAdd { get; set; }
    }
}
