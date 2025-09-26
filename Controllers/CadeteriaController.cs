using Microsoft.AspNetCore.Mvc;

// ● [Get] GetPedidos() => Retorna una lista de Pedidos
// ● [Get] GetCadetes() => Retorna una lista de Cadetes
// ● [Get] GetInforme() => Retorna un objeto Informe
// ● [Post] AgregarPedido(Pedido pedido)
// ● [Put] AsignarPedido(int idPedido, int idCadete)
// ● [Put] CambiarEstadoPedido(int idPedido,int NuevoEstado)
// ● [Put] CambiarCadetePedido(int idPedido,int idNuevoCadete)



namespace CadeteriaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CadeteriaController : ControllerBase
    {
        private static readonly Object _lock = new();
        private AccesoADatosJSON accesoADatos;
        public Cadeteria cadeteria;
        public CadeteriaController()
        {
            accesoADatos = new AccesoADatosJSON();
            cadeteria = accesoADatos.leerDatos();
        }

        [HttpGet("GetPedidos")]
        public IActionResult GetPedidos()
        {
            lock (_lock)
            {
                return Ok(cadeteria.ListadoPedidos);
            }
        }

        [HttpGet("GetCadetes")]
        public IActionResult GetCadetes()
        {
            lock (_lock)
            {
                return Ok(cadeteria.ListadoCadetes);
            }
        }

        [HttpPost("AgregarPedido")]
        public ActionResult<Pedidos> AgregarPedido(string nombreCliente, string direccionCliente, string telefonoCliente, string datosReferencia, string observaciones)
        {
            Clientes cliente = new Clientes(nombreCliente, direccionCliente, telefonoCliente, datosReferencia);

            Pedidos pedido = new Pedidos(observaciones, cliente);

            cadeteria.ListadoPedidos.Add(pedido);
            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return Created();
        }
    }
}