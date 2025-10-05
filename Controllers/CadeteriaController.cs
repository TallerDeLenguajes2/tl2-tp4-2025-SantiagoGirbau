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
        public ActionResult<Pedido> AgregarPedido(Pedido Pedido)
        {
            
            Cliente cliente = new Cliente(Pedido.Cliente.Nombre, Pedido.Cliente.Direccion, Pedido.Cliente.Telefono, Pedido.Cliente.DatosReferenciaDireccion);

            Pedido pedido = new Pedido(Pedido.Obs, cliente);

            cadeteria.ListadoPedidos.Add(pedido);
            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return Created();
        }
        [HttpPut("AsignarPedido")]
        public ActionResult<Pedido> AsignarPedido(int idPedido, int idCadete)
        {
            var pedido = cadeteria.ListadoPedidos[idPedido];
            var cadete = cadeteria.ListadoCadetes[idCadete];
            pedido.Cadete = cadete;
            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return Ok(pedido);
        }

        [HttpPut("CambiarEstadoPedido")]
        public ActionResult<Pedido> CambiarEstadoPedido(int idPedido, int Estado)
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
            return Ok(pedido);
        }
        [HttpPut("CambiarCadetePedido")]
        public ActionResult<Pedido> CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            var pedido = cadeteria.ListadoPedidos[idPedido];
            var cadete = cadeteria.ListadoCadetes[idNuevoCadete];
            pedido.Cadete = cadete;
            accesoADatos.GuardarPedidos(cadeteria.ListadoPedidos);
            return Ok(pedido);
        }
    }
}