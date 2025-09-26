public class Clientes{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    
    public Clientes() { }
    public Clientes(string nombre, string direccion, string telefono, string datosRefDirec){
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosRefDirec;
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }
}