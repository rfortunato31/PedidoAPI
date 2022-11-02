using System;
using System.Collections.Generic;

namespace PedidoAPI.Models
{
    public partial class Produto
    {
        public Produto()
        {
            ItensPedidos = new HashSet<ItensPedido>();
        }

        public int Id { get; set; }
        public string NomeProduto { get; set; } = null!;
        public decimal Valor { get; set; }

        public virtual ICollection<ItensPedido> ItensPedidos { get; set; }
    }
}
