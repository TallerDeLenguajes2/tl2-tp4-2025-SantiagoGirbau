public class Pedidos{
    private static int nroMax = 0;
    private int nro;
    private string obs;
    private Clientes cliente;
    private Estado estado;

    private Cadetes cadete;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Estado EstadoPedido { get => estado; set => estado = value; }
    public Cadetes Cadete { get => cadete; set => cadete = value; }

    public Pedidos(string obs, Clientes cliente){

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