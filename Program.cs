using System;
namespace DS_DPRN2_U3_A3_HICL
{

    class Program
    {
        static void Main()//Método principal
        {
            int? numeroDeFotos = null;//Variable para almacenar el # de imagenes a checar

            try//Esta sentencia Try-Catch gestionará las excepciones
            {  //que se produzcan en el programa

                numeroDeFotos = MenuPrincipal();//Se llama al menu principal

                //Creamos una matriz para el número de fotos con los 6 colores por verificar
                CrearMatriz(numeroDeFotos);

                Console.ReadKey();//Espera de una tecla
            }
            catch (Exception e)
            {   //Se invoca método para controlar los errores
                ControlarExcepciones(e);//Se envía por parámetro el error
                Console.ReadKey();//Espera de una tecla
                Main();//Después de indicar el error, se vuelve al menú principal
            }
        }

        static void CrearMatriz(int? numeroDeFotos)//Método que crea la matriz
        {
            //Creamos un arreglo o matriz bidimensional para capturar las fotos
            string[,] fotos = new string[6, (Int16)numeroDeFotos];

            //Creamos un arreglo o matriz para capturar las instancias de fotos
            Foto[] fotografia = new Foto[(Int16)numeroDeFotos];

            //Variable boleana para capturar colores o no
            bool continua = true;

            for (int i = 0; i < numeroDeFotos; i++)//Avanzamos foto por foto
            {

                string temporal = null;
                //Checamos si es blanco/negro o a color la foto
                string color = null;
                string blancoNegro = null;
                string tipo = null;

                EtiquetasMatriz();//Invocamos método para imprimir etiquetas

                //Imprimimos el número de foto a capturar
                Console.SetCursorPosition(67, 14);
                Console.WriteLine(i + 1);

                for (int j = 0; j < 6; j++)//Recorremos los colores
                {
                    while (continua)//Mientras no ingresen una X o espacio en blanco
                    {
                        //El usuario ingresa los colores
                        Console.SetCursorPosition(56, 16 + j);
                        temporal = Console.ReadLine();//Se guarda en var temporal

                        //Validamos que lo capturado sea X o espacio en blanco
                        if (temporal.ToLower().Equals("x") || temporal.Equals(""))
                        {
                            fotos[j, i] = temporal;//si es correcto se almacena el valor
                            continua = false;//Se sale del ciclo while

                            if (j <= 2)
                            {//indices 0-2 son colores
                                color = color + fotos[j, i];
                            }
                            else
                            {//indices 3-5 son bco, ngo y gris
                                blancoNegro = blancoNegro + fotos[j, i];
                            }

                        }
                        else
                        {   //Si no es X o espacio en blanco, se informa
                            Console.SetCursorPosition(46, 25);
                            Console.WriteLine("¡Ingrese X o deje en blanco!");
                            Console.ReadKey();

                            //Se limpian campos
                            Console.SetCursorPosition(46, 25);
                            Console.WriteLine("                             ");
                            Console.SetCursorPosition(56, 16 + j);
                            Console.WriteLine(" ");

                            continua = true;//Vuelve a pedir que se ingrese
                        }
                    }
                    continua = true;//Se reestablece el valor para la siguiente iteración
                } //FIN DEL SEGUNDO CICLO FOR j

                if (!String.IsNullOrEmpty(color))//Si la foto tiene uno de los colores
                {
                    tipo = "Color";
                }//Si la foto tiene un negro o gris o blanco PERO ningún color
                else if (!String.IsNullOrEmpty(blancoNegro) && String.IsNullOrEmpty(color))
                {
                    tipo = "Blanco y negro";
                }//si no se seleccionó ningún color
                else
                {
                    tipo = "Indeterminado";
                }

                //Instanciamos la clase Foto dentro del arreglo de Foto
                //pasando los valores al constructor de la clase Foto
                fotografia[i] = new Foto("Foto " + (i + 1), i + 1, tipo);

                //Invocamos método para imprimir el resultado mandando
                //por parámetro el objeto instanciado previamente
                ImprimirResultadoFoto(fotografia[i]);
            }
        }

        static void ImprimirResultadoFoto(Foto _foto)
        {
            //Método que imprime etiquetas
            EtiquetasResultado();

            Console.SetCursorPosition(82, 16 + _foto.Numero);//Impresión en pantalla
            Console.WriteLine(" {0} es a {1}", _foto.Nombre, _foto.Color);

            Console.ReadKey();
        }

        static void EtiquetasResultado()//Etiquetas en pantalla
        {
            Console.SetCursorPosition(82, 13);
            Console.WriteLine("|----------------------------|");
            Console.SetCursorPosition(82, 14);
            Console.WriteLine("|   ORDEN DE LAS FOTOS       |");
            Console.SetCursorPosition(82, 15);
            Console.WriteLine("|----------------------------|");
        }

