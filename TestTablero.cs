using NUnit.Framework;

using Conecta4;

namespace Tests
{
    [TestFixture]
    public class TestTablero
    {

        /// <summary>
        /// Constructor del tablero vacía su contenido.
        /// </summary>
        [Test]
        public void TestCtor()
        {
            // Arrange
            Tablero tab = new Tablero();

            // Act
            TipoCasilla esquina1 = tab.GetCasilla(0, 0);
            TipoCasilla esquina2 = tab.GetCasilla(5, 0);
            TipoCasilla esquina3 = tab.GetCasilla(0, 6);
            TipoCasilla esquina4 = tab.GetCasilla(5, 6);

            // Assert
            Assert.That(esquina1, Is.EqualTo(TipoCasilla.VACIA), "ERROR: nada más construirse un tablero tiene las esquinas vacías");
            Assert.That(esquina2, Is.EqualTo(TipoCasilla.VACIA), "ERROR: nada más construirse un tablero tiene las esquinas vacías");
            Assert.That(esquina3, Is.EqualTo(TipoCasilla.VACIA), "ERROR: nada más construirse un tablero tiene las esquinas vacías");
            Assert.That(esquina4, Is.EqualTo(TipoCasilla.VACIA), "ERROR: nada más construirse un tablero tiene las esquinas vacías");

        }


        [Test]
        public void TestGetCasilla()
        {
            // Arrange
            Tablero tab = new Tablero();
            tab.ColocaFicha(2, 3, TipoCasilla.ROJA);
            tab.ColocaFicha(3, 5, TipoCasilla.AMARILLA);

            // Act
            TipoCasilla dosTres = tab.GetCasilla(2, 3);
            TipoCasilla tresCinco = tab.GetCasilla(3, 5);

            // Assert
            Assert.That(dosTres, Is.EqualTo(TipoCasilla.ROJA), "ERROR: GetCasilla no devuelve el color establecido.");
            Assert.That(tresCinco, Is.EqualTo(TipoCasilla.AMARILLA), "ERROR: GetCasilla no devuelve el color establecido.");
        }

        /// <summary>
        /// Ante dos colocaciones de ficha en la misma posición, prevalece la primera
        /// </summary>
        [Test]
        public void TestColocaFichaDoble()
        {
            // Arrange
            Tablero tab = new Tablero();
            tab.ColocaFicha(2, 3, TipoCasilla.ROJA);
            tab.ColocaFicha(2, 3, TipoCasilla.AMARILLA);

            // Act
            TipoCasilla color = tab.GetCasilla(2, 3);

            // Assert
            Assert.That(color, Is.EqualTo(TipoCasilla.ROJA), "ERROR: la segunda llamada a ColocaFicha con la misma posición no tiene efecto.");

        }

        /// <summary>
        /// Colocar una ficha VACIA no tiene efecto.
        /// </summary>
        [Test]
        public void TestColocaFichaVacia()
        {
            // Arrange
            Tablero tab = new Tablero();
            tab.ColocaFicha(2, 3, TipoCasilla.ROJA);
            tab.ColocaFicha(2, 3, TipoCasilla.VACIA);

            // Act
            TipoCasilla color = tab.GetCasilla(2, 3);

            // Assert
            Assert.That(color, Is.EqualTo(TipoCasilla.ROJA), "ERROR: colocar una ficha VACIA no debe cambiar lo que hubiera.");
        }

        /// <summary>
        /// Colocar fuera de los límites no tiene efecto.
        /// </summary>
        [Test]
        public void TestColocaOutOfBounds()
        {
            // Arrange
            Tablero tab = new Tablero();

            // Act

            // Assert
            Assert.DoesNotThrow(() => tab.ColocaFicha(-1, 3, TipoCasilla.ROJA));
            Assert.DoesNotThrow(() => tab.ColocaFicha(3, -1, TipoCasilla.ROJA));
            Assert.DoesNotThrow(() => tab.ColocaFicha(-5, -10, TipoCasilla.ROJA));
            Assert.DoesNotThrow(() => tab.ColocaFicha(10, 3, TipoCasilla.ROJA));
            Assert.DoesNotThrow(() => tab.ColocaFicha(3, 15, TipoCasilla.ROJA));
            Assert.DoesNotThrow(() => tab.ColocaFicha(15, 15, TipoCasilla.ROJA));
        }

