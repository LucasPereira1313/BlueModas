namespace BlueModas.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public Model.Categoria Categoria { get; set; }
        public string ImageBase64 { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
    }
}
