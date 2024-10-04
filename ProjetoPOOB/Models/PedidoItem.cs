namespace ProjetoPOOB.Models
{
    public class PedidoItem
    {
        public int IdPedidoItem { get; set; }
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
        public int Qtde { get; set; }
        public decimal VlrUnitario { get; set; }
        public decimal VlrTotal { get; set; }
    }
}
