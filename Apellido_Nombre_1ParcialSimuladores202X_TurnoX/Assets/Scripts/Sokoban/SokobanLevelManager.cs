using UnityEngine;
using System.Collections.Generic;

public class SokobanLevelManager : MonoBehaviour
{

    // esta clase deberia ser la que cree un tablero en base de una imagen

    public Texture2D[] imagenesTableros;    // referencio las imagenes para generar tableros
    private Texture2D imagenTablero;    // para referenciar en las funcionas la imagenTablero que quiero

    public GameObject casillero;
    public GameObject casilleroTarget;
    public GameObject jugador;
    public GameObject bloque;
    public GameObject pared;
 
    public static SokobanLevelManager instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public List<GameObject> dameLstPrefabsSokoban()
    {
        List<GameObject> lstPrefabsSokoban = new List<GameObject>();
        lstPrefabsSokoban.Add(casillero);
        lstPrefabsSokoban.Add(casilleroTarget);
        lstPrefabsSokoban.Add(jugador);
        lstPrefabsSokoban.Add(pared);
        lstPrefabsSokoban.Add(bloque);
        
        return lstPrefabsSokoban;
    }
                
    private Tablero dameTablero(int x, int y)
    {
        Tablero tablero = new Tablero(x, y);

        for (int i = 0; i < tablero.casilleros.GetLength(0); i++)
        {
            for (int j = 0; j < tablero.casilleros.GetLength(1); j++)
            {
                tablero.setearObjeto(casillero, new Vector2(i, j));
            }
        }

        return tablero;
    }

    public Nivel dameNivel(string nombre)
    {
        return SokobanLevelManager.instancia.dameNiveles(nombre).Find(x => x.Nombre == nombre);
    }

    private List<Nivel> dameNiveles(string nombre)
    {
        List<Nivel> lstNiveles = new List<Nivel>();
        lstNiveles.Add(new Nivel(nombre, SokobanLevelManager.instancia.dameTableroEnBaseAImagen(nombre)));  // cambie esto para que llame a mi nueva funcion
        return lstNiveles;
    }

