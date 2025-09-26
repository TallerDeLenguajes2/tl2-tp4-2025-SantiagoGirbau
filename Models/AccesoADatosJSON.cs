using System.Text.Json;
using System.Text.Json.Nodes;
public class AccesoADatosJSON : IAccesoADatos
{
    private static readonly Object _lock = new();
    public Cadeteria leerDatos()
    {
        var textoCadeteria = File.ReadAllText("Cadeteria.json");


        Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(textoCadeteria);

        var textoCadetes = File.ReadAllText("Cadetes.json");

        cadeteria.ListadoCadetes = JsonSerializer.Deserialize<List<Cadetes>>(textoCadetes);

        var textoPedidos = File.ReadAllText("Pedidos.json");

        cadeteria.ListadoPedidos = JsonSerializer.Deserialize<List<Pedidos>>(textoPedidos);

        return cadeteria;
    }



    public void GuardarPedidos(List<Pedidos> pedidos)
    {
        // Serializo la lista en JSON con formato indentado (más legible)
        string jsonString = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });

        // Guardo el JSON en un archivo
        File.WriteAllText("Pedidos.json", jsonString);
    }
}