using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conecta4
{
    public class GUI
    {
        /// <summary>
        /// Escribe el tablero y a quién le toca. Si el turno es TipoCasilla.VACIA,
        /// entonces no escribe a quién le toca :-)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="turno"></param>
        public static void PintaTablero(Tablero t, TipoCasilla turno)
        {
            for (int y = 5; y >= 0; --y)
            {
                string line = "|";
                for (int x = 0; x < 7; ++x)
                {
                    line += ToChar(t.GetCasilla(x, y));
                }
                line += "|";

                Console.WriteLine(line);
            }

            for (int x = 0; x < 2 + 7; ++x)
                Console.Write('=');
            Console.WriteLine();
            Console.WriteLine();

            if (turno != TipoCasilla.VACIA)
            {
                Console.WriteLine("Juegan " + ToUserFriendly(turno) + " (" + ToChar(turno) + ")");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Pregunta por una columna. No comprueba que el valor introducido esté en el rango.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="turno"></param>
        /// <returns></returns>
        public static int PideColumna()
        {
            return LeeEntero("Indica la columna donde colocar la ficha: ");
        }

        public static void EscribeGanador(Tablero t, TipoCasilla ganador)
        {
            Console.WriteLine();
            Console.WriteLine();
            PintaTablero(t, TipoCasilla.VACIA);

            Console.WriteLine("FIN DE JUEGO. Ganan " + ToUserFriendly(ganador));
        }

        public static bool JugarOtra()
        {
            Console.WriteLine();
            string respuesta;
            do
            {
                Console.Write("Otra partida? (S/N): ");
                respuesta = Console.ReadLine();
                respuesta = respuesta.ToLower();
            } while ((respuesta != "s") && (respuesta != "n"));

            return respuesta == "s";
        }
        private static char ToChar(TipoCasilla t)
        {
            switch (t)
            {
                case TipoCasilla.VACIA: return ' ';
                case TipoCasilla.ROJA: return 'X';
                case TipoCasilla.AMARILLA: return 'O';
            }
            return ' '; // Aquí no llegará nunca...
        }

        private static string ToUserFriendly(TipoCasilla t)
        {
            switch (t)
            {
                case TipoCasilla.ROJA: return "rojas";
                case TipoCasilla.AMARILLA: return "amarillas";
            }
            return "";
        }

        private static int LeeEntero(string prompt)
        {
            int ret;
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (!Int32.TryParse(input, out ret));

            return ret;
        }

        public static void Main()
        {
            Reglas reglas;
            do
            {
                reglas = new Reglas();
                Tablero tablero = reglas.TableroInicial();
                TipoCasilla turno = reglas.QuienEmpieza();
                turno = reglas.ColorContrario(turno); // El "último en poner" fue el contrario al que empieza
                do
                {
                    turno = reglas.ColorContrario(turno);
                    PintaTablero(tablero, turno);
                    int col;
                    do
                    {
                        col = PideColumna();
                    } while (!tablero.CaeFicha(col - 1, turno));

                } while (!reglas.Gana(tablero, turno) && !tablero.TableroCompleto());
                if (!tablero.TableroCompleto())
                    EscribeGanador(tablero, turno);
                else
                {
                    PintaTablero(tablero, TipoCasilla.VACIA);
                    Console.WriteLine(" ==== TABLAS ====");
                }
            } while (JugarOtra());
        }

    }
}
