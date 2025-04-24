using NUnit.Framework;

using Conecta4;

namespace TEST1
{
    [TestFixture]
    public class TableroCompleto
    {

        [Test]
        public void TestCompletoConColocaFicha()
        {
            // Arrange
            Tablero tab = new Tablero();

            for (int y = 0; y < 6; ++y)
            {
                for (int x = 0; x < 7; ++x)
                    tab.ColocaFicha(x, y, TipoCasilla.ROJA);
            }

            // Act
            bool completo = tab.TableroCompleto();

            // Assert
            Assert.That(completo, Is.True);
        }

        [Test]
        public void TestCompletoConCaeFicha()
        {
            // Arrange
            Tablero tab = new Tablero();

            for (int y = 0; y < 6; ++y)
            {
                for (int x = 0; x < 7; ++x)
                {
                    tab.CaeFicha(x, TipoCasilla.ROJA);
                }
            }

            // Act
            bool completo = tab.TableroCompleto();

            // Assert
            Assert.That(completo, Is.True);
        }

        [Test]
        public void TestNoCompleto()
        {
            // Arrange
            Tablero tab = new Tablero();

            for (int y = 0; y < 6; ++y)
            {
                for (int x = 0; x < 7; ++x)
                {
                    tab.ColocaFicha(x % 4, y, TipoCasilla.ROJA);
                }
            }

            // Act
            bool completo = tab.TableroCompleto();

            // Assert
            Assert.That(completo, Is.False, "ERROR: llamar 42 veces a ColocaFicha no implica un tablero completo.");
        }

    }
}
