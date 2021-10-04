using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SokobanGameManager : MonoBehaviour
{
    Nivel nivel, nivelAux;
    GameObject casillero, casilleroTarget, pared, jugador, bloque;
    List<Vector2> posOcupadasEsperadasCasillerosTarget;
    List<Vector2> posOcupadasBloques;   // agrego una lista para saber donde hay bloques en el nivel

    Stack pilaTablerosAnteriores;

    string orientacionJugador;
    string nombreNivelActual = "Nivel1";
    bool gameOver = false;
    bool estoyDeshaciendo = false;


    private void Start()
    {
        casillero = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Casillero");
        casilleroTarget = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "CasilleroTarget");
        pared = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Pared");
        jugador = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Jugador");
        bloque = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Bloque");

        CargarNivel(nombreNivelActual);

        gameOver = false;

        pilaTablerosAnteriores = new Stack();   

    }

    private void CargarNivel(string nombre)
    {
        nivel = SokobanLevelManager.instancia.dameNivel(nombre);
        posOcupadasEsperadasCasillerosTarget = nivel.Tablero.damePosicionesObjetos("CasilleroTarget");
        posOcupadasBloques = nivel.Tablero.damePosicionesObjetos("Bloque");
        InstanciadorPrefabs.instancia.graficarCasilleros(nivel.Tablero, casillero);
        InstanciadorPrefabs.instancia.graficarCasillerosTarget(nivel.Tablero, casilleroTarget);
        InstanciadorPrefabs.instancia.graficarObjetosTablero(nivel.Tablero, SokobanLevelManager.instancia.dameLstPrefabsSokoban());
    }

    private void Update()
    {
        if (gameOver == false)  // si el juego no acabo
        {

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                orientacionJugador = "derecha";
                mover();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) // me quiero mover pa la izq
            {
                orientacionJugador = "izquierda";
                mover();
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                orientacionJugador = "arriba";
                mover();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) // me quiero mover pa abajo
            {
                orientacionJugador = "abajo";
                mover();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                estoyDeshaciendo = true;
                mover();
            }
        }
        else { }

    }

    private void mover()
    {
        if (estoyDeshaciendo == false)  // si no estoy deshaciendo
        {
            Debug.Log("Intente moverme.");

            Vector2 posicionJugador = new Vector2(nivel.Tablero.damePosicionObjeto("Jugador").x, nivel.Tablero.damePosicionObjeto("Jugador").y);
            GameObject objProximo, objProximoProximo;
            objProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, 1);
            objProximoProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, 2);

            if (objProximo != null && objProximo.CompareTag("casillero"))   // si el objeto proximo es un casillero vacio, me muevo
            {
                GuardarTablero();   // guardo el tablero antes de moverme

                nivel.Tablero.setearObjeto(casillero, posicionJugador);
                nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 1);
            }
            else  // si el objeto proximo no es un casillero vacio
            {

                if (objProximo != null && objProximo.CompareTag("bloque") && objProximoProximo != null && objProximoProximo.CompareTag("casillero"))  // si es un bloque y el que le sige es un casillero vacio
                {
                    GuardarTablero(); // guardo el tablero antes de moverme

                    nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 1);
                    {
                        nivel.Tablero.setearObjeto(casillero, posicionJugador);
                        nivel.Tablero.setearObjeto(bloque, posicionJugador, orientacionJugador, 2); ;
                    }
                }
                else { }
            }
           
            InstanciadorPrefabs.instancia.graficarObjetosTablero(nivel.Tablero, SokobanLevelManager.instancia.dameLstPrefabsSokoban());

            posOcupadasBloques = nivel.Tablero.damePosicionesObjetos("Bloque"); // actualizo las posiciones de los bloques

            if (ChequearVictoria(nivel.Tablero))    // si los bloques estan en sus lugares, gane
            {
                gameOver = true;    
                Debug.Log("Gané");
            }
        }
        else  // estoy deshaciendo
        {
            Debug.Log("Intente deshacer.");

            estoyDeshaciendo = false;
        }
    }

    private bool SonIgualesLosVectores(Vector2 v1, Vector2 v2)
    {
        return (v1.x == v2.x && v1.y == v2.y);
    }

    private bool ChequearVictoria(Tablero tablero)  // compara las posiciones de los objetos en las listas de casillerotarget y bloque para ver si gane
    {
        int cantidad = posOcupadasEsperadasCasillerosTarget.Count;
        int check = 0;

        for (int p = 0; p < cantidad; p++)  // comparo todas las posiciones de una lista con las de otra
        {
            if (SonIgualesLosVectores(posOcupadasEsperadasCasillerosTarget[p], posOcupadasBloques[p]))
            {
                check++;    // aumento el contador de matches cada vez que coinciden las posiciones
            }
        }

        if (check == cantidad)  // si hubo igual cantidad de matches que cantidad de casillerostarget
        {
            return true;    //gane
        }
        else      // si no...
        {
            return false;   //no gane
        }
    }

    private void GuardarTablero()   // guarda los tableros en una pila
    {
        Tablero tablAux = new Tablero(nivel.Tablero.casilleros.GetLength(0), nivel.Tablero.casilleros.GetLength(1));
        tablAux.setearObjetos(casillero, nivel.Tablero.damePosicionesObjetos("Casillero"));
        tablAux.setearObjetos(casilleroTarget, nivel.Tablero.damePosicionesObjetos("CasilleroTarget"));
        tablAux.setearObjetos(bloque, nivel.Tablero.damePosicionesObjetos("Bloque"));
        tablAux.setearObjetos(pared, nivel.Tablero.damePosicionesObjetos("Pared"));
        tablAux.setearObjetos(jugador, nivel.Tablero.damePosicionesObjetos("Jugador"));

        pilaTablerosAnteriores.Push(tablAux); // agrego el tablero a la pila de tableros anteriores

        Debug.Log("Guarde el tablero en la pila de tableros anteriores.");

    }
}

