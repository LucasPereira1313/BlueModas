using System.Linq;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueModas.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        // GET: api/<CarrinhoController>
        [HttpGet]
        public int GetQtd(string guid)
        {
            Interface.IPresist<Model.Carrinho> car = new Dao.Carrinho();
            Model.Carrinho iten = car.Get(guid);
            return iten.Produtos.Count();
        }

        // GET: api/<CarrinhoController>
        [HttpPut]
        public int AddProduto(string guid, int produto)
        {
            Dao.Carrinho car = new Dao.Carrinho();
            Dao.Produto prod = new Dao.Produto();
            car.AddProduto(guid, prod.Get(produto));

            return this.GetQtd(guid);
        }

    }
}
