public class Cadete{

    private static int idMax = 0;
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private int cantidadDePedidosEntregados;


    public int Id { get => id; }
    public string Nombre {get => nombre; set => nombre = value;}
    public string Direccion {get => direccion; set => direccion = value;}
    public string Telefono {get => telefono; set => telefono = value;}
    public int CantidadDePedidosEntregados { get => cantidadDePedidosEntregados; set => cantidadDePedidosEntregados = value; }

    public Cadete(string nombre, string direccion, string telefono)
    {
        id = idMax;
        idMax++;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.cantidadDePedidosEntregados = 0;
       
    }


}