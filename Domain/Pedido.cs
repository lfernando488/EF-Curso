using EF_Curso.ValueObjects;

namespace EF_Curso.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime IniciadoEm {  get; set;  }
        public DateTime FinalizadoEM { get; set; }
        public TipoFrete TipoFrete { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public string Observacao { get; set; }
        public ICollection<PedidoItem> Items { get; set; }
    }
}
