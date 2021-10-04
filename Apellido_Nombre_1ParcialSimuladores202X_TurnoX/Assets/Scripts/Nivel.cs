
    public class Nivel
    {
        // esta clase puede que se tenga que editar para hacer que se instancien tableros en base a una imagen

        public string Nombre { get; set; }
        public Tablero Tablero { get; set; }

        public Nivel(string nombre, Tablero tablero)
        {
            Nombre = nombre;
            Tablero = tablero;
        }
    }
