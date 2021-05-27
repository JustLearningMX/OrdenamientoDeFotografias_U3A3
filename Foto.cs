using System;

namespace DS_DPRN2_U3_A3_HICL
{
    class Foto
    {
        //Declaramos sus atributos
        protected string nombre;
        protected int numero;
        protected string color;

        //Propiedades Getter y Setter
        public string Nombre { get => nombre; set => nombre = value; }
        public int Numero { get => numero; set => numero = value; }
        public string Color { get => color; set => color = value; }

        //Constructor de la clase
        public Foto(string _nombre, int _numero, string _color)
        {
            //Guardamos los datos
            this.Nombre = _nombre;
            this.Numero = _numero;
            this.Color = _color;            
        }
    }
}