    private Tablero dameTableroEnBaseAImagen(string nombre)
    {
        Tablero tablero = SokobanLevelManager.instancia.dameTablero(8, 8);

        if (nombre == "Nivel1") {imagenTablero = imagenesTableros[0];}
        else if (nombre == "Nivel2") { imagenTablero = imagenesTableros[1]; }



        for (int x = 0; x < imagenTablero.width; x++)
        {
            for (int y = 0; y < imagenTablero.height; y++)
            {
                
                Color pixel = imagenTablero.GetPixel(x, y);


                GameObject obj = null;

                if (pixel.Equals(Color.red))    // asigno cual prefab voy a instanciar segun el color
                {
                    // agrego una pared
                    obj = pared;
                }
                else if (pixel.Equals(Color.blue))
                {
                    // agrego al jugador
                    obj = jugador;
                }
                else if (pixel.Equals(Color.magenta))
                {
                    // agrego bloque
                    obj = bloque;
                }
                else if (pixel.Equals(Color.white))
                {
                    // agrego casillero target
                    obj = casilleroTarget;
                }


                if (obj != null)    // si hay algun objeto que posicionar
                {
                    tablero.setearObjeto(obj, new Vector2(x, y));       // paso que prefab es y la posicion segun los fors que estan navegando la imagen

                }

            }

        }




        //Color[] pixeles = imagenTablero.GetPixels();    // hago un array con los pixeles de mi imagen

        //int p = 0;  // esto va a servir como contador de pixeles en el foreach


        //foreach (Color pix in pixeles)
        //{
        //    GameObject obj = null;


        //    // por favor que reconozca los colores please

        //    if (pix.Equals(Color.red))
        //    {
        //        // agrego una pared
        //        obj = pared;
        //    }
        //    else if (pix.Equals(Color.blue))
        //    {
        //        // agrego al jugador
        //        obj = jugador;
        //    }
        //    else if (pix.Equals(Color.magenta))
        //    {
        //        // agrego bloque
        //        obj = bloque;
        //    }
        //    else if (pix.Equals(Color.green))
        //    {
        //        // agrego casillero target
        //        obj = casilleroTarget;
        //    }



            // tengo que fijarme el valor del contador
            // importante recordar que el array de pixeles esta ordenado de una manera particular
            // recorrio la imagen de izquierda a derecha y de abajo hacia arriba



            //if (obj != null)    // si hay algun objeto que posicionar
            //{
            





            //    if (p <= 7)    // si no es mayor a 7, el valor en X es el contador y en Y es 0
            //    {
            //        tablero.setearObjeto(obj, new Vector2(p, 0));
            //    }
            //    else if (p > 7 && p <= 15)    // si es mayor a 7 y no es mayor a 15, el valor en X es (contador - 8) y en Y es 1
            //    {
            //        tablero.setearObjeto(obj, new Vector2((p - 8), 1));
            //    }
            //    else if (p > 15 && p <= 23)    // si es mayor a 15 y no es mayor a 23, el valor en X es (contador - 16) y en Y es 2
            //    {
            //        tablero.setearObjeto(obj, new Vector2((p - 16), 2));
            //    }
            //    else if (p > 23 && p <= 31)   // si es mayor a 23 y no es mayor a 31, el valor en X es (contador - 24) y en Y es 3
            //    {
            //        tablero.setearObjeto(obj, new Vector2((p - 24), 3));
            //    }
            //    else if (p > 31 && p <= 39)   // si es mayor a 31 y no es mayor a 39, el valor en X es (contador - 32) y en Y es 4
            //    {
            //        tablero.setearObjeto(obj, new Vector2((p - 32), 4));
            //    }
            //    else if (p > 39 && p <= 47)   // si es mayor a 39 y no es mayor a 47, el valor en X es (contador - 40) y en Y es 5
            //    {
            //        tablero.setearObjeto(obj, new Vector2((p - 40), 5));
            //    }
            //    else if (p > 47 && p <= 55)   // si es mayor a 47 y no es mayor a 55, el valor en X es (contador - 48) y en Y es 6
            //    {
            //        tablero.setearObjeto(obj, new Vector2((p - 48), 6));
            //    }
            //    else if (p > 55 && p <= 63)   // si es mayor a 55 y no es mayor a 63, el valor en X es (contador - 56) y en Y es 7
            //    {
            //        tablero.setearObjeto(obj, new Vector2((p - 56), 7));
            //    }

            //}

            //p++;    // por cada bucle le agrego al contador


        //}



        // aca hay que hacer un for recorriendo un array de pixeles basadon en la textura2D que representa el nivel
        // si el color del pixel es amarillo, no se agrega nada
        // si el color del pixel es rojo, se agrega una pared
        // si el color del pixel es magenta, se agrega un bloque
        // si el color del piexel es verde, se agrega un casillerotarget



        //tablero.setearObjeto(pared, new Vector2(6, 6));
        //tablero.setearObjeto(jugador, new Vector2(1, 1));
        //tablero.setearObjeto(bloque, new Vector2(5, 4));
        //tablero.setearObjeto(bloque, new Vector2(3, 3));
        //tablero.setearObjeto(bloque, new Vector2(4, 4));
        //tablero.setearObjeto(casilleroTarget, new Vector2(1, 7));
        //tablero.setearObjeto(casilleroTarget, new Vector2(2, 7));
        //tablero.setearObjeto(casilleroTarget, new Vector2(3, 7));





        return tablero;

    }

    //private Tablero dameTableroNivel1()     
    //{
    //    Tablero tablero = SokobanLevelManager.instancia.dameTablero(8, 8);


    //    tablero.setearObjeto(pared, new Vector2(6, 6));
    //    tablero.setearObjeto(jugador, new Vector2(1,1));
    //    tablero.setearObjeto(bloque, new Vector2(5,4));
    //    tablero.setearObjeto(bloque, new Vector2(3, 3));
    //    tablero.setearObjeto(bloque, new Vector2(4, 4));
    //    tablero.setearObjeto(casilleroTarget, new Vector2(1, 7));
    //    tablero.setearObjeto(casilleroTarget, new Vector2(2, 7));
    //    tablero.setearObjeto(casilleroTarget, new Vector2(3, 7));
    //    return tablero;
    //}
}


