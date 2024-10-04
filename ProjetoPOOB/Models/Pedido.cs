using System;

namespace ProjetoPOOB.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public DateTime DtHora { get; set; }
        public Cliente Cliente { get; set; }
        public Char Status { get; set; }
        public string StatusTratado
        {
            get
            {
                switch (Status)
                {
                    case 'P':
                        return "Pendente";
                        break;
                    case 'F':
                        return "Finalizado";
                        break;
                    case 'C':
                        return "Cancelado";
                        break;
                    default:
                        return "Desconhecido";
                        break;
                }
            }
        }
    }
}