        static void EtiquetasMatriz()//Método para imprimir etiquetas en pantalla
        {
            Console.SetCursorPosition(42, 13);
            Console.WriteLine("|-----------------------------------|");
            Console.SetCursorPosition(42, 14);
            Console.WriteLine("| CAPTURAR FOTO NÚMERO: [   ]       |");
            Console.SetCursorPosition(42, 15);
            Console.WriteLine("|-----------------------------------|");
            Console.SetCursorPosition(42, 16);
            Console.WriteLine("|     CIAN:  [ ]      Ingrese una   |");
            Console.SetCursorPosition(42, 17);
            Console.WriteLine("|  MAGENTA:  [ ]        [X] si la   |");
            Console.SetCursorPosition(42, 18);
            Console.WriteLine("| AMARILLO:  [ ]      imagen tiene  |");
            Console.SetCursorPosition(42, 19);
            Console.WriteLine("|   BLANCO:  [ ]        un color.   |");
            Console.SetCursorPosition(42, 20);
            Console.WriteLine("|     GRIS:  [ ]      Si no, deje   |");
            Console.SetCursorPosition(42, 21);
            Console.WriteLine("|    NEGRO:  [ ]       en blanco    |");
            Console.SetCursorPosition(42, 22);
            Console.WriteLine("|-----------------------------------|");
        }

        static int? MenuPrincipal()//Menú que imprime etiquetas
        {                          //para el total de fotos
            int? numeroDeFotos = null;

            DibujaTitulo();
            //Menú de opciones
            Console.SetCursorPosition(42, 9);
            Console.WriteLine("|-----------------------------------|");
            Console.SetCursorPosition(42, 10);
            Console.WriteLine("| Número de fotos: [   ]            |");
            Console.SetCursorPosition(42, 11);
            Console.WriteLine("|-----------------------------------|");

            numeroDeFotos = CapturarNumeroFotos();//Se invoca método de captura total de fotos

            //Para lanzar excepción propia si # fotos < 1 o > 100
            if (numeroDeFotos < 1 || numeroDeFotos > 100)
            {
                //se lanza la excepción propia
                throw new GestionDeErroresPropiosException("INGRESE UN NÚMERO MAYOR 1 Y MENOR A 100");
            }

            return numeroDeFotos;
        }

        static int? CapturarNumeroFotos()//Método para captura del total de fotos
        {//cualquier excepción que se presente subirá hasta el método main
            int? numeroDeFotos = null;
            Console.SetCursorPosition(62, 10);
            numeroDeFotos = Convert.ToInt32(Console.ReadLine());

            return numeroDeFotos;
        }

        static void ControlarExcepciones(Exception e)//Método para controlar las 
        //excepciones. Según la excepción muestra un mensaje
        {
            //Variable para almacenar tipo de error
            string error = null;
            error = Convert.ToString(e.GetType());

            //Mensaje para el usuario
            Console.SetCursorPosition(40, 24);
            Console.WriteLine("** ¡SE PRODUJO UN ERROR! **");
            Console.SetCursorPosition(40, 26);
            Console.WriteLine("Error: " + e.GetType());
            Console.SetCursorPosition(40, 27);
            Console.WriteLine(e.Message);

            //Si es un error por que se ingresó letras
            if (error.Equals("System.FormatException"))
            {
                Console.SetCursorPosition(40, 25);
                Console.WriteLine("¡INGRESE NÚMEROS SIN LETRAS!");

            }

            //Si es un error por que se ingresó un número muy grande o muy pequeño
            if (error.Equals("System.OverflowException"))
            {
                Console.SetCursorPosition(40, 25);
                Console.WriteLine("** ¡NÚMERO MUY GRANDE! **");
            }

            //Si es un error por fuera de rango de un array
            if (error.Equals("System.IndexOutOfRangeException"))
            {
                Console.SetCursorPosition(40, 25);
                Console.WriteLine("SUPERÓ EL RANGO DE LA MATRIZ");
            }
        }

        static void DibujaTitulo()//Método que permite agregar título a la ventana principal
        {
            //Mensajes de bienvenida al programa
            Console.Clear();
            Console.SetCursorPosition(40, 1);
            Console.WriteLine("****************************************");
            Console.SetCursorPosition(40, 2);
            Console.WriteLine("*        Programación .NET II          *");
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("*               Unidad 3               *");
            Console.SetCursorPosition(40, 4);
            Console.WriteLine("*              Actividad 3             *");
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("*   Manejo seguro de arreglos en C#    *");
            Console.SetCursorPosition(40, 6);
            Console.WriteLine("*      Alumno: Hiram Chávez López      *");
            Console.SetCursorPosition(40, 7);
            Console.WriteLine("****************************************");
        }
    }
}