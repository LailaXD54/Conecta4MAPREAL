using System;

namespace Conecta4
{
    public class Conecta4
    {
        
    }

    public class Tablero
    {
        private TipoCasilla[,] tablero;
        private int filas;
        private int columnas;
        public Tablero(int filas = 6, int columnas = 7)
        {
            this.filas = filas;
            this.columnas = columnas;
            tablero = new TipoCasilla[filas, columnas]; // [FILAS, COLUMNAS]

            for (int fila = 0; fila < filas; fila++)
            {
                for (int col = 0; col < columnas; col++)
                {
                    tablero[fila, col] = TipoCasilla.VACIA; // Consistente con [fila,col]
                }
            }
        }

        public void ColocaFicha(int col, int fil, TipoCasilla tipo)
        {
            try
            {
                if(tablero[fil, col] == TipoCasilla.VACIA) tablero[fil, col] = tipo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool TableroCompleto()
        {
            int fila = 0, col = 0;
            bool completo = false;
            int vacio = 0;

            while (fila < filas)
            {
                col = 0;
                while (col < columnas)
                {
                    if (tablero[fila, col] == TipoCasilla.VACIA)
                    {
                        vacio++;
                    }
                    col++;
                }
                fila++;
            }
            return completo = vacio == 0;
        }

        public TipoCasilla GetCasilla(int col, int fil)
        {
            try
            {
                return tablero[fil, col];
            }
            catch (IndexOutOfRangeException)
            {
                return TipoCasilla.VACIA;
            }
        }

        public bool CaeFicha(int n, TipoCasilla tipo) //Coloca la ficha en la columna n, y si hay algo entonces se pone arriba
        {
            int fil = 0;
            bool cae = false;

            if (tipo != TipoCasilla.VACIA)
            {
                try
                {
                    if (tablero[fil, n] == TipoCasilla.VACIA) {
                        tablero[fil, n] = tipo;
                        cae = true;
                    }
                    else if (n < columnas) {
                        while (fil < filas && !cae) {
                            fil++;
                            if (tablero[fil, n] == TipoCasilla.VACIA) {
                                tablero[fil, n] = tipo;
                                cae = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return cae;

        }
        public int GetNumFilas() {
            return filas;
        }

        public int GetNumColumnas() {
            return columnas;
        }
    }


    public enum TipoCasilla //COMPLETADO
    {
        VACIA,
        ROJA,
        AMARILLA
    }

    public class Reglas //COMPLETADO
    {
        public TipoCasilla QuienEmpieza()
        {
            return TipoCasilla.AMARILLA;
        }

        public TipoCasilla ColorContrario(TipoCasilla tipo)
        {
            if (tipo == TipoCasilla.VACIA)
            {
                return tipo;
            }
            else if (tipo == TipoCasilla.ROJA)
            {
                return TipoCasilla.AMARILLA;
            }
            else return TipoCasilla.ROJA;
        }

        public bool PuedePoner(Tablero t, int columna)
        {
            return t.GetCasilla(columna, 5) == TipoCasilla.VACIA;
        }

        /// <summary>
        /// Señalas cuantas casillas estan seguidas
        /// ox y xy son para ver la columna y fila y de esta localizar la ficha colocada
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="ox"></param>
        /// <param name="oy"></param>
        /// <param name="incrx"></param>
        /// <param name="incry"></param>
        /// <returns></returns>
       /*
        public int CuantasSeguidas(Tablero tab, int ox, int oy, int incrx, int incry) //usado mas tarde como privado para el metodo publico de Gana, incrementa de x y y para un for en gana que recorre el tablero será (1,1)(1,0)(-1,0)(-1,1)(0,1)(0,-1)
        {
            TipoCasilla expected = tab.GetCasilla(ox, oy);
            int ret = 1;

            while (tab.GetCasilla(ox + incrx, oy + incry) == expected)
            {
                ox += incrx;
                oy += incry;
                ++ret;
            }
            return ret;
        }

        */
        public int CuantasSeguidas(Tablero tab, int ox, int oy, int incrx, int incry) //usado mas tarde como privado para el metodo publico de Gana, incrementa de x y y para un for en gana que recorre el tablero será (1,1)(1,0)(-1,0)(-1,1)(0,1)(0,-1)
        {
            TipoCasilla expected = tab.GetCasilla(ox, oy);

            int ret = 1;
            if (expected != TipoCasilla.VACIA) {
                while (ox >= 0 && ox < tab.GetNumColumnas() && oy >= 0 && oy < tab.GetNumFilas() && tab.GetCasilla(ox + incrx, oy + incry) == expected) {
                    ox += incrx;
                    oy += incry;
                    ++ret;
                }
            }
            return ret;
        }
        public Tablero TableroInicial()
        {
            Tablero tab = new Tablero();
            return tab;
        }

        public bool Gana(Tablero tab, TipoCasilla tipo) //entra en esta la tabla y el que juega en este turno
        {
            for (int i = 0; i < tab.GetNumFilas(); i++) {
                for (int j = 0; j < tab.GetNumColumnas(); j++) {
                    if (tab.GetCasilla(j, i) == tipo) {
                        int[,] direcciones = new int[,] {
                            {1, 0},
                            {0, 1},
                            {1, 1},
                            {-1, 1}
                        };

                        for (int k = 0; k < 2; k++) {
                            int dx = direcciones[k, 0];
                            int dy = direcciones[k, 1];

                            if (CuantasSeguidas(tab, j, i, dx, dy) >= 4)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

    }

}
