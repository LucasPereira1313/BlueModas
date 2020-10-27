using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlueModas.Interface;
using BlueModas.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueModas.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {

        private static IPersist<Carrinho> NewMethod()
        {
            return new Dao.Carrinho();
        }

        // GET: api/<CarrinhoController>
        [HttpGet]
        public int GetQtd(string guid)
        {
            IPersist<Carrinho> car = NewMethod();
            Model.Carrinho iten = car.Get(guid);
            return iten.Produtos.Count();
        }

        //Get car of the shopping
        [HttpGet]
        public List<Model.Shopping> GetShopping(string guid)
        {
            IPersist<Carrinho> car = NewMethod();
            Model.Carrinho iten = car.Get(guid);

            var itens = (from Model.Produto item in iten.Produtos
                         group item by item.Id into newitem
                         orderby newitem.Key
                         select new Model.Shopping
                         {
                             Contagem = newitem.Count(),
                             ValorTotal = newitem.Sum( x => x.Valor),
                             Produto = newitem.First(),
                         }).ToList();

            return itens;
        }

        // GET: api/<CarrinhoController>
        [HttpPost]
        public int AddProduto(string guid, int produto)
        {
            Dao.Carrinho car = new Dao.Carrinho();
            Dao.Produto prod = new Dao.Produto();
            car.AddProduto(guid, prod.Get(produto));

            return this.GetQtd(guid);
        }

    }
}
