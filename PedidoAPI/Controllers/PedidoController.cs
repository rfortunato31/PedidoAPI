using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PedidoAPI.Models;

namespace PedidoAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class PedidoController : Controller
    {        

        private readonly PedidoDBContext dbContext;

        public PedidoController(PedidoDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetPedidos()
        {
            var pedidos = await dbContext.Pedidos.ToListAsync();

            List<PedidoItensSaida> listPedidoSaida = new List<PedidoItensSaida>();

            for (int j = 0; j < pedidos.Count(); j++)
            {
                PedidoItensSaida pedidoSaida = new PedidoItensSaida();
                var pedido = await dbContext.Pedidos.FirstOrDefaultAsync(p => p.Id == pedidos[j].Id);

                List<ItensPedido> itensPedido = dbContext.ItensPedidos.Where(i => i.IdPedido == pedidos[j].Id).ToList<ItensPedido>();

                if (pedido == null)
                    return NotFound("Pedido Não Encontrado");
                if (itensPedido == null)
                    return NotFound("Iten(s) do Pedido Não Encontrado");

                pedidoSaida.Id = pedido.Id;
                pedidoSaida.NomeCliente = pedido.NomeCliente;
                pedidoSaida.EmailCliente = pedido.EmailCliente;
                pedidoSaida.Pago = pedido.Pago;
                decimal valorTotal = 0;
                pedidoSaida.Itens = new List<ItensPedidoSaida>();

                for (int i = 0; i < itensPedido.Count(); i++)
                {
                    ItensPedidoSaida itensSaida = new ItensPedidoSaida();
                    itensSaida.Id = itensPedido[i].Id;
                    itensSaida.IdProduto = itensPedido[i].IdProduto;
                    var produto = await dbContext.Produtos.FirstOrDefaultAsync(p => p.Id == itensPedido[i].IdProduto);
                    if (produto == null)
                        return NotFound("Produto Não Encontrado");
                    itensSaida.NomeProduto = produto.NomeProduto;
                    itensSaida.Valor = produto.Valor;
                    itensSaida.Quantidade = itensPedido[i].Quantidade;
                    valorTotal += itensPedido[i].Quantidade * produto.Valor;

                    pedidoSaida.Itens.Add(itensSaida);

                }
                pedidoSaida.ValorTotal = valorTotal;

                listPedidoSaida.Add(pedidoSaida);
            }            

            return Ok(listPedidoSaida);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedido(int id)
        {
            PedidoItensSaida pedidoSaida = new PedidoItensSaida();
            var pedido = await dbContext.Pedidos.FirstOrDefaultAsync(p => p.Id == id);

            List<ItensPedido> itensPedido = dbContext.ItensPedidos.Where(i => i.IdPedido == id).ToList<ItensPedido>();

            if (pedido == null)
                return NotFound("Pedido Não Encontrado");
            if (itensPedido == null)
                return NotFound("Iten(s) do Pedido Não Encontrado");

            pedidoSaida.Id = pedido.Id;
            pedidoSaida.NomeCliente = pedido.NomeCliente;
            pedidoSaida.EmailCliente = pedido.EmailCliente;
            pedidoSaida.Pago = pedido.Pago;
            decimal valorTotal = 0;
            pedidoSaida.Itens = new List<ItensPedidoSaida>();
            
            for (int i = 0; i < itensPedido.Count(); i++)
            {
                ItensPedidoSaida itensSaida = new ItensPedidoSaida();
                itensSaida.Id = itensPedido[i].Id;
                itensSaida.IdProduto = itensPedido[i].IdProduto;
                var produto = await dbContext.Produtos.FirstOrDefaultAsync(p => p.Id == itensPedido[i].IdProduto);
                if (produto == null)
                    return NotFound("Produto Não Encontrado");
                itensSaida.NomeProduto = produto.NomeProduto;
                itensSaida.Valor = produto.Valor;
                itensSaida.Quantidade = itensPedido[i].Quantidade;
                valorTotal += itensPedido[i].Quantidade * produto.Valor;
                
                pedidoSaida.Itens.Add(itensSaida);
                
            }
            pedidoSaida.ValorTotal = valorTotal;

            return Ok(pedidoSaida);

        }

        [HttpPost]
        public async Task<IActionResult> InsertPedido(PedidoAdd pedidoAdd)
        {
            Pedido pedido = new Pedido();
            pedido.NomeCliente = pedidoAdd.NomeCliente;
            pedido.EmailCliente = pedidoAdd.EmailCliente;
            pedido.Pago = pedidoAdd.Pago;
            pedido.DataCriacao = pedidoAdd.DataCriacao;

            await dbContext.Pedidos.AddAsync(pedido);

            await dbContext.SaveChangesAsync();

            for (int i = 0; i < pedidoAdd.ItensAdd.Count(); i++)
            {
                ItensPedido itensPedido = new ItensPedido();
                itensPedido.IdPedido = pedido.Id;
                itensPedido.IdProduto = pedidoAdd.ItensAdd[i].IdProduto;
                itensPedido.Quantidade = pedidoAdd.ItensAdd[i].Quantidade;

                await dbContext.ItensPedidos.AddAsync(itensPedido);
            }
            await dbContext.SaveChangesAsync();

            return Ok("Pedido Inserido com sucesso!");
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePedido(PedidoAdd pedidoData)
        {
            if (pedidoData == null || pedidoData.Id == 0)
                return BadRequest();

            var pedido = await dbContext.Pedidos.FindAsync(pedidoData.Id);
            if (pedido == null)
                return NotFound("Pedido Não Encontado");  

            pedido.NomeCliente = pedidoData.NomeCliente;
            pedido.EmailCliente = pedidoData.EmailCliente;
            pedido.Pago = pedidoData.Pago;

            await dbContext.SaveChangesAsync();
            for (int i = 0; i < pedidoData.ItensAdd.Count(); i++)
            {
                var itemPedido = await dbContext.ItensPedidos.FindAsync(pedidoData.ItensAdd[i].Id);
                if (itemPedido == null)
                    return NotFound("Item do Pedido Não Encontado");
                dbContext.ItensPedidos.Remove(itemPedido);

            }

            for (int i = 0; i < pedidoData.ItensAdd.Count(); i++)
            {
                ItensPedido itensPedido = new ItensPedido();
                itensPedido.IdPedido = pedido.Id;
                itensPedido.IdProduto = pedidoData.ItensAdd[i].IdProduto;
                itensPedido.Quantidade = pedidoData.ItensAdd[i].Quantidade;

                await dbContext.ItensPedidos.AddAsync(itensPedido);
            }
            await dbContext.SaveChangesAsync();

            return Ok("Pedido Alterado com sucesso!");
        }        

        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeletePedido(int id)
        {
            List<ItensPedido> itensPedido = dbContext.ItensPedidos.Where(i => i.IdPedido == id).ToList<ItensPedido>();
            for (int i = 0; i < itensPedido.Count(); i++)
            {
                dbContext.ItensPedidos.Remove(itensPedido[i]);
                await dbContext.SaveChangesAsync();
            }
            var pedido = await dbContext.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                dbContext.Pedidos.Remove(pedido);

                await dbContext.SaveChangesAsync();

                return Ok("Pedido Deletado com sucesso!");
            }

            return NotFound("Pedido não encontrado.");
        }
    }
}
