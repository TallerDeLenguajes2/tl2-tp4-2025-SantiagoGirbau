public class AccesoADatosCSV : IAccesoADatos
{
    public Cadeteria leerDatos()
    {
        var textoCadeteria = File.ReadAllText("Cadeteria.csv");
        var infoCadeteria = textoCadeteria.Split(',');

        Cadeteria cadeteria = new Cadeteria(infoCadeteria[0], infoCadeteria[1]);

        var textoCadetes = File.ReadAllLines("Cadetes.csv");

        foreach (var infoCadete in textoCadetes)
        {
            var datosCadete = infoCadete.Split(',');
            Cadete cadete = new Cadete(datosCadete[1], datosCadete[2], datosCadete[3]);
            cadeteria.ListadoCadetes.Add(cadete);
        }
        return cadeteria;
    }
}