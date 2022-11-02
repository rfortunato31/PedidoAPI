using System;
using System.Collections.Generic;

namespace PedidoAPI.Models
{
    public partial class Pedido
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = null!;
        public string EmailCliente { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public virtual List<ItensPedido> ItensPedidos { get; set; }
    }
}
