

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos;
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.ListadoCadetes = new List<Cadete>();
        this.ListadoPedidos = new List<Pedido>();
    }

     public List<string> ListarCadetes()
    {
        List<string> lista = new List<string>();
        foreach (var cadete in ListadoCadetes)
        {
            lista.Add($"ID: {cadete.Id}\nCadete: {cadete.Nombre}\nTeléfono: {cadete.Telefono}\nDirección: {cadete.Direccion}");
        }
        return lista;
    }

   public List<string> listarPedidos()
    {
        var lista = new List<string>();
        foreach (var pedido in listadoPedidos)
        {
            lista.Add($"Pedido nro {pedido.Nro}\n obs: {pedido.Obs}\n estado: {pedido.EstadoPedido}");
        }

        return lista;
    }

    public int JornalACobrar(int idCadete)
    {

        Cadete cadete = listadoCadetes[idCadete];
        int monto = cadete.CantidadDePedidosEntregados * 500;
        cadete.CantidadDePedidosEntregados = 0;
        return monto;
    }


    public void AgregarPedido(string nombreCliente, string direccionCliente, string telefonoCliente, string datosReferencia, string observaciones)
{
    // Crear cliente
    Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferencia);

    // Crear pedido
    Pedido pedido = new Pedido(observaciones, cliente);

    // Agregar al listado
    ListadoPedidos.Add(pedido);
}
    public void asignarCadete()
    {
        if (listadoPedidos.Count != 0)
        {
            bool encontrado = false;
            int nroB;

            do
            {
                listarPedidos();
                System.Console.WriteLine("Ingrese el Nro de pedido: ");
                nroB = int.Parse(Console.ReadLine());
                for (int i = 0; i < listadoPedidos.Count; i++)
                {
                    if (nroB == listadoPedidos[i].Nro)
                    {
                        encontrado = true;
                        break;
                    }
                    else
                    {
                        System.Console.WriteLine("No se encontró un pedido con ese número.");
                    }
                }
            } while (!encontrado);
            ListarCadetes();
            System.Console.WriteLine("Escriba ID del repartidor a asignar: ");
            var IdCadete = Console.ReadLine();
            int Id;
            if (int.TryParse(IdCadete, out Id))
            {
                Cadete cadete = listadoCadetes[Id];
                listadoPedidos[nroB].Cadete = cadete;
                System.Console.WriteLine($"Repartidor: {cadete.Nombre}");
                listarPedidos();
            }
        }
        else
        {
            System.Console.WriteLine("No hay pedidos activos.\n");
        }

    }
}
