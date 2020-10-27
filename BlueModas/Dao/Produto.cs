using BlueModas.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BlueModas.Dao
{
    internal class Produto : IPersist<Model.Produto>
    {
        List<Model.Produto> listaprodutos = null;

        public Produto(List<Model.Produto> itens)
        {
            this.listaprodutos = itens;
        }

        public Produto()
        {
            this.listaprodutos = Dao.Db.ProdutoDisponivel;
        }

        public bool Delete(Model.Produto obj)
        {
            this.listaprodutos.RemoveAll(x => x.Id.Equals(obj.Id));
            return true;
        }

        public Model.Produto Get(object id)
        {
            if (id.GetType().Equals(typeof(int)))
            {
                int search = (int)id;

                return (from Model.Produto x in this.listaprodutos
                        where x.Id.Equals(search)
                        select x).FirstOrDefault();
            }

            return null;
        }

        public List<Model.Produto> GetList()
        {
            return this.listaprodutos;
        }

        public List<Model.Produto> GetList(object obj)
        {
            if (obj.GetType().Equals(typeof(string)))
            {
                string descricao = (string)obj;

                return (from Model.Produto x in this.listaprodutos
                        where x.Descricao.Equals(descricao)
                        select x).ToList();
            }

            if (obj.GetType().Equals(typeof(Model.Categoria)))
            {
                Model.Categoria categoria = (Model.Categoria)obj;

                return (from Model.Produto x in this.listaprodutos
                        where x.Categoria.Id.Equals(categoria.Id)
                        select x).ToList();
            }

            return new List<Model.Produto>();
        }

        public bool Save(Model.Produto obj)
        {
            if (obj != null)
            {
                this.listaprodutos.Add(obj);
                return true;
            }
            
            return false;
        }
    }
}
