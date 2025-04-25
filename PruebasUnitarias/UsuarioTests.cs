using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PruebasUnitarias
{
    [TestClass]
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public List<string> Pedidos { get; set; } = new List<string>();
    }

    [TestClass]
    public class UsuarioTests
    {
        [TestMethod]
        public void Usuario_CreacionCorrecta()
        {
            // Preparar
            var usuario = new Usuario
            {
                UsuarioId = 1,
                Nombre = "Juan Pérez",
                Correo = "juan@example.com"
            };

            // Verificar
            Assert.AreEqual("Juan Pérez", usuario.Nombre);
            Assert.AreEqual("juan@example.com", usuario.Correo);
            Assert.IsNotNull(usuario.Pedidos);
            Assert.AreEqual(0, usuario.Pedidos.Count);
        }
    }
}
