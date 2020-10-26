using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueModas.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        // GET: api/<ProdutoController>
        [HttpGet]
        public List<Model.Produto> GetList()
        {
            Dao.Produto obj = new Dao.Produto();
            return obj.GetList();
        }

        // GET api/<ProdutoController>/5
        [HttpGet]
        public List<Model.Produto> GetListId(int categoria)
        {
            Dao.Produto obj = new Dao.Produto();
            return obj.GetList(new Model.Categoria() { Id = categoria });
        }
    }
}
