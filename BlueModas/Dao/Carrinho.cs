using BlueModas.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Dao
{
    public class Carrinho : IPresist<Model.Carrinho>
    {
        public bool Delete(Model.Carrinho obj)
        {
            if (obj != null)
            {
                Dao.Db.Carrinhos.Remove(obj);
                return true;
            }

            return false;
        }

        public Model.Carrinho Get(object guid)
        {
            Model.Carrinho dataout = null;
            string search = Guid.NewGuid().ToString();

            if (guid.GetType().Equals(typeof(string)))
            {
                search = (string)guid;

                dataout = (from Model.Carrinho x in Dao.Db.Carrinhos
                            where x.Guid.Equals(search, StringComparison.InvariantCultureIgnoreCase)
                            select x).FirstOrDefault();
            }

            if (dataout == null)
            {
                dataout = new Model.Carrinho()
                {
                    Guid = search,
                    DataHora = DateTime.Now,
                    Produtos = new List<Model.Produto>(),
                    Usuario = null,
                };

                Dao.Db.Carrinhos.Add(dataout);
            }

            return dataout;
        }

        public List<Model.Carrinho>GetList()
        {
            return Dao.Db.Carrinhos;
        }

        public List<Model.Carrinho>GetList(object obj)
        {
            return Dao.Db.Carrinhos;
        }

        public bool Save(Model.Carrinho obj)
        {
            Model.Carrinho carrinho = Dao.Db.Carrinhos.FirstOrDefault(x => x.Guid.Equals(obj.Guid, StringComparison.InvariantCultureIgnoreCase));
            if (carrinho != null)
            {
                carrinho.Usuario = obj.Usuario;
                carrinho.Produtos = obj.Produtos;
                return true;
            }

            return false;
        }

        public void AddProduto(object guid,
                               Model.Produto obj)
        {
            if (obj != null)
            {
                Model.Carrinho car = this.Get(guid);
                if (car != null)
                {
                    Produto prod = new Produto(car.Produtos);
                    prod.Save(obj);
                }                
            }
        }
    }
}
