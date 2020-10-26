using  System;
using  System.Collections.Generic;

namespace  BlueModas.Model
{
        public  class  Carrinho
        {
                public  string  Guid  {  get;  set;  }
                public  Model.Usuario  Usuario  {  get;  set;  }
                public  List<Model.Produto>  Produtos  {  get;  set;  }
                public  DateTime  DataHora  {  get;  set;  }
        }
}
