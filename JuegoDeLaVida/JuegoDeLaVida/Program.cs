﻿using System;


namespace Juegodelavida
{
	class Program
	{
		static void Main(string[] args)
		{
			const int MAXALTO = 50, MAXANCHO = 100;//ancho y alto de la pantalla
			int max = MAXALTO - 1, maxa = MAXANCHO - 1; //alto y ancho de la matriz
			//Console.SetBufferSize(MAXANCHO, MAXALTO);   //el buffer de la pantalla
			Console.SetWindowSize(MAXANCHO, MAXALTO);   //el tamaño de la pantalla
			int[,] tabla = new int[max, maxa];  //la matriz donde se almacenara
			int ninteracciones=0;       //contador de iteracciones
			Console.CursorVisible = false;
			aleatorio(tabla);

			Console.WriteLine(dibujartabla(tabla, max, maxa));
			while (true)
			{
				tabla = actualizarTabla(tabla);
				System.Threading.Thread.Sleep(75);
				Console.Write(dibujartabla(tabla, max, maxa));
				Console.Title = "Numero de generaciones "+ninteracciones++;	//muestra en el titulo de la consosa el numero de generaciones
			}
		}
		/// <summary>
		/// metodo para la parte gráfica del juego de la vida
		/// </summary>
		/// <param name="tabla">matriz</param>
		/// <param name="maxAlto">alto maximo</param>
		/// <param name="maxAncho">ancho maximo</param>
		/// <returns></returns>
		static string dibujartabla(int[,] tabla, int maxAlto, int maxAncho) // dibujar tabla
		{
			Console.SetCursorPosition(0, 0);
			string cadena = string.Empty;
			for (int i = 0; i < maxAlto; i++)
			{
				for (int j = 0; j < maxAncho; j++)
				{
					if (tabla[i, j] == 1) //escribe * si en esa casilla hay un 1
					{
						cadena += "*";
					}
					else
					{
						// si no pues pone un espacio
						cadena += " ";
					}

				}
				cadena += "\r\n";
			}
			return cadena;
		}
		/// <summary>
		/// actualiza la tabla con los nacimientos y las muertes
		/// </summary>
		/// <param name="tabla">tabla</param>
		/// <returns>nueva generacion de la tabla</returns>
		static int[,] actualizarTabla(int[,] tabla) // actualiza la tabla
		{
			int[,] nuevatabla = new int[tabla.GetLongLength(0), tabla.GetLength(1)];
			for (int i = 0; i < tabla.GetLongLength(0); i++)
			{
				for (int j = 0; j < tabla.GetLongLength(1); j++)
				{
					int vecino = 0;
					#region kk
					/* 
                    //control en la fila anterior
					try { vecino += tabla[i - 1, j - 1]; }//vecino numero 1
					catch { }
					try { vecino += tabla[i - 1, j]; } //vecino numero 2
					catch { }
					try { vecino += tabla[i - 1, j + 1]; }//vecino numero 3
					catch { }
					//control en la misma fila
					try { vecino += tabla[i, j + 1]; }//vecino numero 5
					catch { }
					try { vecino += tabla[i, j - 1]; }//vecino numero 4
					catch { }
					//control en la fila siguiente
					try { vecino += tabla[i + 1, j]; }//vecino numero 7
					catch { }
					try { vecino += tabla[i + 1, j - 1]; }//vecino numero 6
					catch { }
					try { vecino += tabla[i + 1, j + 1]; } //vecino numero 8
					catch { }
					*/
					#endregion

					vecino = contarvecinas(tabla, i, j);
					if (vecino < 2) nuevatabla[i, j] = 0; // muerte por soledad
					if (vecino > 3) nuevatabla[i, j] = 0; // muerte por sobrepoblacion
					if (tabla[i, j] == 0 && vecino == 3) nuevatabla[i, j] = 1; // nacimiento 
					if (vecino >= 2 && vecino <= 3 && tabla[i, j] == 1) nuevatabla[i, j] = 1; //se queda igual
				}
			}
			return nuevatabla;
		}
		/// <summary>
		/// pone celulas vivas en posiciones aleatorias
		/// </summary>
		/// <param name="tabla"></param>
		static void aleatorio(int[,] tabla)
		{
			Random posi = new Random();
			for (int i = 0; i < tabla.GetLength(0); i++)
			{
				for (int j = 0; j < tabla.GetLongLength(1); j++)
				{
					tabla[i, j] = posi.Next(0, 2);
				}
			}
		}
		/// <summary>
		/// cuenta las celulas vecinas
		/// </summary>
		/// <param name="M">matriz</param>
		/// <param name="f">fila</param>
		/// <param name="c">columna</param>
		/// <returns>numero de vecinas</returns>
		static int contarvecinas(int[,] M, int f, int c)
		{
			int fil = M.GetLength(0);                                               // +-------+ 
			int col = M.GetLength(1);                                               // |1  2  3|
			int nvivas = 0;                                                         // |4  C  5|  <-- C indica la celula y los numeros los vecinos
			// |6  7  8|
			// +-------+
			nvivas = M[((f - 1 + fil) % fil), ((c - 1 + col) % col)] + //vecino 1
				M[((f - 1 + fil) % fil), ((c + col) % col)] + //vecino 2
				M[((f - 1 + fil) % fil), ((c + 1 + col) % col)] +//vecino 3
				M[((f + fil) % fil), ((c - 1 + col) % col)] +//vecino 4
				M[((f + fil) % fil), ((c + 1 + col) % col)] +//vecino 5
				M[((f + 1 + fil) % fil), ((c - 1 + col) % col)] +//vecino 6
				M[((f + 1 + fil) % fil), ((c + col) % col)] +//vecino 7
				M[((f + 1 + fil) % fil), ((c + 1 + col) % col)];//vecino 8

			return nvivas;
		}
	}
}