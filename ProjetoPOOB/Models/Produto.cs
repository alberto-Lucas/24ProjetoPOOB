namespace ProjetoPOOB.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public string CodBarras { get; set; }
        public char Unidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public int EstoqueAtual { get; set; }
    }
}
