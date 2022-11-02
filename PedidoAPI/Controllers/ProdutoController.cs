using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidoAPI.Models;

namespace PedidoAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly PedidoDBContext dbContext;

        public ProdutoController(PedidoDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await dbContext.Produtos.ToListAsync();
            return Ok(produtos);
        }

        
    }
}
