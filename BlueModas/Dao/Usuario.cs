using BlueModas.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueModas.Dao
{
    internal class Usuario : IPresist<Model.Usuario>
    {
        public bool Delete(Model.Usuario obj)
        {
            Model.Usuario item = Dao.Db.Usuarios.FirstOrDefault(x => x.Equals(obj));
            if (item != null)
            {
                lock (Dao.Db.Usuarios)
                {
                    Dao.Db.Usuarios.Remove(item);
                }
                
                return true;
            }
            return false;
        }

        public Model.Usuario Get(object id)
        {
            if (id.GetType().Equals(typeof(int)))
            {
                int search = (int)id;

                return (from Model.Usuario x in Dao.Db.Usuarios
                        where x.Id.Equals(search)
                        select x).FirstOrDefault();
            }

            if (id.GetType().Equals(typeof(string)))
            {
                string search = (string)id;

                return (from Model.Usuario x in Dao.Db.Usuarios
                        where x.Nome.Equals(search, StringComparison.InvariantCultureIgnoreCase)
                        select x).FirstOrDefault();
            }

            return null;
        }

        public List<Model.Usuario>GetList()
        {
            return Dao.Db.Usuarios;
        }

        public List<Model.Usuario> GetList(object obj)
        {
            if (obj != null)
            {
                if (obj.GetType().Equals(typeof(string)))
                {
                    string nome = (string)obj;

                    return (from Model.Usuario x in Dao.Db.Usuarios
                            where x.Nome.Contains(nome)
                            select x).ToList();
                }
            }

            return Dao.Db.Usuarios;
        }

        public bool Save(Model.Usuario obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Nome))
            {
                obj.Id = Dao.Db.Usuarios.Count;

                if (obj.Historico == null)
                {
                    obj.Historico = new List<Model.Carrinho>();
                }

                lock (Dao.Db.Usuarios)
                {
                    Dao.Db.Usuarios.Add(obj);                    
                }

                return true;
            }

            return false;
        }

    }
}
