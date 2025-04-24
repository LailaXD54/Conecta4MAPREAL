using Conecta4;

using NUnit.Framework;


namespace TEST1
{
    [TestFixture]
    public class TestReglas
    {
        [Test]
        public void TestQuienEmpieza()
        {
            // Arrange
            Reglas r = new Reglas();

            // Act
            TipoCasilla empieza = r.QuienEmpieza();

            // Assert
            Assert.That(empieza, Is.EqualTo(TipoCasilla.AMARILLA), "ERROR: Las reglas del juego deben indicar que empizan las amarillas.");
        }

        [Test]
        public void TestNoHaySiguienteAVacia()
        {
            // Arrange
            Reglas r = new Reglas();

            // Act
            TipoCasilla siguiente = r.ColorContrario(TipoCasilla.VACIA);

            // Assert
            Assert.That(siguiente, Is.EqualTo(TipoCasilla.VACIA), "ERROR: el color contrario a VACIA es VACIA");
        }


        [Test]
        public void TestColorContrario()
        {
            // Arrange
            Reglas r = new Reglas();

            // Act
            TipoCasilla inicial = TipoCasilla.ROJA;
            TipoCasilla turno1 = r.ColorContrario(inicial);
            TipoCasilla turno2 = r.ColorContrario(turno1);

            // Assert
            Assert.That(turno1, Is.EqualTo(TipoCasilla.AMARILLA), "ERROR: el color contrario a ROJA es AMARILLA");
            Assert.That(turno2, Is.EqualTo(TipoCasilla.ROJA), "ERROR: el color contrario a AMARILLA es ROJA");
        }

        [Test]
        public void PuedePonerFichaColumnaVacía()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Assert and Act
            Assert.That(r.PuedePoner(tab, 1), Is.True, "ERROR: debería poder poner en columna vacía");

        }

        [Test]
        public void PuedePonerFichaColumnaLlena()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Act
            for (int fila = 0; fila < 6; fila++)
            {
                tab.ColocaFicha(3, fila, TipoCasilla.ROJA);
            }

            // Assert
            Assert.That(r.PuedePoner(tab, 3), Is.False, "ERROR: no debería poder poner en columna llena");
        }

        [Test]
        public void CuantasSeguidasHorizontal()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Act
            tab.ColocaFicha(0, 0, TipoCasilla.ROJA);
            tab.ColocaFicha(1, 0, TipoCasilla.ROJA);
            tab.ColocaFicha(2, 0, TipoCasilla.ROJA);

            // Assert
            Assert.That(r.CuantasSeguidas(tab, 0, 0, 1, 0), Is.EqualTo(3), "ERROR: debería contar 3 seguidas horizontalmente");
        }

        [Test]
        public void CuantasSeguidasVertical()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Act
            tab.ColocaFicha(3, 0, TipoCasilla.AMARILLA);
            tab.ColocaFicha(3, 1, TipoCasilla.AMARILLA);

            // Assert
            Assert.That(r.CuantasSeguidas(tab, 3, 0, 0, 1), Is.EqualTo(2), "ERROR: debería contar 2 seguidas verticalmente");
        }

        [Test]
        public void CuantasSeguidasDiagonal()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Act
            tab.ColocaFicha(4, 0, TipoCasilla.ROJA);
            tab.ColocaFicha(5, 1, TipoCasilla.ROJA);
            tab.ColocaFicha(6, 2, TipoCasilla.ROJA);

            // Assert
            Assert.That(r.CuantasSeguidas(tab, 4, 0, 1, 1), Is.EqualTo(3), "ERROR: debería contar 3 seguidas diagonalmente");
        }

        [Test]
        public void CuantasSeguidasPosicionVacia()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Assert and Act
            Assert.That(r.CuantasSeguidas(tab, 0, 5, 1, 0), Is.EqualTo(1), "ERROR: casilla vacía debería devolver 1");
        }

        [Test]
        public void CuantasSeguidasFueraDeLimites()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Act
            tab.ColocaFicha(6, 5, TipoCasilla.AMARILLA);
            int resultado1 = r.CuantasSeguidas(tab, 6, 5, 1, 0);
            int resultado2 = r.CuantasSeguidas(tab, 6, 5, 0, 1);
            int resultado3 = r.CuantasSeguidas(tab, -1, -1, 1, 1);

            // Assert
            Assert.That(resultado1, Is.EqualTo(1), "ERROR: al salir de los límites del tablero debería devolver 1");
            Assert.That(resultado2, Is.EqualTo(1), "ERROR: al salir de los límites verticales debería devolver 1");
            Assert.That(resultado3, Is.EqualTo(1), "ERROR: posición inicial inválida debería devolver 1");
        }

        [Test]
        public void CuantasSeguidasConCambioDeColor()
        {
            // Arrange
            Reglas r = new Reglas();
            Tablero tab = new Tablero();

            // Act
            tab.ColocaFicha(0, 1, TipoCasilla.ROJA);
            tab.ColocaFicha(1, 1, TipoCasilla.AMARILLA);

            // Assert
            Assert.That(r.CuantasSeguidas(tab, 0, 1, 1, 0), Is.EqualTo(1), "ERROR: debería parar al cambiar de color");
        }

    }
}
