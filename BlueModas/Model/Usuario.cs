using  System.Collections.Generic;

namespace  BlueModas.Model
{
        public  class  Usuario
        {
                public  int  Id  {  get;  set;  }
                public  string  Nome  {  get;  set;  }
                public  string  Password  {  get;  set;  }
                public  List<Model.Carrinho>  Historico  {  get;  set;  }
        }
}
