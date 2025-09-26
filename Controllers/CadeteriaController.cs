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
        public IActionResult AgregarPedido(string nombreCliente, string direccionCliente, string telefonoCliente, string datosReferencia, string observaciones)
        {
            Clientes cliente = new Clientes(nombreCliente, direccionCliente, telefonoCliente, datosReferencia);

            Pedido pedido = new Pedido(observaciones, cliente);

            cadeteria.ListadoPedidos.Add(pedido);
            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return Created();
        }
        [HttpPut("AsignarPedido")]
        public IActionResult AsignarPedido(int idPedido, int idCadete)
        {
            var pedido = cadeteria.ListadoPedidos[idPedido];
            var cadete = cadeteria.ListadoCadetes[idCadete];
            pedido.Cadete = cadete;
            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return NoContent();
        }

        [HttpPut("CambiarEstadoPedido")]
        public IActionResult CambiarEstadoPedido(int idPedido, int Estado)
        {
            var pedido = cadeteria.ListadoPedidos[idPedido];
            switch (Estado)
            {
                case 0:
                    pedido.EstadoPedido = Pedido.Estado.pendiente;
                    break;
                case 1:
                    pedido.EstadoPedido = Pedido.Estado.entregado;
                    cadeteria.ListadoPedidos[idPedido].Cadete.CantidadDePedidosEntregados++;
                    break;
                case 2:
                    pedido.EstadoPedido = Pedido.Estado.cancelado;
                    break;
                default:
                    pedido.EstadoPedido = Pedido.Estado.pendiente;
                    break;
            }

            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return NoContent();
        }
        [HttpPut("CambiarCadetePedido")]
        public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            var pedido = cadeteria.ListadoPedidos[idPedido];
            var cadete = cadeteria.ListadoCadetes[idNuevoCadete];
            pedido.Cadete = cadete;
            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return NoContent();
        }
    }
}