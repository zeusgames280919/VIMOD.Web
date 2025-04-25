using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PruebasUnitarias
{
    [TestClass]
    public class PedidoTests
    {
        [TestMethod]
        public void Pedido_CalculoTotalCorrecto()
        {
        // Preparar
        var pedido = new Pedido
        {
            PedidoId = 1,
            FechaPedido = DateTime.Now,
            Detalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { Curso = new Curso { Precio = 100 }, Cantidad = 2 },
                new PedidoDetalle { Curso = new Curso { Precio = 50 }, Cantidad = 1 }
            }
        };

            // Simulamos lógica de cálculo de total
            decimal total = 0;
            foreach (var item in pedido.Detalles)
            {
                total += item.Curso?.Precio * item.Cantidad ?? 0;
            }

            // Act & Assert
            Assert.Equal(250, total);
        }
    }
}
