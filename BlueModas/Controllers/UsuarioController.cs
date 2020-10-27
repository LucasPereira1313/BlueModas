using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueModas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // POST api/<UsuarioController>
        [HttpPost]
        public void Post(string nome, string password)
        {
            Dao.Usuario user = new Dao.Usuario();
            user.Save(new Model.Usuario()
            {
                Id = Dao.Db.Usuarios.Count,
                Historico = new List<Model.Carrinho>(),
                Nome = nome,
                Password = password,
            });
        }
    }
}
