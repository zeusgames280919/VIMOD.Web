namespace Pruebas
{
    [TestClass]
    public sealed class Test1
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
