using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PruebasUnitarias
{
    [TestClass]
    public class CursoTests
    {
        [TestMethod]
        public void Curso_PuedeAsignarseACategoria()
        {
            // Arrange
            var categoria = new Categoria { CategoriaId = 1, Nombre = "Programación" };
            var curso = new Curso
            {
                CursoId = 1,
                Nombre = "C# Básico",
                Categoria = categoria
            };

            // Act & Assert
            Assert.Equal("C# Básico", curso.Nombre);
            Assert.Equal("Programación", curso.Categoria?.Nombre);
        }
    }
}
