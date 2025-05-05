using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using LAB08_Mamanchura.Models;

namespace LAB08_Mamanchura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjerciciosController : ControllerBase
    {
        private readonly Lab08Context _context;

        public EjerciciosController(Lab08Context context)
        {
            _context = context;
        }

        // Ejercicio 1:
        [HttpGet("clientes-nombre/{nombre}")]
        public IActionResult ObtenerClientesPorNombre(string nombre)
        {
            var clientes = _context.Clients
                                   .Where(c => c.Name.Contains(nombre))
                                   .ToList();
            return Ok(clientes);
        }

        // Ejercicio 2:
        [HttpGet("productos-precio-mayor/{precio}")]
        public IActionResult ObtenerProductosPorPrecioMayor(decimal precio)
        {
            var productos = _context.Products
                                    .Where(p => p.Price > precio)
                                    .ToList();
            return Ok(productos);
        }

        // Ejercicio 3:
        [HttpGet("detalles-orden/{orderId}")]
        public IActionResult ObtenerDetallesDeOrden(int orderId)
        {
            var detalles = _context.Orderdetails
                                   .Where(d => d.OrderId == orderId)
                                   .Select(d => new { d.Product.Name, d.Quantity })
                                   .ToList();
            return Ok(detalles);
        }

        // Ejercicio 4:
        [HttpGet("cantidad-total-productos/{orderId}")]
        public IActionResult ObtenerCantidadTotalPorOrden(int orderId)
        {
            var cantidadTotal = _context.Orderdetails
                                        .Where(d => d.OrderId == orderId)
                                        .Sum(d => d.Quantity);
            return Ok(cantidadTotal);
        }

        // Ejercicio 5:
        [HttpGet("producto-mas-caro")]
        public IActionResult ObtenerProductoMasCaro()
        {
            var productoMasCaro = _context.Products
                                          .OrderByDescending(p => p.Price)
                                          .FirstOrDefault();
            return Ok(productoMasCaro);
        }

        // Ejercicio 6:
        [HttpGet("pedidos-recientes/{fecha}")]
        public IActionResult ObtenerPedidosRecientes(DateTime fecha)
        {
            var pedidos = _context.Orders
                                  .Where(p => p.OrderDate > fecha)
                                  .ToList();
            return Ok(pedidos);
        }

        // Ejercicio 7:
        [HttpGet("precio-promedio")]
        public IActionResult ObtenerPrecioPromedio()
        {
            var precioPromedio = _context.Products
                                         .Average(p => p.Price);
            return Ok(precioPromedio);
        }

        // Ejercicio 8:
        [HttpGet("productos-sin-descripcion")]
        public IActionResult ObtenerProductosSinDescripcion()
        {
            var productos = _context.Products
                                    .Where(p => string.IsNullOrEmpty(p.Description))
                                    .ToList();
            return Ok(productos);
        }

        // Ejercicio 9:
        [HttpGet("cliente-con-mas-pedidos")]
        public IActionResult ObtenerClienteConMasPedidos()
        {
            var cliente = _context.Orders
                                  .GroupBy(p => p.ClientId)
                                  .OrderByDescending(g => g.Count())
                                  .Select(g => new { ClienteId = g.Key, NumeroDePedidos = g.Count() })
                                  .FirstOrDefault();
            return Ok(cliente);
        }

        // Ejercicio 10:
        [HttpGet("pedidos-detalles")]
        public IActionResult ObtenerPedidosDetalles()
        {
            var detalles = _context.Orderdetails
                                   .Select(d => new { d.OrderId, d.Product.Name, d.Quantity })
                                   .ToList();
            return Ok(detalles);
        }

        // Ejercicio 11:
        [HttpGet("productos-cliente/{clientId}")]
        public IActionResult ObtenerProductosVendidosPorCliente(int clientId)
        {
            var productos = _context.Orderdetails
                                    .Where(d => d.Order.ClientId == clientId)
                                    .Select(d => d.Product.Name)
                                    .Distinct()
                                    .ToList();
            return Ok(productos);
        }

        // Ejercicio 12:
        [HttpGet("clientes-que-compraron-producto/{productId}")]
        public IActionResult ObtenerClientesQueCompraronProducto(int productId)
        {
            var clientes = _context.Orderdetails
                                   .Where(d => d.ProductId == productId)
                                   .Select(d => d.Order.Client)
                                   .Distinct()
                                   .ToList();
            return Ok(clientes);
        }
    }
}


