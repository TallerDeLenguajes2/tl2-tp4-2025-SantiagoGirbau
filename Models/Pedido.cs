public class Pedido{
    private static int nroMax = 0;
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estado estado;

    private Cadete? cadete;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Estado EstadoPedido { get => estado; set => estado = value; }
    public Cadete? Cadete { get => cadete; set => cadete = value; }
     public Cliente Cliente { get => cliente; set => cliente = value; }

    public Pedido(string obs, Cliente cliente){

        nro = nroMax;
        nroMax++;
        this.obs = obs;
        this.cliente = cliente;
        this.estado = 0;
    }

    public enum Estado{
        pendiente = 0,
        entregado = 1,
        cancelado = 2
    }
}