        /// <summary>
        /// Acceder fuera de los límites no genera excepciones
        /// </summary>
        [Test]
        public void TestGetOutOfBounds()
        {
            // Arrange
            Tablero tab = new Tablero();

            // Act

            // Assert
            Assert.DoesNotThrow(() => tab.GetCasilla(-1, 3));
            Assert.DoesNotThrow(() => tab.GetCasilla(3, -1));
            Assert.DoesNotThrow(() => tab.GetCasilla(-5, -10));
            Assert.DoesNotThrow(() => tab.GetCasilla(10, 3));
            Assert.DoesNotThrow(() => tab.GetCasilla(3, 15));
            Assert.DoesNotThrow(() => tab.GetCasilla(15, 15));
        }

        /// <summary>
        /// Acceder fuera de los límites devuelve VACIA
        /// </summary>
        [Test]
        public void TestResultGetOutOfBounds()
        {
            // Arrange
            Tablero tab = new Tablero();

            // Act
            TipoCasilla color = tab.GetCasilla(-3, 1);

            // Assert
            Assert.That(color, Is.EqualTo(TipoCasilla.VACIA), "ERROR: acceder fuera del tablero debe devolver VACIA.");
        }


        // CaeFicha deja caer una ficha en la columna indicada
        [Test]
        public void TestCaeFichaSimple()
        {
            // Arrange
            Tablero tab = new Tablero();
            tab.CaeFicha(2, TipoCasilla.ROJA);

            // Act
            TipoCasilla colorEnLaBase = tab.GetCasilla(2, 0);
            TipoCasilla colorEncimaBase = tab.GetCasilla(2, 1);
            TipoCasilla colorArriba = tab.GetCasilla(2, 5);

            // Assert
            Assert.That(colorEnLaBase, Is.EqualTo(TipoCasilla.ROJA), "ERROR: al dejar caer una ficha en un tablero vacío debe llegar hasta abajo.");
            Assert.That(colorEncimaBase, Is.EqualTo(TipoCasilla.VACIA), "ERROR: al dejar caer una ficha en un tablero vacío debe llegar hasta abajo.");
            Assert.That(colorArriba, Is.EqualTo(TipoCasilla.VACIA), "ERROR: al dejar caer una ficha en un tablero vacío debe llegar hasta abajo.");
        }


        // CaeFicha deja caer una ficha en la columna indicada y la segunda queda
        // encima
        [Test]
        public void TestCaeFichaDoble()
        {
            // Arrange
            Tablero tab = new Tablero();
            tab.CaeFicha(0, TipoCasilla.ROJA);
            tab.CaeFicha(0, TipoCasilla.AMARILLA);

            // Act
            TipoCasilla colorEnLaBase = tab.GetCasilla(0, 0);
            TipoCasilla colorEncimaBase = tab.GetCasilla(0, 1);
            TipoCasilla colorArriba = tab.GetCasilla(0, 5);

            // Assert
            Assert.That(colorEnLaBase, Is.EqualTo(TipoCasilla.ROJA), "ERROR: al dejar caer una ficha en un tablero vacío debe llegar hasta abajo.");
            Assert.That(colorEncimaBase, Is.EqualTo(TipoCasilla.AMARILLA), "ERROR: al dejar caer una segunda ficha debe quedar justo encima de la primera.");
            Assert.That(colorArriba, Is.EqualTo(TipoCasilla.VACIA), "ERROR: al dejar caer dos fichas en la misma columna, la celda superior debe quedar VACIA.");
        }
    }
}
